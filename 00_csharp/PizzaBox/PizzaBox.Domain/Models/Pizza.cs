using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Enums;

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
            return $"{Size} {Type} pizza with {Crust} crust - {Price}";
         }
      /*
      public decimal Price { get; set; }
      public ECrust Crust { get; set; }
      public ESize Size { get; set; }
      public ESauce Sauce { get; set; }
      public ECheese Cheese { get; set; }
      public List<ETopping> Toppings { get; set; }

      public Pizza()
      {
         Toppings = new List<ETopping>();
         SetPrice();
      }

      private void SetPrice()
      {
         switch(Size)
         {
            case ESize.Small:
               Price = 5.99M;
               break;
            case ESize.Medium:
               Price = 9.99M;
               break;
            case ESize.Large:
               Price = 12.99M;
               break;
            default:
               Price = 16.99M;
               break;
         }
      }
      */
   }
}