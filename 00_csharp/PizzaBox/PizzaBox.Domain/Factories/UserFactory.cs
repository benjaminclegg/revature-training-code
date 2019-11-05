using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Interfaces;

namespace PizzaBox.Domain.Factories
{
   public class UserFactory : IUserFactory
   {
      public AUser Create<T>() where T : AUser, new()
      {
         return new T();
      }
   }
}