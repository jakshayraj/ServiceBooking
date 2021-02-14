using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class ServiceViewModel
    {
        public int ServiceId { get; set; }
        [Required]
        [DisplayName("Service Name")]
        public string ServiceName { get; set; }
        [Required]
        public Nullable<decimal> Price { get; set; }
        [Required(ErrorMessage = ("Duration required like 1 Hour, 1 Day"))]
        public string Duration { get; set; }
        [Required]
        public Nullable<bool> Active { get; set; }
        public IEnumerable<ServiceViewModel> servicelist { get; set; }
    }
}
