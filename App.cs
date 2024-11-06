using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Internal.Windows;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.UI;
using RoomNumberTA.Commands;
namespace RoomNumberTA
{
    class App : IExternalApplication
    {
        private UIControlledApplication ControlledApplication { get; set; }

        public Result OnStartup(UIControlledApplication a)
        {
            ControlledApplication = a;
            string tabName = "TestAssign";
            CreateTab(tabName);
            var testPanel = CreatePanel(tabName, "TestPanel");
            CreatePushButton(testPanel, "Room_name");
            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
        public void CreateTab(string tabName)
        {
            bool isTab = false;
            Autodesk.Windows.RibbonTab ribbonTab = null;
            Autodesk.Windows.RibbonTabCollection ribbonTabCollection = Autodesk.Windows.ComponentManager.Ribbon.Tabs;
            foreach (Autodesk.Windows.RibbonTab tab in ribbonTabCollection)
            {
                if (tab.Id == tabName)
                {
                    ribbonTab = tab;
                    isTab = true;
                    break;
                }
            }
            if (isTab == false) ControlledApplication.CreateRibbonTab(tabName);
        }
        public RibbonPanel CreatePanel(string tabName, string panelName)
        {
            RibbonPanel panel = ControlledApplication.CreateRibbonPanel(tabName, panelName);
            return panel;
        }
        public PushButton CreatePushButton(RibbonPanel panel, string name)
        {
            
            string className = typeof(Cmd).FullName;
          
            var pathAssembly = typeof(Cmd).Assembly.Location;
            PushButtonData buttonData = new PushButtonData(
                name,
                name,
                pathAssembly,

                className)
            {

            };

            PushButton button = panel.AddItem(buttonData) as PushButton;
            return button;
        }
    }
    
   
    
    
}
