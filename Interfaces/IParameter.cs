using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomNumberTA.Interfaces
{
    public interface IParameter
    {
        string Name { get; }

        int Copy(Document doc, int sourceElementId, IEnumerable<int> targetElementIds);
    }
}
