namespace MyWebSite.Models
{
    public class Checkout
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string Email { get; set; } = "";
        public string ShipAddress { get; set; } = "";
        public string ShipCity { get; set; } = "";
        public string ShipState { get; set; } = "";
        public string ShipZipCode { get; set; } = "";
        public string Notes { get; set; } = "";
    }
}
