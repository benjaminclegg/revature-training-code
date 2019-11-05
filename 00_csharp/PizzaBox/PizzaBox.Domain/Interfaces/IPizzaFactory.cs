using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Interfaces
{
   public interface IPizzaFactory
   {
      Pizza Create();
   }
}