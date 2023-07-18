using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace MyWebSite.Models
{
    /// <summary>
    /// Only Used for cookies, this is not required for database
    /// </summary>
    public class ShoppingCart
    {
        public float TotalPrice { get; set; }
        public bool FreeShipping { get;set; }
        public float ShippingCost { get; set; }
        public List<NailItem> NailItems { get; set; }

        public ShoppingCart() { 
          NailItems = new List<NailItem>();
        }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }

    public class NailItem
    {
        [Key]
        public int ProductId { get; set; }
        public float CustomerWidth { get; set; }
        public float CustomerLength { get; set; }
        public string PressOnLength { get; set; } = "";
        public string PressOnStyle { get; set; } = "";
        public int Sets { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

        public bool Equals(NailItem nailItem)
        {
            if(nailItem == null) return false;
            if (this == null) return false;

            if(nailItem.ProductId != ProductId) return false;
            if(this.CustomerWidth != nailItem.CustomerWidth) return false;
            if (this.CustomerLength != nailItem.CustomerLength) return false;
            if (this.PressOnLength.ToLower() != nailItem.PressOnLength.ToLower()) return false;
            if (this.PressOnStyle.ToLower() != nailItem.PressOnStyle.ToLower()) return false;
            if (this.Sets != nailItem.Sets) return false;

            return true;
        }
    }
}
