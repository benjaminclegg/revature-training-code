using System;
using MediaWorld.Domain.Abstracts;
using MediaWorld.Domain.Interfaces;
using static MediaWorld.Domain.Delegates.ControlDelegate;

namespace MediaWorld.Domain.MediaPlayerSingleton
{
   public class MediaPlayerSingleton : IPlayer
   {
      private static readonly MediaPlayerSingleton _instance = new MediaPlayerSingleton();
      
      public static MediaPlayerSingleton Instance
      {
         get
         {
            return _instance;
         }
      }

      private MediaPlayerSingleton() {}
      public void Execute(ButtonDelegate button, AMedia media)
      {
         media.ResultEvent += ResultHandler;
         button();
      }

      public void ResultHandler(AMedia media)
      {
         System.Console.WriteLine("{0} media is playing...", media.Title);
      }

      bool IVolume.VolumeUp()
      {
         return true;
      }

      bool IVolume.VolumeDown()
      {
         return true;
      }

      bool IVolume.VolumeMute()
      {
         return true;
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