using Autodesk.Revit.DB;
using RoomNumberTA.Common;
using RoomNumberTA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomNumberTA.Models
{
    public class StringParameter: IParameter
    {
        public string Name { get; set; }
        public StringParameter(string name)
            { Name = name; }
        public int Copy(Document doc, int sourceElementId, IEnumerable<int> targetElementIds)
        {
            int i = 0;
            var sourceElement = Data.Doc.GetElement(new ElementId(sourceElementId));
            if (targetElementIds == null|| sourceElement ==null)
            {
                return -1;
            }
            var sourceParameter = sourceElement.LookupParameter(Name)?.AsString();
            if(sourceParameter == null) 
            { 
                return -1; 
            }
            foreach (var targetElementId in targetElementIds) 
            {
                var targetElement = Data.Doc.GetElement(new ElementId(targetElementId));
                if (targetElement == null)
                {
                    continue;
                }
                var targetParameter = targetElement.LookupParameter(Name);
                if(targetParameter == null)
                {
                    continue;
                }
                if (targetParameter.Set(sourceParameter))
                {
                    i++; 
                }
            }
            return i;
           
        }
    }
}
