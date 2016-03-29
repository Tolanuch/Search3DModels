using System;
using System.Data;
using System.Windows.Forms;

namespace Search3DModel
{
    public partial class SearchForm : Form
    {
        private Inventor.Application inventorApp = Button.InventorApplication;
        private Configuration config;

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
        // Handling non-numerical symbols
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
        // Check entered information about model parameters
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
        // Fixing entered too big deviation
        private void deviationBox_Leave(object sender, EventArgs e)
        {
            if ((sender as NumericUpDown).Value > 100)
                (sender as NumericUpDown).Value = 100;
        }
        // Method for opening selected model in table
        private void openButton_Click(object sender, EventArgs e)
        {
            try
            {
                Inventor.PartDocument partDoc;
                string name;
                int index;

                index=searchGridView.SelectedCells[0].RowIndex;
                name = searchGridView.Rows[index].Cells["Name"].Value.ToString();
                string path;
                path = config.Path + "\\" + name;
                if (System.IO.File.Exists(path))
                    partDoc = (Inventor.PartDocument)inventorApp.Documents.Open(path, true);
                else
                {
                    MessageBox.Show("Cannot find file " + path);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        // Metod for searching model using entered model parameters
        private void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                double x = Convert.ToDouble(xTextBox.Text);
                double y = Convert.ToDouble(yTextBox.Text);
                double z = Convert.ToDouble(zTextBox.Text);
                Model3D model = new Model3D(x, y, z, "");
                DataTable dataTable;
                dataTable = model.Search3DModel((int)deviationBox.Value);
                searchGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
