using System;
using System.Collections.Generic;

namespace Domain
{
    public class Invoice
    {
        public int InvoiceID { get; set; }

        public int CustomerID { get; set; }

        public DateTime InvoiceDate { get; set; } 
        public virtual Customer customer { get; set; }

        public virtual ICollection<InvoiceLine> InvoiceLines { get; set; }
    }
}
