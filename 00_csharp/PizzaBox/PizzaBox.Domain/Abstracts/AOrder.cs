using System;

namespace PizzaBox.Domain.Abstracts
{
   public abstract class AOrder
   {
      public long Id { get; set; }

      public AOrder()
      {
         Id = DateTime.Now.Ticks;
      }

      public void UpdateID()
      {
         Id = DateTime.Now.Ticks;
      }
   }
}