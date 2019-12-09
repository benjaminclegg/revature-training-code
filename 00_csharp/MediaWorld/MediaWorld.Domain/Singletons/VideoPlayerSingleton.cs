using System;
using MediaWorld.Domain.Abstracts;
using MediaWorld.Domain.Interfaces;

namespace MediaWorld.Domain.VideoPlayerSingleton
{
   public class VideoPlayerSingleton : IPlayer
   {
      private static readonly VideoPlayerSingleton _instance = new VideoPlayerSingleton();
      
      public static VideoPlayerSingleton Instance
      {
         get
         {
            return _instance;
         }
      }

      private VideoPlayerSingleton() {}
      public void Execute(string command, AMedia media)
      {
         Console.WriteLine(media);
      }

      public bool VolumeUp()
      {
         throw new NotImplementedException();
      }

      public bool VolumeDown()
      {
         throw new NotImplementedException();
      }

      public bool VolumeMute()
      {
         throw new NotImplementedException();
      }

      public bool PowerUp()
      {
         throw new NotImplementedException();
      }

      public bool PowerDown()
      {
         throw new NotImplementedException();
      }
   }
}