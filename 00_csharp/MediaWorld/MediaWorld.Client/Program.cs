using System;
using Serilog;
using MediaWorld.Domain.MediaPlayerSingleton;
using MediaWorld.Storing.Repositories;
using MediaWorld.Domain.Abstracts;
using System.Threading;
using System.Threading.Tasks;

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
        private static async Task Main(string[] args)
        {
            (new Program()).ApplicationStart();
            //Play();
            await MagicAsync();

            Console.WriteLine("End of code");
            //Log.Warning("end of Main method");
        }

         private void ApplicationStart()
         {
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel
               .Debug()
               .WriteTo.Console()
               .WriteTo.File("log.txt")
               .CreateLogger();
         }

         private static MediaRepository _repository = new MediaRepository();
         private static void Play()
         {
            Log.Information("Play Method");
            var mediaPlayer = MediaPlayerSingleton.Instance;

            foreach (var item in _repository.MediaLibrary)
            {
               Log.Debug("{item}", item.Title);
               mediaPlayer.Execute(item.Play, item);
            }
         }

         private static void MagicThread()
         {
            var t1 = new Thread( () => { Run("A"); } );
            var t2 = new Thread( () => { Run("B"); } );

            t1.Start();
            t2.Start();

            t1.Join(); // t1 will need to end before moving to next instruction
            t2.Join();
         }

         private static void MagicTask()
            {
            var t1 = new Task( () => { Run("A"); } );
            var t2 = new Task( () => { Run("B"); } );

            t1.Start();
            t2.Start();

            Task.WaitAll(new Task[] {t1, t2}, 1000);
         }

         private static async Task MagicAsync()
         {
            await Task.Run( () => { Run("C"); });
            await Task.Run( () => { Run("D"); });
         }

         private static void Run(string s)
         {
               for (var x = 0; x < 1000; x += 1)
               {
                  Console.Write(s);
               }
         }
    }
}