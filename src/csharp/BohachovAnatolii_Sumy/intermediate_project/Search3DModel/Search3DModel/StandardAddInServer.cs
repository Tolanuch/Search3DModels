using System;
using System.Runtime.InteropServices;
using Inventor;
using Microsoft.Win32;
using System.Drawing;
using System.Windows.Forms;

namespace Search3DModel
{
    /// <summary>
    /// This is the primary AddIn Server class that implements the ApplicationAddInServer interface
    /// that all Inventor AddIns are required to implement. The communication between Inventor and
    /// the AddIn is via the methods on this interface.
    /// </summary>
    [GuidAttribute("08eb6268-0bd8-4e22-8a84-b23a6c873f96")]
    public class StandardAddInServer : Inventor.ApplicationAddInServer
    {
        #region Members

        // Inventor application object.
        private Inventor.Application inventorApplication;

        // Buttons
        private ConfigurationButton configurationButton;
        private AddButton addButton;
        private SearchButton searchButton;
        private AddFromFolderButton addFromFolderButton;

        // User interface event
        private UserInterfaceEvents userInterfaceEvents;

        // Ribbon panel
        RibbonPanel search3DRibbonPanel;

        //event handler delegates
        private Inventor.UserInterfaceEventsSink_OnResetCommandBarsEventHandler
            UserInterfaceEventsSink_OnResetCommandBarsEventDelegate;
        private Inventor.UserInterfaceEventsSink_OnResetEnvironmentsEventHandler
            UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate;
        private Inventor.UserInterfaceEventsSink_OnResetRibbonInterfaceEventHandler
            UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate;

        #endregion Members
        public StandardAddInServer()
        {
        }

        #region ApplicationAddInServer Members

        public void Activate(Inventor.ApplicationAddInSite addInSiteObject, bool firstTime)
        {

            // This method is called by Inventor when it loads the addin.
            // The AddInSiteObject provides access to the Inventor Application object.
            // The FirstTime flag indicates if the addin is loaded for the first time.

            // Initialize AddIn members.
            inventorApplication = addInSiteObject.Application;

            // Initialize event delegates
            userInterfaceEvents = inventorApplication.UserInterfaceManager.UserInterfaceEvents;

            UserInterfaceEventsSink_OnResetCommandBarsEventDelegate = new UserInterfaceEventsSink_OnResetCommandBarsEventHandler(UserInterfaceEvents_OnResetCommandBars);
            userInterfaceEvents.OnResetCommandBars += UserInterfaceEventsSink_OnResetCommandBarsEventDelegate;

            UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate = new UserInterfaceEventsSink_OnResetEnvironmentsEventHandler(UserInterfaceEvents_OnResetEnvironments);
            userInterfaceEvents.OnResetEnvironments += UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate;

            UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate = new UserInterfaceEventsSink_OnResetRibbonInterfaceEventHandler(UserInterfaceEvents_OnResetRibbonInterface);
            userInterfaceEvents.OnResetRibbonInterface += UserInterfaceEventsSink_OnResetRibbonInterfaceEventDelegate;            

            // Retrieve the GUID for this class
            GuidAttribute addInCLSID;
            addInCLSID = (GuidAttribute)GuidAttribute.GetCustomAttribute(typeof(StandardAddInServer), typeof(GuidAttribute));
            string addInCLSIDString;
            addInCLSIDString = "{" + addInCLSID.Value + "}";


            // Create buttons
            addButton = new AddButton(
                "Add", "Add", CommandTypesEnum.kShapeEditCmdType,
                addInCLSIDString, "Add current detail to database",
                "Add", ButtonDisplayEnum.kAlwaysDisplayText, inventorApplication);

            searchButton = new SearchButton(
                "Search", "Search", CommandTypesEnum.kShapeEditCmdType,
                addInCLSIDString, "Search 3D model(s)",
                "Search", ButtonDisplayEnum.kAlwaysDisplayText, inventorApplication);

            configurationButton = new ConfigurationButton(
                "Configuration", "Configuration", CommandTypesEnum.kShapeEditCmdType,
                addInCLSIDString, "Configuration database connection",
                "Configuration",  ButtonDisplayEnum.kAlwaysDisplayText, inventorApplication);

            addFromFolderButton = new AddFromFolderButton(
                "Add from folder(s)", "Add from folder(s)", CommandTypesEnum.kShapeEditCmdType,
                addInCLSIDString, "Add 3D model(s) from folder(s)",
                "Add from folder(s)", ButtonDisplayEnum.kAlwaysDisplayText, inventorApplication);

            // Create the command category
            CommandCategory cmdCategory = inventorApplication.CommandManager.CommandCategories.Add("3DModel", "Autodesk:Search3DModel:SlotCmdCat", addInCLSIDString);

            cmdCategory.Add(addButton.ButtonDefinition);
            cmdCategory.Add(searchButton.ButtonDefinition);
            cmdCategory.Add(configurationButton.ButtonDefinition);
            cmdCategory.Add(addFromFolderButton.ButtonDefinition);

            UserInterfaceEvents_OnResetRibbonInterface(null);

        }

