using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoomNumberTA.Handlers;
using RoomNumberTA.Common;
using Autodesk.Revit.Attributes;
using System.Windows;
using RoomNumberTA.Models;
using RoomNumberTA.Interfaces;
using RoomNumberTA.Views;
namespace RoomNumberTA.Commands
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Cmd : IExternalCommand
    {
        public static ExternalEvent ExEvent { get; set; }
        public static RevitEventHandler Handler { get; set; }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Handler = new RevitEventHandler();
            Handler.App = commandData.Application;
            ExEvent = ExternalEvent.Create(Handler);

            Data.UiApp = commandData.Application;
            Data.UiDocument = Data.UiApp.ActiveUIDocument;
            Data.App = Data.UiApp.Application;
            Data.Doc = Data.UiDocument.Document;

            Data.Doc.GetElement(new ElementId(263692));

            List<IParameter> parameters = new List<IParameter>();
            parameters.Add(new StringParameter("Room_Name"));

            MainWindow mainWindow = new MainWindow(parameters, new PipeDuctSelector(), new ElemSelector());
            mainWindow.Show();

            return Result.Succeeded;

        }
    }
}
