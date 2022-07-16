using EloElo;
using EloElo.Enums;
using EloElo.Exceptions;
using EloElo.Interfaces;
using Xunit;


namespace EloEloTests
{
    public class EloRatingServiceTests
    {
        [Fact]
        public void GetExpectedResultThrowException_ForInvalidRatings()
        {
            var ratingSystem = GetEloRatingSystem(RatingSystemVariation.EloWithNoviceKRating);

            Assert.Throws<InvalidRatingException>(() =>
                ratingSystem.GetExpectedScore(decimal.MinValue, decimal.MaxValue));
        }

        [Fact]
        public void EloWithNoviceKRating_ReturnsCorrectValue_WhenParticipantOneWins()
        {
            var partOneRating = 1200;
            var partTwoRating = 1400;
            var expectedOneRating = 1230.4m;
            var expectedTwoRating = 1369.6m;
            var expectedChange = 30.4m;

            var ratingSystem = GetEloRatingSystem(RatingSystemVariation.EloWithNoviceKRating);
            var result = ratingSystem.GetResultRating(partOneRating, partTwoRating, ResultType.ParticipantOneWins);

            Assert.Equal(expectedOneRating, result.ParticipantOne.NewRating, 1);
            Assert.Equal(expectedTwoRating, result.ParticipantTwo.NewRating, 1);
            Assert.Equal(expectedChange, result.ParticipantOne.RatingChange, 1);
        }

        [Fact]
        public void EloWithNoviceKRating_ReturnsCorrectValue_WhenParticipantTwoWins()
        {
            var partOneRating = 1200;
            var partTwoRating = 1400;
            var expectedOneRating = 1190.4m;
            var expectedTwoRating = 1409.6m;
            var expectedChange = -9.6m;

            var ratingSystem = GetEloRatingSystem(RatingSystemVariation.EloWithNoviceKRating);
            var result = ratingSystem.GetResultRating(partOneRating, partTwoRating, ResultType.ParticipantTwoWins);

            Assert.Equal(expectedOneRating, result.ParticipantOne.NewRating, 1);
            Assert.Equal(expectedTwoRating, result.ParticipantTwo.NewRating, 1);
            Assert.Equal(expectedChange, result.ParticipantOne.RatingChange, 1);
        }

        [Fact]
        public void EloWithIntermediateKRating_ReturnsCorrectValue_ForDraw()
        {
            var partOneRating = 1000m;
            var partTwoRating = 1100m;
            var expectedOneRating = 1002.8m;
            var expectedTwoRating = 1097.2m;
            var expectedChange = 2.8m;

            var ratingSystem = GetEloRatingSystem(RatingSystemVariation.EloWithIntermediateKRating);
            var result = ratingSystem.GetResultRating(partOneRating, partTwoRating, ResultType.Draw);

            Assert.Equal(expectedOneRating, result.ParticipantOne.NewRating, 1);
            Assert.Equal(expectedTwoRating, result.ParticipantTwo.NewRating, 1);
            Assert.Equal(expectedChange, result.ParticipantOne.RatingChange, 1);
        }

        [Fact]
        public void EloWithMasterKRating_ReturnsCorrectValue_ForPlayerTwoWin()
        {
            var partOneRating = 1900m;
            var partTwoRating = 1850m;
            var expectedOneRating = 1894.3m;
            var expectedTwoRating = 1855.7m;
            var expectedChange = -5.7m;

            var ratingSystem = GetEloRatingSystem(RatingSystemVariation.EloWithMasterKRating);
            var result = ratingSystem.GetResultRating(partOneRating, partTwoRating, ResultType.ParticipantTwoWins);

            Assert.Equal(expectedOneRating, result.ParticipantOne.NewRating, 1);
            Assert.Equal(expectedTwoRating, result.ParticipantTwo.NewRating, 1);
            Assert.Equal(expectedChange, result.ParticipantOne.RatingChange, 1);
        }

        private IRatingSystem GetEloRatingSystem(RatingSystemVariation ratingSystemVariation)
            => EloRatingFactory.GetRatingSystem(ratingSystemVariation);
    }
}
