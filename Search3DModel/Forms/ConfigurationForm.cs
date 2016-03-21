using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Search3DModel
{
    public partial class ConfigurationForm : Form
    {
        private static ConfigurationForm Instance;

        ConfigurationClass config =  ConfigurationClass.getConfiguration();
        private ConfigurationForm()
        {
            InitializeComponent();
        }

        public static ConfigurationForm getInstance()
        {
            if (Instance==null || Instance.IsDisposed)
            {
                Instance = new ConfigurationForm();
            }
            return Instance;
        }

        private void saveConfigurationButton_Click(object sender, EventArgs e)
        {
            // Save configuration            
            config.Path = pathTextBox.Text;
            config.IP = ipTextBox.Text;
            config.Port = portTextBox.Text;
            config.Username = userNameTextBox.Text;
            config.Password = passwordTextBox.Text;
            config.DBName = dbNameTextBox.Text;

            config.WriteConfigurationToFile();
            MessageBox.Show("Configuration saved");
        }

        private void ConfigurationForm_Load(object sender, EventArgs e)
        {
            // Load configurations to form            
            config.ReadConfigurationFromFile();

            pathTextBox.Text=config.Path;
            ipTextBox.Text=config.IP;
            portTextBox.Text=config.Port;
            userNameTextBox.Text = config.Username;
            passwordTextBox.Text = config.Password;
            dbNameTextBox.Text = config.DBName;
        }
    }
}
