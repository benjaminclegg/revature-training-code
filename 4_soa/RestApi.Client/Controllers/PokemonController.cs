using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using RestApi.Client.Models;

namespace RestApi.Client.Controllers
{
   public class PokemonController : ControllerBase
   {
      private static readonly List<Pokemon> pokemons = new List<Pokemon>()
      {
         new Pokemon() { Name = "bulbasaur" },
         new Pokemon() { Name = "ivysaur" },
         new Pokemon() { Name = "venusaur" }
      };

      public IEnumerable<Pokemon> Get()
      {
         return pokemons;
      }

      public Pokemon Get(int id)
      {
         return pokemons[id - 1];
      }
   }
}