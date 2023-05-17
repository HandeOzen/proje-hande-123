
using FuelAutomation.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Sales
    {
        public int Id { get; set; }
        public int TanksId { get; set; }
        public Tanks Tanks { get; set; } //hangi tank
        public DateTimeOffset    CreatedOn { get; set; } //tarih
        public string CarPlate { get; set; } //plaka

        public string Quantity { get; set; }
        public string Price { get; set; }
        public string UserId { get; set; }
     //   public A MyProperty { get; set; }
         public Users User { get; set; }

    }
}
