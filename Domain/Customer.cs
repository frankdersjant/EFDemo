using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class Customer
    {
        public int CustomerID { get; set; }
        
        [MaxLength(60)]
        public string FirstName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        [NotMapped]
        public int Age { get; set; }
        public virtual ICollection<Invoice> Invoice { get; set; }
    }
}
