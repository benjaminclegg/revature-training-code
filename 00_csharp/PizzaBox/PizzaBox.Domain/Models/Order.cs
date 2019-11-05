using System;
using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Enums;

namespace PizzaBox.Domain.Models
{
   public class Order : AOrder
   {
      private List<Pizza> pizzas = new List<Pizza>();
      
      public List<Pizza> Pizzas { get => pizzas; set => pizzas = value; }

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
      public DateTime OrderDate { get; }

      public Order()
      {
         OrderDate = DateTime.Now;
      }

      public bool isEmpty()
      {
         if(Pizzas == null)
         {
            return true;
         }
         else { return false; }
      }
   }
}

/*
      public List<Pizza> Pizzas { get; }

      public DateTime OrderDate { get; }
      public Order()
      {
         //Pizzas = new List<Pizzas>();
         OrderDate = DateTime.Now;
      }

      }
      */