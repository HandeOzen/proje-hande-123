using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Tanks
    {
        public int Id { get; set; }
        public double Quantity { get; set; }
        public double Capacity { get; set; }
        public int FuelTypesId { get; set; }
        public FuelTypes FuelTypes { get; set; }
    }
}
