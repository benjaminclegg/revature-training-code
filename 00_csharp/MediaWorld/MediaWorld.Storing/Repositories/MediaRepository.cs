using System.Collections.Generic;
using MediaWorld.Domain.Abstracts;
using MediaWorld.Domain.Factories;
using MediaWorld.Domain.Models;

namespace MediaWorld.Storing.Repositories
{
   public class MediaRepository
   {

      private List<AMedia> _mediaRepository;

      public List<AMedia> MediaLibrary
      {
         get
         {
            return _mediaRepository;
         }
      }
      public MediaRepository()
      {
         Initialize();
      }

      private List<AMedia> Initialize()
      {
         var audioFactory = new AudioFactory();
         var videoFactory = new VideoFactory();

         if(_mediaRepository == null)
         {
            _mediaRepository = new List<AMedia>();

            _mediaRepository.AddRange(new AMedia[]
            {
               audioFactory.Create<Book>(),
               audioFactory.Create<Song>(),
               videoFactory.Create<Movie>(),
               videoFactory.Create<Photo>()
            });
         }

         return _mediaRepository;
      }
   }
}