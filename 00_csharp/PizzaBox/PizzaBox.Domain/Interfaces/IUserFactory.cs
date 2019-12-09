using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Interfaces
{
   public interface IUserFactory
   {
      User Create();
   }
}