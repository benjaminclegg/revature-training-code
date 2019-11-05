using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;

namespace PizzaBox.Storing.Repositories
{
   public class StoreRepository
   {
      private List<Store> _storeRepository;

      public List<Store> StoreLibrary
      {
         get
         {
            return _storeRepository;
         }
      }

      public StoreRepository()
      {
         Initialize();
      }

      private List<Store> Initialize()
      {
         if(_storeRepository == null)
         {
            _storeRepository = new List<Store>();
         }

         return _storeRepository;
      }

      public void Persist(Store store)
      {
         _storeRepository.Add(store);
      }

      public Store RetrieveStoreByID(int storeID)
      {
         foreach(Store store in this._storeRepository)
         {
            if(store.StoreID == storeID)
            {
               return store;
            }
         }
         return null;
      }
   }
}