using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
   public class Store : AStore
   {
      public List<Order> Orders { get; private set; }
      public decimal Sales
      {
         get
         {
            decimal sum = 0;

            foreach(var item in Orders)
            {
               sum += item.TotalCost;
            }

            return sum;
         }
      }
      public Store()
      {
         Orders = new List<Order>();
      }
   }
}