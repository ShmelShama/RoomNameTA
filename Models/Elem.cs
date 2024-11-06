using RoomNumberTA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoomNumberTA.Models
{
    public class Elem : IElem
    {
        public int Id { get; set ; }
        public string Name { get ; set; }

        public Elem(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
