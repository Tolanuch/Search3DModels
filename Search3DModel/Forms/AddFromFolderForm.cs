using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using Inventor;

namespace Search3DModel
{
    public partial class AddFromFolderForm : Form
    {
        private Inventor.Application inventorApp = Button.InventorApplication;
        private Configuration config;

        private  static AddFromFolderForm Instance;
        private AddFromFolderForm()
        {
            InitializeComponent();
            config = Configuration.getConfiguration();
            config.ReadConfigurationFromFile();
        }

        public static AddFromFolderForm getInstance()
        {
            if (Instance == null || Instance.IsDisposed)
                Instance = new AddFromFolderForm();
            return Instance;
        }
        // Method for selecting folder by user
        private void addFolderButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            folderDialog.ShowDialog();
            folderListBox.Items.Add(folderDialog.SelectedPath.ToString());
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void showModelsButton_Click(object sender, EventArgs e)
        {
            // Scan chosen directory(es) for inventor parts
            Regex file = new Regex(@".*\.ipt$");
            foreach (string folder in folderListBox.Items)
            {
                DirectoryInfo dr = new DirectoryInfo(@folder);
                Search(dr, file);
            }
        }

        // Scanning for Inventor Part files in chosen folders
        void Search(DirectoryInfo dr, Regex file)
        {
            FileInfo[] fi = dr.GetFiles();
            foreach (FileInfo info in fi)
            {
                if (file.IsMatch(info.Name))
                {
                    modelsListBox.Items.Add(info.FullName);
                }
            }
            DirectoryInfo[] dirs = dr.GetDirectories();
            foreach (DirectoryInfo directoryInfo in dirs)
            {
                Search(directoryInfo, file);
            }
        }
        // Method for adding all chosen models in list
        private void addModelsButton_Click(object sender, EventArgs e)
        {
            try
            {
                double x, y, z;
                // Converting modelsListBox items to string
                var paths = new System.Collections.Generic.List<string>();
                foreach (string item in modelsListBox.Items)
                {
                    paths.Add(item);
                }
                // Value of added documents
                int i = 0;
                // Creating analog element like outputsListBox which does not exists in UI thread.
                var outputs = new System.Collections.Generic.List<string>();
                // Multi threading for models adding
                Parallel.ForEach<string>(paths, modelPath =>
                    {
                        PartDocument partDoc = (PartDocument)inventorApp.Documents.Open(modelPath, false);
                        Box size = partDoc.ComponentDefinition.RangeBox;
                        x = Math.Abs(size.MaxPoint.X - size.MinPoint.X);
                        y = Math.Abs(size.MaxPoint.Y - size.MinPoint.Y);
                        z = Math.Abs(size.MaxPoint.Z - size.MinPoint.Z);
                        Model3D model = new Model3D(x, y, z, partDoc.DisplayName);
                        if (!model.Exists())
                        {
                            i++;
                            partDoc.SaveAs(config.Path + "\\" + partDoc.DisplayName, true);
                        }
                        outputs.Add((partDoc.DisplayName + ": " + model.AddModel()).ToString());
                        partDoc.Close(true);                        
                    });
                // Adding output information to UI
                foreach (string output in outputs)
                    outputListBox.Items.Add(output);
                outputListBox.Items.Add("Added " + i + " files");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void folderListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                folderListBox.Items.Remove(folderListBox.SelectedItem);
        }

        private void modelsListBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Delete)
                modelsListBox.Items.Remove(modelsListBox.SelectedItem);
        }
    }
}
