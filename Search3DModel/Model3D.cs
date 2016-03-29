using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Search3DModel
{
    /// <summary>
    /// Class for saving and searching model as object
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
                        "INSERT INTO parameters (Name, X,Y,Z) values ('" + this.Name + "', " + this.X + "," + this.Y + ", " + this.Z + ");".Replace("'", "''");
                    mySQLCommand.ExecuteNonQuery();
                    connection.Close();
                    return "Model have added to database";
                }
            }
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show(e.Message);
                return "Something went wrong!";
            }
        }

        // Method for searching model(s)
        public DataTable Search3DModel(int dev)
        {
            try
            {
                // Making double value from percentage
                double divMinus, divPlus;
                divMinus = 1.0 - (double)dev / 100.0;
                divPlus = 1.0 + (double)dev / 100.0;                
                connection.Open();
                mySQLCommand.CommandText =
                    "SELECT * FROM parameters  WHERE (X>= " + (this.X*divMinus) + " and X<=" + (this.X * divPlus) + ") "+
                     " and (Y >= " + (this.Y*divMinus) + " and Y<= " + (this.Y * divPlus) + ") " +
                     " and (Z >= " + (this.Z*divMinus) + " and Z<= " + (this.Z * divPlus) + "); "  ;
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(mySQLCommand);
                DataTable dataTable = new DataTable();
                dataTable.Clear();
                dataAdapter.Fill(dataTable);
                connection.Close();
                return dataTable;
            }
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show(e.Message);
                DataTable dataTable;
                return dataTable = new DataTable();
            }
        }
        // Method to check model for existing in database and in library folder
        public bool Exists()
        {
            try
            {
                connection.Open();
                mySQLCommand.CommandText =
                    "SELECT ID FROM parameters  WHERE Name = '" + this.Name + "' and  X= " + this.X + " and Y= " + this.Y + " and Z=" + this.Z + ";".Replace("'", "''"); 
                MySqlDataReader reader = mySQLCommand.ExecuteReader();
                if (reader.HasRows && System.IO.File.Exists(config.Path + "\\" + this.Name))
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
                connection.Close();
                MessageBox.Show(e.Message);
                return false;
            }
        }

        #endregion Methods
    }
}
