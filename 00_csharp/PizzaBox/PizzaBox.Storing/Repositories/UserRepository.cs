using System;
using System.Collections.Generic;
using PizzaBox.Domain.Abstracts;

namespace PizzaBox.Storing.Repositories
{
   public class UserRepository
   {
      private List<AUser> _userRepository;

      public List<AUser> UserLibrary
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

      private List<AUser> Initialize()
      {
         if(_userRepository == null)
         {
            _userRepository = new List<AUser>();
         }

         return _userRepository;
      }

      public void Persist(AUser user)
      {
         _userRepository.Add(user);
      }

      public AUser VerifyUserByEmail(string field)
      {
         foreach(AUser user in this.UserLibrary)
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