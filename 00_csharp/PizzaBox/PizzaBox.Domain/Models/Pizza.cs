using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
   public class Pizza : APizza
   {
      private decimal price;
      private string type;
      public decimal Price { get; set; }
      public string Type { get; set; }

      public override string ToString()
         {
            return $" - {Size} {Type} pizza with {Crust} crust - ${Price}";
         }
   }
}