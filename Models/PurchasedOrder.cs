using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyWebSite.Models
{
    public class PurchasedOrder
    {
        [Key]
        public int OrderId { get; set; }
        public List<NailItem> NailsOrdered { get; set; } = new List<NailItem>();
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string EmailAddress { get; set; } = "";
        public string PhoneNumber { get;set; } = "";
        public string Address { get;set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";
        public string PostalCode { get; set; } = "";
        public string TotalPrice { get; set; } = "";
        public DateTime PurchasedDate { get; set; } = DateTime.Now;
    }
}
