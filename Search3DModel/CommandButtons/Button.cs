using System;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic.Compatibility.VB6;
using Inventor;

namespace Search3DModel
{
	/// <summary>
	/// Base class for command buttons
	/// </summary>

	public abstract class Button
	{
		#region Data Members
        
		protected static Inventor.Application inventorApplication;

		private ButtonDefinition buttonDefinition;

        private ButtonDefinitionSink_OnExecuteEventHandler ButtonDefinition_OnExecuteEventDelegate;

		#endregion		

		#region "Properties"

		public static Inventor.Application InventorApplication
		{
			set
			{
                inventorApplication = value;
			}
			get
			{
                return inventorApplication;
			}
		}

		public Inventor.ButtonDefinition ButtonDefinition
		{
			get
			{
                return buttonDefinition;
			}
		}

		#endregion
        
		#region "Methods"

		public Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, Icon standardIcon, Icon largeIcon, ButtonDisplayEnum buttonDisplayType)
		{
            try
            {
				// Get IPictureDisp for icons
				stdole.IPictureDisp standardIconIPictureDisp;
				standardIconIPictureDisp = (stdole.IPictureDisp)Support.IconToIPicture(standardIcon);

				stdole.IPictureDisp largeIconIPictureDisp;
                largeIconIPictureDisp = (stdole.IPictureDisp)Support.IconToIPicture(largeIcon);
		
				// Create button definition
				buttonDefinition = inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition(displayName, internalName, commandType, clientId, description, tooltip, standardIconIPictureDisp , largeIconIPictureDisp, buttonDisplayType);
												
				// Enable the button
                buttonDefinition.Enabled = true;
				
				// Connect the button event sink
                ButtonDefinition_OnExecuteEventDelegate = new ButtonDefinitionSink_OnExecuteEventHandler(ButtonDefinition_OnExecute);
                buttonDefinition.OnExecute += ButtonDefinition_OnExecuteEventDelegate;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

		public Button(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
		{
            try
            {
                // Create button definition
                buttonDefinition = inventorApplication.CommandManager.ControlDefinitions.AddButtonDefinition(displayName, internalName, commandType, clientId, description, tooltip, Type.Missing, Type.Missing, buttonDisplayType);
								
				// Enable the button
                buttonDefinition.Enabled = true;
				
				// Connect the button event
				ButtonDefinition_OnExecuteEventDelegate = new ButtonDefinitionSink_OnExecuteEventHandler(ButtonDefinition_OnExecute);
                buttonDefinition.OnExecute += ButtonDefinition_OnExecuteEventDelegate;
        }
			catch(Exception e)
			{

                MessageBox.Show(e.ToString());
			}
}

		abstract protected void ButtonDefinition_OnExecute(NameValueMap context);

		#endregion
	}

}
