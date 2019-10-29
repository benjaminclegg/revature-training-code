using System;
using MediaWorld.Domain.Abstracts;

namespace MediaWorld.Domain.MediaPlayerSingleton
{
   public class AudioPlayerSingleton
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
   }
}