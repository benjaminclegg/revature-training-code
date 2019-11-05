using System.Collections.Generic;
using PizzaBox.Domain.Models;

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
         }

         return _orderRepository;
      }

      public void Persist(Order order)
      {
         _orderRepository.Add(order);
      }
   }
}