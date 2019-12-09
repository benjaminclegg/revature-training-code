using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Connectors;

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
            //_storeRepository.AddRange(new FileSystemConnector().StoreReadXml());
         }

         return _storeRepository;
      }

      public void Persist(Store store)
      {
         _storeRepository.Add(store);
         Save();
      }

      public void Save()
      {
         var fs = new FileSystemConnector();
         fs.StoreWriteXml(_storeRepository);
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