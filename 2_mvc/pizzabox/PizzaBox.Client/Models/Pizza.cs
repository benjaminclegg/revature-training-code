using System.Collections.Generic;

namespace PizzaBox.Client.Models
{
   public class Pizza
   {


      public string Crust { get; set; }
      public string Size { get; set; }

      [Range]
      public int Quantity { get; set; }

      [NameAttribute(ErrorMessage = "The name must be alpha letters.")]
      [StringLength(50)]
      public string Name { get; set; }
      public List<string> Crusts { get; set; }
      public List<string> Sizes { get; set; }

      public Pizza()
      {
         Crusts = new List<string> { "Thin", "Handtossed", "Pan"};
         Sizes = new List<string> {"Small", "Medium", "Large"};
      }
   }
}