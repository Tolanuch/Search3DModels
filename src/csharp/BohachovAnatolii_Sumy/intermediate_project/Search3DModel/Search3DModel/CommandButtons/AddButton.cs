using System;
using Inventor;
using System.Drawing;
using System.Windows.Forms;

namespace Search3DModel
{
    /// <summary>
    /// Class to add current 3D model to database
    /// </summary>
    /// 
    class AddButton : Button
    {       
        AddForm addForm;
        public AddButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
			: base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
		{
			
		}
        protected override void ButtonDefinition_OnExecute(NameValueMap context)
        {
            try
            {
                addForm = AddForm.getInstance();
                addForm.Show();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

    }
}
