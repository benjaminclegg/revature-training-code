using System.Linq;
using RestApi.Client.Controllers;
using Xunit;

namespace RestApi.Testing
{
   public class PokemonUnitTest
   {
      [Fact]
      public void Test_GetAllPokemon()
      {
         var sut = new PokemonController();
         var actual = sut.Get();

         Assert.True(actual.Count() > 0);
      }

      [Theory]
      [InlineData(1)]
      [InlineData(2)]
      [InlineData(3)]
      public void Test_GetPokemon(int id)
      {
         var sut = new PokemonController();
         var actual = sut.Get(id);

         Assert.False(string.IsNullOrWhiteSpace(actual.Name));
      }
   }
}