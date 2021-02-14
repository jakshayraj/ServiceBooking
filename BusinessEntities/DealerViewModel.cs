using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class DealerViewModel
    {
        public int DealerId { get; set; }
        public string Name { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int Zipcode { get; set; }
        public string EmailId { get; set; }
        public string ContactNo { get; set; }
        public virtual ICollection<MechanicViewModel> Mechanics { get; set; }
    }
}
