using System;
using MediaWorld.Domain.Abstracts;
using MediaWorld.Domain.Interfaces;

namespace MediaWorld.Domain.MediaPlayerSingleton
{
   public class AudioPlayerSingleton : IPlayer
   {
      private static readonly AudioPlayerSingleton _instance = new AudioPlayerSingleton();
      
      public static AudioPlayerSingleton Instance
      {
         get
         {
            return _instance;
         }
      }

      private AudioPlayerSingleton() {}
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