using System.Collections.Generic;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{

   public abstract class AStore
   {
      private int storeID;
      private string address;
      
      public int StoreID
      {
         get => storeID;
         set => storeID = value;
      }

      public string Address
      {
         get => address;
         set => address = value;
      }
   }
}