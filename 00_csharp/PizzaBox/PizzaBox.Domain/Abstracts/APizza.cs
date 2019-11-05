using System.Collections.Generic;

namespace PizzaBox.Domain.Abstracts
{
   public abstract class APizza
   {
      private string crust;
      private string size;
      public string Crust { get; set; }
      public string Size { get; set; }
   }
}