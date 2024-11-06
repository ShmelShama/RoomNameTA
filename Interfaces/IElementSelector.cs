using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomNumberTA.Interfaces
{
    public interface IElementSelector
    {
        IElem SelectElement(Document doc, UIDocument uIDocument);
    }
}
