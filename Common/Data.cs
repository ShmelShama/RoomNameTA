using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomNumberTA.Common
{
    public static class Data
    {
        public static UIApplication UiApp { get; set; }
        public static UIDocument UiDocument { get; set; }
        public static Application App { get; set; }
        public static Document Doc { get; set; }
    }
}
