using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RoomNumberTA.Common;
using RoomNumberTA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomNumberTA.Models
{
    public class PipeDuctSelector : IElementsSelector
    {
        public IEnumerable<IElem> SelectElements(Document doc,UIDocument uIDocument)
        {
            var selIds = Data.UiDocument.Selection.GetElementIds();
            IEnumerable<Reference> references;
            if (!selIds.Any())
            {
                try
                {
                    references = Data.UiDocument.Selection.PickObjects(ObjectType.Element);
                }
                catch
                {
                    return new List<Elem>();
                }
                selIds = references.Select(r => r.ElementId).ToList();
            }
            List<Elem> elems= new List<Elem>();
            foreach (ElementId selId in selIds)
            {
                var element = doc.GetElement(selId);
                if (element == null)
                    continue;
                if(element is Duct || element is Pipe)
                    elems.Add(new Elem(element.Id.IntegerValue, element.Name));
            }
            return elems;
        }
    }
}
