using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }
        [Required]
        [DisplayName("Customer Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Address 1")]
        public string Address1 { get; set; }
        [Required]
        [DisplayName("Address 2")]
        public string Address2 { get; set; }
        [Required]
        public int Zipcode { get; set; }
        [Required]
        [DisplayName("Phone No")]
        [MinLength(10, ErrorMessage ="Phone no greater than 10 digit")]
        [MaxLength(10, ErrorMessage = "Phone no less than 10 digit")]
        public string PhoneNo { get; set; }
        [MinLength(10, ErrorMessage = "Phone no must be 11 digit")]
        [MaxLength(10, ErrorMessage = "Phone no must be 11 digit")]
        public string HomePhone { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }
        [Required]
        public string Password { get; set; }
        public IEnumerable<CustomerViewModel> customerlist { get; set; }
    }
}
