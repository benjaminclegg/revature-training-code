using System;
using MediaWorld.Domain.Abstracts;

namespace MediaWorld.Domain.VideoPlayerSingleton
{
   public class VideoPlayerSingleton
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
   }
}