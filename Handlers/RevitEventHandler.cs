using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RoomNumberTA.Handlers
{
    public class RevitEventHandler : IExternalEventHandler
    {
        public UIApplication App { get; set; }
        public Action TransactAction;

        public void Execute(UIApplication app)
        {
            try
            {
                TransactAction?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public string GetName()
        {
            return "RevitMethod";
        }
    }
}
