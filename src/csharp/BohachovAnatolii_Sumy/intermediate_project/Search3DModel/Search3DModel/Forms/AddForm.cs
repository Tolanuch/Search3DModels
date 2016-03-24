using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Inventor;

namespace Search3DModel
{
    public partial class AddForm : Form
    {
        private static AddForm Instance;
        private Inventor.Application inventorApp = Button.InventorApplication;

        Configuration config = Configuration.getConfiguration();
        private AddForm()
        {
            InitializeComponent();
        }

        public static AddForm getInstance()
        {
            if (Instance == null || Instance.IsDisposed)
            {
                Instance = new AddForm();
            }
            return Instance;
        }

        private void AddForm_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    config.ReadConfigurationFromFile();
                    PartDocument currentDoc = (PartDocument)inventorApp.ActiveDocument;
                    Box size = currentDoc.ComponentDefinition.RangeBox;
                    // Taking parameters
                    double x = Math.Ceiling(Math.Abs(size.MaxPoint.X - size.MinPoint.X));
                    double y = Math.Ceiling(Math.Abs(size.MaxPoint.Y - size.MinPoint.Y));
                    double z = Math.Ceiling(Math.Abs(size.MaxPoint.Z - size.MinPoint.Z));
                    var currentModel = new Model3D(x, y, z, currentDoc.DisplayName);
                    if (!currentModel.Exists())
                        currentDoc.SaveAs(config.Path + "\\" + currentDoc.DisplayName, true);
                    addLabel.Text = currentModel.AddCurrentModel();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }
    }
}
