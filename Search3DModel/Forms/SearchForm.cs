using System;
using System.Data;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Search3DModel
{
    public partial class SearchForm : Form
    {
        private readonly Inventor.Application inventorApp = Button.InventorApplication;
        private readonly Configuration config;

        private static SearchForm Instance;
        private SearchForm()
        {
            InitializeComponent();
            config = Configuration.getConfiguration();
            config.ReadConfigurationFromFile();
        }

        public static SearchForm getInstance()
        {
            if (Instance == null || Instance.IsDisposed)
                Instance = new SearchForm();
            return Instance;
        }

        // Handling non-numerical symbols.
        private void numericOnly(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar==(char)Keys.Delete || e.KeyChar==(char)Keys.Back)
            {
                e.Handled = false;
                return;
            }
            if ((e.KeyChar >= 48) && (e.KeyChar <= 57) )
                e.Handled = false;
            else e.Handled = true;
        }

        // Check entered information about model parameters.
        private void checkFormat(object sender, EventArgs e)
        {
            if (!System.Text.RegularExpressions.Regex.IsMatch((sender as TextBox).Text, "^0(.\\d{1,10})?$|^[1-9][0-9]*(.\\d{1,10})?$"))
            {
                MessageBox.Show("Wrong format for '"+(sender as TextBox).Text + "' value");
                (sender as TextBox).Focus();
                return;
            }
            
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
        // Fixing entered too big deviation.
        private void deviationBox_Leave(object sender, EventArgs e)
        {
            if ((sender as NumericUpDown).Value > 100)
                (sender as NumericUpDown).Value = 100;
        }

        // Method for opening selected model in table.
        private void openButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Closing all opened documents.
                inventorApp.Documents.CloseAll();

                // Creating list of found model.
                List<string> names = new List<string>();
               for (var i = 0;i<searchGridView.Rows.Count; i++)
                {
                    names.Add(searchGridView.Rows[i].Cells["Name"].Value.ToString());
                }

                Inventor.PartDocument partDoc;

                // Opening all found documents.
                Parallel.ForEach<string>(names, name =>
                {
                    // Full path to the model in library.
                    string path;
                    path = config.Path + "\\" + name;
                    if (System.IO.File.Exists(path))
                    {
                        partDoc = (Inventor.PartDocument)inventorApp.Documents.Open(path, true);                        
                    }
                    else
                    {
                        MessageBox.Show("Cannot find file " + path);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Metod for searching model using entered model parameters.
        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Taking paramters of model.
                double x = Convert.ToDouble(xTextBox.Text);
                double y = Convert.ToDouble(yTextBox.Text);
                double z = Convert.ToDouble(zTextBox.Text);
                int deviation;
                deviation= (int)deviationBox.Value;
                int scale;
                scale = Convert.ToInt32(scaleBox.Text);

                Model3D model = new Model3D(x, y, z, "Search");
                DataTable dataTable;
                dataTable = model.Search3DModel(deviation, scale);
                searchGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SearchForm_Load(object sender, EventArgs e)
        {
            scaleBox.Text = "100";
        }

        private void compareButton_Click(object sender, EventArgs e)
        {
            if (searchGridView.RowCount<=1)
            {
                MessageBox.Show("You have 1 or less models in resault. It does not make sanse to compare.");
                return;
            }
            try
            {
                int i;
                double x, y, z;
                List<Model3D> modelList = new List<Model3D>();

                Inventor.Parameters allParams;
                Inventor.PartDocument partDoc = (Inventor.PartDocument)inventorApp.ActiveDocument;
                Inventor.Box size;

                size = partDoc.ComponentDefinition.RangeBox;
                x = Math.Abs(size.MaxPoint.X - size.MinPoint.X);
                y = Math.Abs(size.MaxPoint.Y - size.MinPoint.Y);
                z = Math.Abs(size.MaxPoint.Z - size.MinPoint.Z);
                Model3D originalModel = new Model3D(x, y, z, partDoc.DisplayName);
                // Reading all parameters of document.
                allParams = partDoc.ComponentDefinition.Parameters;
                for (i = 1; i <= allParams.ModelParameters.Count; i++)
                    originalModel.Parameters.Add(Convert.ToDouble(allParams.ModelParameters[i].Value));
                bool original = false;
                // Creating list of models for comparing
                for (int j = 1; j <= inventorApp.Documents.Count; j++)
                {
                    partDoc = (Inventor.PartDocument)inventorApp.Documents[j];

                    if (partDoc.DisplayName == originalModel.Name)
                    {
                        original = true;
                        continue;
                    }

                    size = partDoc.ComponentDefinition.RangeBox;
                    x = Math.Abs(size.MaxPoint.X - size.MinPoint.X);
                    y = Math.Abs(size.MaxPoint.Y - size.MinPoint.Y);
                    z = Math.Abs(size.MaxPoint.Z - size.MinPoint.Z);

                    // Creating model object from current model.
                    modelList.Add(new Model3D(x, y, z, partDoc.DisplayName));

                    // Reading all parameters of document.
                    allParams = partDoc.ComponentDefinition.Parameters;
                    for (i = 1; i <= allParams.ModelParameters.Count; i++)
                    {
                        if (original == false)
                            modelList[j - 1].Parameters.Add(Convert.ToDouble(allParams.ModelParameters[i].Value));
                        else modelList[j - 2].Parameters.Add(Convert.ToDouble(allParams.ModelParameters[i].Value));
                    }
                }

                //Creating reference model.
                x = Convert.ToDouble(xTextBox.Text);
                y = Convert.ToDouble(yTextBox.Text);
                z = Convert.ToDouble(zTextBox.Text);
                Model3D referenceModel = new Model3D(x, y, x, "Reference");

                int deviation, scale;
                deviation = Convert.ToInt32(deviationBox.Value);
                scale = Convert.ToInt32(scaleBox.Text);
                Dictionary <string, double> standardDeviation = new Dictionary<string, double>();
                standardDeviation = Model3D.Compare(referenceModel,originalModel, modelList, deviation,scale);

                var searchInfoForm = new SearchInfoForm();
                searchInfoForm.paramListBox.Items.Add("Sorted from less to high (less = more similar to original) :");
                foreach (var item in standardDeviation)
                {
                    searchInfoForm.paramListBox.Items.Add(item.Key + " " + item.Value);
                }

                searchInfoForm.Show();
                searchInfoForm.Activate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
