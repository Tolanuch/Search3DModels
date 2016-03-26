using System;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Search3DModel
{
    /// <summary>
    /// Class for saving model as object
    /// </summary>
    /// 
    class Model3D
    {
        #region Properies

        private Configuration config = Configuration.getConfiguration();
        private MySqlConnection connection;
        private MySqlCommand mySQLCommand;
                
        private string name;
        /// <summary>
        /// Name of model in library
        /// </summary>
        public string Name { set; get; }
        // Length of 3D model
        private double x;
        public double X { get; set; }
        // Width of 3D model
        private double y;
        public double Y { get; set; }
        // Height of 3D model
        private double z;
        public double Z { get; set; }

        #endregion Properties

        public Model3D(double x, double y, double z, string name)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Name = name;
            // Creating MySQL connection
            connection = new MySqlConnection("Server=" + config.IP
                 + "; Port=" + config.Port + "; user id=" + config.Username + "; password=" + config.Password
                 + "; Persist Security Info = True;  database = " + config.DBName + ";  charset = utf8;");
            config.ReadConfigurationFromFile();
            // Creating MySQL command
            mySQLCommand = new MySqlCommand();
            mySQLCommand.Connection = connection;
        }

        #region Methods

        // Method for adding model to database (DB)
        public string AddModel()
        {
            try
            {
                // Executing query to database
                if (this.Exists())
                {
                    return "This model already exists in database";
                }
                else
                {
                    connection.Open();
                    // Creating query with correct ' recognition
                    mySQLCommand.CommandText =
                        "INSERT INTO parameters (Path, X,Y,Z) values ('" + this.Name + "', " + this.X + "," + this.Y + ", " + this.Z + ");".Replace("'", "''");
                    mySQLCommand.ExecuteNonQuery();
                    connection.Close();
                    return "Model have added to database";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return "Something went wrong!";
            }
        }

        // Method for searching model(s)
        public void Search3DModel()
        {

        }

        public bool Exists()
        {
            try
            {
                connection.Open();
                mySQLCommand.CommandText =
                    "SELECT ID FROM parameters  WHERE Path = '" + this.Name + "' and  X= " + this.X + " and Y= " + this.Y + " and Z=" + this.Z + ";".Replace("'", "''"); 
                MySqlDataReader reader = mySQLCommand.ExecuteReader();
                if (reader.HasRows || System.IO.File.Exists(config.Path + "\\" + this.Name))
                {
                    connection.Close();
                    return true;
                }
                else
                {
                    connection.Close();
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        #endregion Methods
    }
}
