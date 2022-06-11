using EloElo;
using EloElo.Enums;
using EloElo.Exceptions;
using EloElo.Interfaces;
using Xunit;

namespace EloEloTests
{
    public class EloFactoryTests
    {
        [Fact]
        public void GetRatingSystem_ThrowsException_ForInvalidInput()
        {
            Assert.Throws<InvalidRatingException>(() =>
                EloRatingFactory.GetRatingSystem(RatingSystemVariation.EloWithNoviceKRating, decimal.Zero, decimal.MaxValue));
        }

        [Fact]
        public void GetRatingSystem_ReturnsRatingSystem_ForValidInput()
        {
            var rating = 1000;
            var ratingSystem = EloRatingFactory.GetRatingSystem(RatingSystemVariation.EloWithNoviceKRating, rating, rating);

            Assert.True(ratingSystem is IRatingSystem);
        }
    }
}