using EloElo.Enums;
using EloElo.Exceptions;
using EloElo.Interfaces;
using EloElo.RatingSystems;

namespace EloElo
{
    public static class EloRatingFactory
    {
        private const decimal _minRating = 100;

        public static IRatingSystem GetRatingSystem(RatingSystemVariation variation, decimal participantOneRating, decimal participantTwoRating)
        {
            CheckRating(participantOneRating, true);
            CheckRating(participantTwoRating, false);

            return new EloRatingService(participantOneRating, participantTwoRating, variation);
        }

        private static bool CheckRating(decimal rating, bool isParticipantOne)
        {
            if (rating < _minRating)
            {
                throw new InvalidRatingException(GenerateExceptionMessage(rating, isParticipantOne));
            }

            return true;
        }

        private static string GenerateExceptionMessage(decimal rating, bool isParticipantOne)
        {
            return isParticipantOne ? $"ParticipantOne has an invalid rating of { rating}"
                : $"ParticipantTwo has an invalid rating of { rating}";
        }
    }
}