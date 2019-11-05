using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Interfaces
{
   public interface IStoreFactory
   {
      Store Create();
   }
}