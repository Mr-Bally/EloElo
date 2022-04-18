using EloElo;
using EloElo.Enums;
using EloElo.Exceptions;
using Xunit;

namespace EloEloTests
{
    public class EloFactoryTests
    {
        [Fact]
        public void GetRatingSystem_ThrowsException_ForInvalidInput()
        {
            Assert.Throws<InvalidRatingException>(() =>
                EloRatingFactory.GetRatingSystem(RatingSystemVariation.EloWithoutKRating, decimal.Zero, decimal.MaxValue));
        }
    }
}