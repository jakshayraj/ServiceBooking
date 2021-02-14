using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class VehicleViewModel
    {
        public int VehicleId { get; set; }
        [Required]
        [DisplayName("Licence Plate No")]
        public string LicencePlate { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public Nullable<System.DateTime> RegistraionDate { get; set; }
        [Required]
        [MinLength(17, ErrorMessage = "Phone no greater than 17 digit")]
        [MaxLength(17, ErrorMessage = "Phone no less than 17 digit")]
        public string ChassiNo { get; set; }
        [Required]
        public Nullable<int> CustomerId { get; set; }
        public virtual CustomerViewModel Customer { get; set; }
        public IEnumerable<VehicleViewModel> vehiclelist { get; set; }
    }
}
