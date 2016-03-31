using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;

namespace Search3DModel
{
    /// <summary>
    /// Configuration class to manage configuration in add-in app.
    /// </summary>
    /// 
   public class Configuration
    {

        #region Properties

        private string cfgFilePath;
        private static Configuration instance;

        // Path to the library folder with model files.
        private string path;
        public string Path
        {
            get
            {
                return path;
            }
            set
            {
                if (Regex.IsMatch(value, "^[a-zA-Z]:"))
                {
                    path = value;
                }
                else
                {
                    this.path = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Documents\\Inventor\\DetailsLibrary";
                    if (!Directory.Exists(this.path))
                        Directory.CreateDirectory(this.path);
                    MessageBox.Show("Wrong library path format. Path library setted to "+this.path);
                }
            }
        }

        // IP string for DB connection.
        private string ip;
        public string IP
        {
            get
            {
                return ip;
            }
            set
            {
                if (Regex.IsMatch(value, "^((25[0-5]|2[0-4]\\d|[01]?\\d\\d?).){3}(25[0-5]|2[0-4]\\d|[01]?\\d\\d?)$"))
                {
                    ip = value;
                }
                else
                {
                    ip = "127.0.0.1";
                    MessageBox.Show("Wrong IP format. It was '127.0.0.1' setted");
                }
            }
        }

        // Port string for DB connection.
        private string port;
        public string Port
        {
            get
            {
                return port;
            }
            set
            {
                if (Regex.IsMatch(value, "^[0-9]+$"))
                {
                    port = value;
                }
                else
                {
                    port = "3306";
                    MessageBox.Show("Wrong port format. It was setted '3306' port");
                }
            }
        }

        // Username string for DB connection.
        private string username;
        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                if (Regex.IsMatch(value, "^[a-zA-Z][a-zA-Z0-9-_.]{1,16}$"))
                {
                    username = value;
                }
                else
                {
                    username = "root";
                    MessageBox.Show("Wrong or too long username format. It was setted 'root' username");
                }
            }
        }

        // Password string for DB connection.
        private string password;
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        //Database name string for DB connection.
        private string dbName;
        public string DBName
        {
            get
            {
                return dbName;
            }
            set
            {
                dbName = value;
            }
        }

        #endregion Properties

        private Configuration()
        {            
            this.cfgFilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)+"\\Documents\\Inventor" + "\\config_file.cfg";
            if (!File.Exists(this.cfgFilePath))
            {
                File.Create(this.cfgFilePath);
            }
        }
        public static Configuration getConfiguration()
        {
            if (instance == null)
                instance = new Configuration();
            return instance;
        }

        #region Methods

        // Method to read configuration information from cfg file.
        public void ReadConfigurationFromFile()
        {  
            try
            {
            StreamReader cfgFile = new StreamReader(cfgFilePath);
                if (cfgFile.Peek() != -1)
                {
                    instance.Path = cfgFile.ReadLine();
                    instance.IP = cfgFile.ReadLine();
                    instance.Port = cfgFile.ReadLine();
                    instance.Username = cfgFile.ReadLine();
                    instance.Password = cfgFile.ReadLine();
                    instance.DBName = cfgFile.ReadLine();
                    cfgFile.Close();
                }
                else
                {
                    MessageBox.Show("Configuration file does not exist or empty");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Method to write configuration information to cfg file.
        public void WriteConfigurationToFile()
        {
            try
            {  
                StreamWriter cfgFile = new StreamWriter(cfgFilePath);
                cfgFile.WriteLine(instance.Path);
                cfgFile.WriteLine(instance.IP);
                cfgFile.WriteLine(instance.Port);
                cfgFile.WriteLine(instance.Username);
                cfgFile.WriteLine(instance.Password);
                cfgFile.WriteLine(instance.DBName);
                cfgFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion Methods
    }
}
