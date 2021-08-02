using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishBowl_PDM_BOM_Import_Addin_Official_
{
    public class Laminate
    {
        public Laminate(string type)
        {
            this.Type = type;
        }

        public int Quantity { get; set; }
        public string Type { get; set; }
    }
}
