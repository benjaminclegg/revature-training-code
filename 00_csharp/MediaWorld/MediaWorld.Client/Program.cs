using System;
using MediaWorld.Domain.Abstracts;
using MediaWorld.Domain.Models;
using MediaWorld.Domain.MediaPlayerSingleton;

namespace MediaWorld.Client
{
   /// <summary>
   /// Contains the start point
   /// </summary>
    internal class Program
    {
       /// <summary>
       /// Starts the application
       /// </summary>
        private static void Main(string[] args)
        {
            Play();
        }

         private static void Play()
         {
            var mediaPlayer = MediaPlayerSingleton.Instance;
            AMedia song = new Song();
            AMedia movie = new Movie();

            mediaPlayer.Execute("play", song);
            mediaPlayer.Execute("play", movie);
         }
    }
}