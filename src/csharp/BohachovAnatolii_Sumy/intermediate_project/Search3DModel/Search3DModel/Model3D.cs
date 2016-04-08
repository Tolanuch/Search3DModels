using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace Search3DModel
{
    /// <summary>
    /// Class for saving and searching model as object
    /// </summary>
    /// 
    class Model3D
    {
        #region Properies

       private readonly Configuration config = Configuration.getConfiguration();
        private MySqlConnection connection;
        private MySqlCommand mySQLCommand;
                
        private string name;
        /// <summary>
        /// Name of model in library.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }
        
        // Length of 3D model.
        private double x;
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }

        // Width of 3D model.
        private double y;
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }

        // Height of 3D model.
        private double z;
        public double Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
            }
        }

        public List<double> Parameters = new List<double>();

        #endregion Properties

        public Model3D(double x, double y, double z, string name)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            this.Name = name;
            // Creating MySQL connection.
            connection = new MySqlConnection("Server=" + config.IP
                 + "; Port=" + config.Port + "; user id=" + config.Username + "; password=" + config.Password
                 + "; Persist Security Info = True;  database = " + config.DBName + ";  charset = utf8;");
            config.ReadConfigurationFromFile();
            // Creating MySQL command.
            mySQLCommand = new MySqlCommand();
            mySQLCommand.Connection = connection;
            try
            {
                connection.Open();
                connection.Close();
            }
            catch (MySqlException)
            {
                connection.Close();
                MessageBox.Show("Cannot connect to database. Please, check your configuration");
            }
            
        }

        #region Methods

        // Method for adding model to database (DB).
        public string AddModel()
        {
            try
            {
                // Executing query to database.
                if (this.Exists())
                {
                    return "This model already exists in database";
                }
                else
                {
                    connection.Open();
                    // Creating query with correct ' recognition.
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

        // Method for searching model(s).
        public DataTable Search3DModel(int dev, int scale)
        {
            try
            {
                // Making double value from percentage.
                double scaleD = (double)scale/100.0;                
                double devMinus, devPlus;
                devMinus = 1.0 - (double)dev / 100.0;
                devPlus = 1.0 + (double)dev / 100.0;
                if (scaleD == 1)
                {
                    // Executing query.
                    connection.Open();
                    mySQLCommand.CommandText =
                        "SELECT * FROM parameters  WHERE (X>= " + (this.X * devMinus) + " and X<=" + (this.X * devPlus) + ") " +
                         " and (Y >= " + (this.Y * devMinus) + " and Y<= " + (this.Y * devPlus) + ") " +
                         " and (Z >= " + (this.Z * devMinus) + " and Z<= " + (this.Z * devPlus) + "); ";
                    var dataAdapter = new MySqlDataAdapter(mySQLCommand);
                    var dataTable = new DataTable();
                    dataTable.Clear();
                    dataAdapter.Fill(dataTable);
                    connection.Close();
                    return dataTable;
                }
                else
                {
                    connection.Open();
                    mySQLCommand.CommandText =
                        "SELECT * FROM parameters  WHERE ((X>= " + (this.X * devMinus*scaleD) + " and X<=" + (this.X * devPlus * scaleD) + ") " +
                         " and (Y >= " + (this.Y * devMinus * scaleD) + " and Y<= " + (this.Y * devPlus * scaleD) + ") " +
                         " and (Z >= " + (this.Z * devMinus * scaleD) + " and Z<= " + (this.Z * devPlus * scaleD) + ")) or " +
                         " ((X >= " + (this.X * devMinus) + " and X<= " + (this.X * devPlus) + ") " +
                         " and (Y >= " + (this.Y * devMinus) + " and Y<= " + (this.Y * devPlus) + ") " +
                         " and (Z >= " + (this.Z * devMinus) + " and Z<= " + (this.Z * devPlus) + "));";
                    var dataAdapter = new MySqlDataAdapter(mySQLCommand);
                    var dataTable = new DataTable();
                    dataTable.Clear();
                    dataAdapter.Fill(dataTable);
                    connection.Close();
                    return dataTable;
                }
            }
            catch (Exception e)
            {
                connection.Close();
                MessageBox.Show(e.Message);
                var dataTable = new DataTable();
                return dataTable;
            }
        }

        // Method to check model for existing in database and in library folder.
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

        public static Dictionary<string,double> Compare(Model3D referenceModel,  Model3D originalModel, List<Model3D> modelList,int deviation, int scale)
        {
            try
            {
                double stDev;
                double  devMinus, devPlus;
                double scaleD;
                scaleD = (double)scale / 100.0;
                devMinus = 1.0 - (double)deviation/ 100.0;
                devPlus = 1.0 + (double)deviation / 100.0;

                double avgParameter = 0.0;
                if (!((originalModel.X >= referenceModel.X * devMinus) && (originalModel.X <= referenceModel.X * devPlus)
                    && (originalModel.Y >= referenceModel.Y * devMinus) && (originalModel.Y <= referenceModel.Y * devPlus)
                    && (originalModel.Z >= referenceModel.Z * devMinus) && (originalModel.Z <= referenceModel.Z * devPlus)))
                {
                    originalModel.X /= scaleD;
                    originalModel.Y /= scaleD;
                    originalModel.Z /= scaleD;
                    foreach (var param in originalModel.Parameters)
                    {
                        avgParameter += param/scaleD;
                    }
                    avgParameter /= originalModel.Parameters.Count;
                }
                else
                {
                    foreach (var param in originalModel.Parameters)
                    {
                        avgParameter += param;
                    }
                    avgParameter /= originalModel.Parameters.Count;
                }

                // Creating standard deviation array for every model in modelList.
                Dictionary<string,double> standDeviation = new Dictionary<string, double>();

                int i,j;
               
                for (i = 0; i < modelList.Count; i++)
                {
                    stDev = 0;
                    if (!((modelList[i].X >= referenceModel.X * devMinus) && (modelList[i].X <= referenceModel.X * devPlus)
                    && (modelList[i].Y >= modelList[i].Y * devMinus) && (modelList[i].Y <= referenceModel.Y * devPlus)
                    && (modelList[i].Z >= referenceModel.Z * devMinus) && (modelList[i].Z <= referenceModel.Z * devPlus)))
                    {
                        modelList[i].X /= scaleD;
                        modelList[i].Y /= scaleD;
                        modelList[i].Z /= scaleD;
                        for (j = 0; j < modelList[i].Parameters.Count; j++)
                        {
                            stDev += Math.Pow((modelList[i].Parameters[j] / scaleD - avgParameter), 2);
                        }
                    }
                    else
                    {
                        for (j = 0; j < modelList[i].Parameters.Count; j++)
                        {
                            stDev += Math.Pow((modelList[i].Parameters[j] - avgParameter), 2);
                        }
                    }
                    standDeviation.Add(modelList[i].Name, Math.Sqrt(stDev / modelList[i].Parameters.Count));
                }

                // Sorting list by value.
                standDeviation = standDeviation.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key, pair => pair.Value);
                
                return standDeviation;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Dictionary<string, double> standDeviation = new Dictionary<string, double>()
                {
                    { "Something went wrong", -1.0 }
                };

                return standDeviation;
            }

        }

        #endregion Methods
    }
}
