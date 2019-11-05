using System.Collections.Generic;
using PizzaBox.Domain.Models;

namespace PizzaBox.Storing.Repositories
{
   public class PizzaRepository
   {
      private List<Pizza> _pizzaRepository;

      public List<Pizza> PizzaLibrary
      {
         get
         {
            return _pizzaRepository;
         }
      }

      public PizzaRepository()
      {
         Initialize();
      }

      private List<Pizza> Initialize()
      {
         if(_pizzaRepository == null)
         {
            _pizzaRepository = new List<Pizza>();
         }

         return _pizzaRepository;
      }
   }
}