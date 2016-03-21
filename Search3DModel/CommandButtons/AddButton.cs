using System;
using Inventor;
using System.Drawing;
using System.Windows.Forms;

namespace Search3DModel
{
    class AddButton : Button
    {
        /// <summary>
        /// Class to add current 3D model to database
        /// </summary>
        public AddButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType, Inventor.Application invApplication)
			: base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType,invApplication)
		{
			
		}
        protected override void ButtonDefinition_OnExecute(NameValueMap context)
        {
            try
            {
                //var openDialog = new OpenFileDialog();
                //openDialog.Multiselect = true;
                //openDialog.Filter = "Inventor Files (.ipt)|*.ipt";
                //openDialog.ShowDialog();

                //var metadatas = new MetaDataManager().GetMetadata(dialog.FileNames);
                //DetailsDataBase.Instance.Add(metadatas);


                //MessageBox.Show(string.Format("{0} items added to database.", metadatas.Count()));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

    }
}
