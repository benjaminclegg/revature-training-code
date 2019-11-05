using System.Collections.Generic;
using PizzaBox.Domain.Interfaces;
using PizzaBox.Domain.Models;

namespace PizzaBox.Domain.Abstracts
{ 
   public abstract class AUser : IUser
   {
      private string userType;
      private string firstName;
      private string lastName;
      private string phone;
      private string address;
      private string email;
      private int storeID;
      private List<Order> orders;


      public string UserType
      {
         get => userType;
         set => userType = value;
      }

      public string FirstName
      {
         get => firstName;
         set => firstName = value;
      }

      public string LastName
      {
         get => lastName;
         set => lastName = value;
      }

      public string Phone
      {
         get => phone;
         set => phone = value;
      }
      
      public string Address
      {
         get => address;
         set => address = value;
      }

      public string Email
      {
         get => email;
         set => email = value;
      }

      public int StoreID
      {
         get => storeID;
         set => storeID = value;
      }

      public List<Order> Orders { get; set; }

      public AUser()
      {
         Orders = new List<Order>();
      }

      public override string ToString()
      {
         return $"{UserType} {FirstName} {LastName}";
      }
   }
}