        public void Deactivate()
        {
            // This method is called by Inventor when the AddIn is unloaded.
            // The AddIn will be unloaded either manually by the user or
            // when the Inventor session is terminated

            try
            {
                userInterfaceEvents.OnResetCommandBars -= UserInterfaceEventsSink_OnResetCommandBarsEventDelegate;
                userInterfaceEvents.OnResetEnvironments -= UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate;

                UserInterfaceEventsSink_OnResetCommandBarsEventDelegate = null;
                UserInterfaceEventsSink_OnResetEnvironmentsEventDelegate = null;
                userInterfaceEvents = null;
                if (search3DRibbonPanel != null)
                {
                    search3DRibbonPanel.Delete();
                }

                // Release inventor Application object
                Marshal.ReleaseComObject(inventorApplication);
                inventorApplication = null;

                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        public void ExecuteCommand(int commandID)
        {
            // Note:this method is now obsolete, you should use the 
            // ControlDefinition functionality for implementing commands.
        }

        public object Automation
        {
            // This property is provided to allow the AddIn to expose an API 
            // of its own to other programs. Typically, this  would be done by
            // implementing the AddIn's API interface in a class and returning 
            // that class object through this property.

            get
            {
                // TODO: Add ApplicationAddInServer.Automation getter implementation
                return null;
            }
        }

        private void UserInterfaceEvents_OnResetCommandBars(ObjectsEnumerator commandBars, NameValueMap context)
        {
            try
            {
                CommandBar commandBar;
                for (int i = 1; i <= commandBars.Count; i++)
                {
                    commandBar = (Inventor.CommandBar)commandBars[i];
                    if (commandBar.InternalName == "Autodesk:Search3DModel:SlotToolbar")
                    {
                        // Add buttons to toolbar
                        commandBar.Controls.AddButton(addButton.ButtonDefinition, 0);
                        commandBar.Controls.AddButton(configurationButton.ButtonDefinition, 0);
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void UserInterfaceEvents_OnResetEnvironments(ObjectsEnumerator environments, NameValueMap context)
        {
            try
            {
                Inventor.Environment environment;
                for (int i = 1; i <= environments.Count; i++)
                {
                    environment = (Inventor.Environment)environments[i];
                    if (environment.InternalName == "PMxPartSketchEnvironment")
                    {
                        // Make this command bar accessible in the panel menu for the 2d sketch environment.
                        environment.PanelBar.CommandBarList.Add(inventorApplication.UserInterfaceManager.CommandBars["Autodesk:Search3DModel:SlotToolbar"]);

                        return;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void UserInterfaceEvents_OnResetRibbonInterface(NameValueMap context)
        {
            try
            {
                UserInterfaceManager userInterfaceManager;
                userInterfaceManager = inventorApplication.UserInterfaceManager;

                // Get the ribbon associated with part document
                Inventor.Ribbons ribbons;
                ribbons = userInterfaceManager.Ribbons;

                Inventor.Ribbon partRibbon;
                partRibbon = ribbons["Part"];

                // Create a new ribbon tab
                RibbonTabs ribbonTabs;
                ribbonTabs = partRibbon.RibbonTabs;

                RibbonTab partSketchRibbonTab;
                partSketchRibbonTab = ribbonTabs.Add("Search 3D Model", "Autodesk:Search3DModel:SlotRibbonPanel", "{08eb6268-0bd8-4e22-8a84-b23a6c873f96}", "", false);
                // Create a new panel with the tab
                RibbonPanels ribbonPanels;
                ribbonPanels = partSketchRibbonTab.RibbonPanels;

                search3DRibbonPanel = ribbonPanels.Add("Actions", "Autodesk:Search3DModel:SlotRibbonPanel", "{08eb6268-0bd8-4e22-8a84-b23a6c873f96}", "", false);

                // Add controls to the slot panel
                CommandControls partSketchSlotRibbonPanelCtrls;
                partSketchSlotRibbonPanelCtrls = search3DRibbonPanel.CommandControls;

                CommandControl addCmdBtnCmdCtrl;
                addCmdBtnCmdCtrl = partSketchSlotRibbonPanelCtrls.AddButton(addButton.ButtonDefinition, false, true, "", false);

                //CommandControl separator1;
                //separator0 = partSketchSlotRibbonPanelCtrls.AddSeparator();

                CommandControl AddFilesFromFoldersCmdBtnCmdCtrl;
                AddFilesFromFoldersCmdBtnCmdCtrl = partSketchSlotRibbonPanelCtrls.AddButton(addFromFolderButton.ButtonDefinition, false, true, "", false);

                //CommandControl separator2;
                //separator1 = partSketchSlotRibbonPanelCtrls.AddSeparator();

                CommandControl SearchCmdBtnCmdCtrl;
                SearchCmdBtnCmdCtrl = partSketchSlotRibbonPanelCtrls.AddButton(searchButton.ButtonDefinition, false, true, "", false);

                //CommandControl separator3;
                //separator3 = partSketchSlotRibbonPanelCtrls.AddSeparator();

                CommandControl configurationCmdBtnCmdCtrl;
                configurationCmdBtnCmdCtrl = partSketchSlotRibbonPanelCtrls.AddButton(configurationButton.ButtonDefinition, false, true, "", false);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        #endregion

    }
}
