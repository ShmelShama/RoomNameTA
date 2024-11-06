using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RoomNumberTA.Common;
using RoomNumberTA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace RoomNumberTA.Models
{
    public class ElemSelector : IElementSelector
    {
        public IElem SelectElement(Document doc, UIDocument uIDocument)
        {
            Reference reference;
            try
            {
                reference = Data.UiDocument.Selection.PickObject(ObjectType.Element);
            }
            catch
            {
                return null;
            }
            Element element = doc.GetElement(reference.ElementId);
            if (element == null)
                return null;
            return new Elem(element.Id.IntegerValue, element.Name);
        }
        
    }
}
