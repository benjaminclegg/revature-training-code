using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Connectors;

namespace PizzaBox.Storing.Repositories
{
   public class OrderRepository
   {
      private List<Order> _orderRepository;

      public List<Order> OrderLibrary
      {
         get
         {
            return _orderRepository;
         }
      }

      public OrderRepository()
      {
         Initialize();
      }

      private List<Order> Initialize()
      {
         if(_orderRepository == null)
         {
            _orderRepository = new List<Order>();
            _orderRepository.AddRange(new FileSystemConnector().OrderReadXml());
         }

         return _orderRepository;
      }

      public void Persist(Order order)
      {
         _orderRepository.Add(order);
         Save();
      }

      public void Save()
      {
         var fs = new FileSystemConnector();
         fs.OrderWriteXml(_orderRepository);
      }

      public List<Order> RetrieveOrderByStoreID(int storeId)
      {
         List<Order> list = new List<Order>();
         foreach(Order order in this.OrderLibrary)
         {
            if(order.StoreId == storeId)
            {
               list.Add(order);
            }
         }
         return list;
      }

      public decimal TotalSalesByLocation(List<Order> list)
      {
         decimal sum = 0;
         foreach(Order order in list)
         {
            sum += order.TotalCost;
         }
         return sum;
      }

      public List<Order> BuildListByUser(AUser user)
      {
         List<Order> ordersByUser = new List<Order>();
         foreach(Order order in this.OrderLibrary)
         {
            if(order.User == user.Email)
            {
               ordersByUser.Add(order);
            }
         }
         return ordersByUser;
      }
   }
}