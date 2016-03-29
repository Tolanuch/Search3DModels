using System;
using Inventor;
using System.Windows.Forms;

namespace Search3DModel
{
    /// <summary>
    /// Class to show form for searching 3D model(s) in database
    /// </summary>
    /// 
    public class SearchButton : Button
    {       
        public SearchButton(string displayName, string internalName, CommandTypesEnum commandType, string clientId, string description, string tooltip, ButtonDisplayEnum buttonDisplayType)
			: base(displayName, internalName, commandType, clientId, description, tooltip, buttonDisplayType)
		{

        }
        protected override void ButtonDefinition_OnExecute(NameValueMap context)
        {
            try
            {
                SearchForm searchForm = SearchForm.getInstance();
                searchForm.Show();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
