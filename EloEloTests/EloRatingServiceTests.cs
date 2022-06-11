using EloElo;
using EloElo.Enums;
using EloElo.Interfaces;
using Xunit;


namespace EloEloTests
{
    public class EloRatingServiceTests
    {
        [Fact]
        public void EloWithKRating_ReturnsCorrectValue_WhenParticipantOneWins()
        {
            var partOneRating = 1200;
            var partTwoRating = 1400;
            var expectedOneRating = 1230.4m;
            var expectedTwoRating = 1369.6m;
            var expectedChange = 30.4m;

            var ratingSystem = GetEloRatingSystem(RatingSystemVariation.EloWithNoviceKRating, partOneRating, partTwoRating);
            var result = ratingSystem.GetResultRating(ResultType.ParticipantOneWins);

            Assert.Equal(expectedOneRating, result.ParticipantOne.NewRating, 1);
            Assert.Equal(expectedTwoRating, result.ParticipantTwo.NewRating, 1);
            Assert.Equal(expectedChange, result.ParticipantOne.RatingChange, 1);
        }

        private IRatingSystem GetEloRatingSystem(RatingSystemVariation ratingSystemVariation, decimal ratingOne, decimal ratingTwo)
            => EloRatingFactory.GetRatingSystem(ratingSystemVariation, ratingOne, ratingTwo);
    }
}
