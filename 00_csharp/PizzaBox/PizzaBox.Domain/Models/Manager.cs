using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;

namespace PizzaBox.Domain.Models
{
   public class Manager : AUser, IManager
   {
      public Manager() {}
   }
}