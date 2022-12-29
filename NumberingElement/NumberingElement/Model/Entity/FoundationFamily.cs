using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entity
{
    public class FoundationFamily
    {
        public Autodesk.Revit.DB.Element RevitElement { get; set; }
        public List<Model.Entity.Element> Foundations { get; set; } = new List<Model.Entity.Element>();
    }
}
