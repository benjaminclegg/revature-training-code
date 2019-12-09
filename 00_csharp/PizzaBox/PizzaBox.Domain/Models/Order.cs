using System;
using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Models
{
   public class Order : AOrder
   {
      private List<Pizza> pizzas = new List<Pizza>();
      private int storeID;
      private string userEmail;
      private decimal total;
      
      public List<Pizza> Pizzas { get => pizzas; set => pizzas = value; }
      public int StoreId { get; set; }
      public string User { get; set; }

      public decimal TotalCost 
      {
         get
         {
            decimal sum = 0;

            foreach (var item in Pizzas)
            {
               sum += item.Price;
            }
            return sum;
         }
      }

      public DateTime OrderDate { get; set; }

      public Order()
      {
         OrderDate = DateTime.Now;
      }

      public bool isEmpty()
      {
         foreach(var pizza in Pizzas)
         {
            if(pizza != null)
            {
               return false;
            }
         }
         return true;
      }

      public void UpdateDate()
      {
         OrderDate = DateTime.Now;
      }

      public override string ToString()
      {
         return $" - Order ID: {Id} Order Date: {OrderDate} from Store #{StoreId}";
      }
   }
}