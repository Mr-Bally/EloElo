using EloElo.Enums;
using EloElo.Exceptions;
using EloElo.Interfaces;
using EloElo.RatingSystems;

namespace EloElo
{
    public class EloRatingFactory
    {
        private const decimal _minRating = 100;

        public IRatingSystem GetRatingSystem(RatingSystemVariation variation, decimal participantOneRating, decimal participantTwoRating)
        {
            CheckRating(participantOneRating, true);
            CheckRating(participantTwoRating, false);

            return new EloWithoutKRating(participantOneRating, participantTwoRating);
        }

        private bool CheckRating(decimal rating, bool isParticipantOne)
        {
            if (rating < _minRating)
            {
                throw new InvalidRatingException(GenerateExceptionMessage(rating, isParticipantOne));
            }

            return true;
        }

        private string GenerateExceptionMessage(decimal rating, bool isParticipantOne)
        {
            return isParticipantOne ? $"ParticipantOne has an invalid rating of { rating}"
                : $"ParticipantTwo has an invalid rating of { rating}";
        }
    }
}