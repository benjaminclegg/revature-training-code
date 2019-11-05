using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Domain.Interfaces
{
   public interface IUserFactory
   {
      AUser Create<T>() where T : AUser, new();
   }
}