using System;
using System.ComponentModel.DataValidations;
using Regex;

namespace PizzaBox.Client.Validations
{
   public class NameAttribute : ValidationAttribute
   {
      public override bool IsValid(object o)
      {
         var s = o as string;
         var regex = new Regex("[a-zA-Z]+");

         if (string.IsNullOrWhiteSpace(s))
         {
            return false;
         }

         if(!regex.IsMatch(s))
         {
            return false;
         }

         return true;
      }
   }
}