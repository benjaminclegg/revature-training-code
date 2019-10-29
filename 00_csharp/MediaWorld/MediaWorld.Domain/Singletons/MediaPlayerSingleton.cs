using System;
using MediaWorld.Domain.Interfaces;
using MediaWorld.Domain.Abstracts;

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
      public void Execute(string command, AMedia media)
      {
         Console.WriteLine(media);
      }
   }
}