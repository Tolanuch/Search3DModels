using System;
using Inventor;
using System.Drawing;
using System.Windows.Forms;

namespace Search3DModel
{
    /// <summary>
    /// Button for showing Configuration Form
    /// </summary>
    /// 
    public class ConfigurationButton : Button
    {
        ConfigurationForm configurationForm;
        public ConfigurationButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType, Inventor.Application invApplication)
            : base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType, invApplication)
        {

        }
        protected override void ButtonDefinition_OnExecute(NameValueMap context)
        {
            try
            {
                configurationForm = ConfigurationForm.getInstance();
                configurationForm.Show();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
