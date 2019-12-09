using MediaWorld.Domain.Abstracts;
using MediaWorld.Domain.Interfaces;

namespace MediaWorld.Domain.Factories
{
   public class AudioFactory : IFactory
   {
      public AMedia Create<T>() where T : AMedia, new()
      {
         return new T();
      }
   }
}