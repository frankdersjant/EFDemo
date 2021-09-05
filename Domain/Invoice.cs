using System;

namespace Domain
{
    public class Invoice
    {
        public int InvoiceID { get; set; }

        public int CustomerID { get; set; }

        public DateTime InvoiceDate { get; set; } 
        public virtual Customer customer { get; set; }

    }
}
