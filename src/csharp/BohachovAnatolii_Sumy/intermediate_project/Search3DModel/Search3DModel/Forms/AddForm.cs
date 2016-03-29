using System;
using System.Windows.Forms;
using Inventor;

namespace Search3DModel
{
    public partial class AddForm : Form
    {
        private static AddForm Instance;
        private Inventor.Application inventorApp = Button.InventorApplication;
        private Configuration config;

        private AddForm()
        {
            InitializeComponent();
            config = Configuration.getConfiguration();
            config.ReadConfigurationFromFile();
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
                PartDocument currentDoc = (PartDocument)inventorApp.ActiveDocument;
                Box size = currentDoc.ComponentDefinition.RangeBox;
                // Taking parameters
                double x = Math.Abs(size.MaxPoint.X - size.MinPoint.X);
                double y = Math.Abs(size.MaxPoint.Y - size.MinPoint.Y);
                double z = Math.Abs(size.MaxPoint.Z - size.MinPoint.Z);
                var currentModel = new Model3D(x, y, z, currentDoc.DisplayName);
                if (!currentModel.Exists())
                    currentDoc.SaveAs(config.Path + "\\" + currentDoc.DisplayName, true);
                addLabel.Text = currentModel.AddModel();
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
