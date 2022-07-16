using EloElo;
using EloElo.Enums;
using EloElo.Interfaces;
using Xunit;

namespace EloEloTests
{
    public class EloFactoryTests
    {
        [Fact]
        public void GetRatingSystem_ReturnsRatingSystem_ForValidInput()
        {
            var ratingSystem = EloRatingFactory.GetRatingSystem(RatingSystemVariation.EloWithNoviceKRating);

            Assert.True(ratingSystem is IRatingSystem);
        }
    }
}