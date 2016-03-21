using System;
using Inventor;
using System.Drawing;
using System.Windows.Forms;

namespace Search3DModel
{
    /// <summary>
    /// Class to show form for adding 3D model(s) from folder to database
    /// </summary>
    /// 
    public class AddFromFolderButton : Button
    {        
        public AddFromFolderButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType, Inventor.Application invApplication)
			: base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType,invApplication)
		{

        }
        protected override void ButtonDefinition_OnExecute(NameValueMap context)
        {
            try
            {
                // Methods to show 

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
