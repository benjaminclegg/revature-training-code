using System;
using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;
using PizzaBox.Domain.Models;
using PizzaBox.Storing.Connectors;

namespace PizzaBox.Storing.Repositories
{
   public class UserRepository
   {
      private List<User> _userRepository;

      public List<User> UserLibrary
      {
         get
         {
            return _userRepository;
         }
      }

      public UserRepository()
      {
         Initialize();
      }

      private List<User> Initialize()
      {
         if(_userRepository == null)
         {
            _userRepository = new List<User>();
            _userRepository.AddRange(new FileSystemConnector().UserReadXml());
         }

         return _userRepository;
      }

      public void Persist(User user)
      {
         _userRepository.Add(user);
         Save();
      }

      public void Save()
      {
         var fs = new FileSystemConnector();
         fs.UserWriteXml(_userRepository);
      }

      public User VerifyUserByEmail(string field)
      {
         foreach(User user in this.UserLibrary)
         {
            if(field == user.Email)
            {
               return user;
            }
         }
         return null;
      }
   }
}