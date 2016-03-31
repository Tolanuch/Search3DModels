using System;
using System.Windows.Forms;

namespace Search3DModel
{
    public partial class ConfigurationForm : Form
    {
        private static ConfigurationForm Instance;

        private readonly Configuration config;
        private ConfigurationForm()
        {
            InitializeComponent();
            config = Configuration.getConfiguration();
            config.ReadConfigurationFromFile();
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
            // Save configuration to the object.
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
            // Load configurations to form.
            pathTextBox.Text=config.Path;
            ipTextBox.Text=config.IP;
            portTextBox.Text=config.Port;
            userNameTextBox.Text = config.Username;
            passwordTextBox.Text = config.Password;
            dbNameTextBox.Text = config.DBName;
        }
    }
}
