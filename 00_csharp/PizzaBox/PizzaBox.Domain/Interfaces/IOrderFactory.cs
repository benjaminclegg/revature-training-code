using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Interfaces
{
   public interface IOrderFactory
   {
      Order Create();
   }
}