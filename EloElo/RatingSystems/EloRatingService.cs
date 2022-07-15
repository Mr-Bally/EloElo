using EloElo.Common;
using EloElo.Enums;
using EloElo.Exceptions;
using EloElo.Interfaces;

namespace EloElo.RatingSystems
{
    internal class EloRatingService : IRatingSystem
    {
        private const decimal _drawResultValue = 0.5m;
        private const decimal _winResultValue = 1m;
        private const decimal _loseResultValue = 0;
        private const decimal _minimumRating = 100;

        private readonly RatingSystemVariation _ratingSystemVariation;

        public EloRatingService(RatingSystemVariation ratingSystemVariation)
        {
            _ratingSystemVariation = ratingSystemVariation;
        }

        public (decimal participantOneExpectedScore, decimal participantTwoExpectedScore) GetExpectedScore(decimal participantOneRating, decimal participantTwoRating)
        {
            CheckInput(participantOneRating, participantTwoRating);

            var participantOneExpectedScore = GetEloExpectedScore(Participant.ParticipantOne, participantOneRating, participantTwoRating);
            var participantTwoExpectedScore = GetEloExpectedScore(Participant.ParticipantTwo, participantOneRating, participantTwoRating);

            return (participantOneExpectedScore, participantTwoExpectedScore);
        }

        public RatingResult GetResultRating(decimal participantOneRating, decimal participantTwoRating, ResultType result)
        {
            CheckInput(participantOneRating, participantTwoRating);

            var expectedScore = GetExpectedScore(participantOneRating, participantTwoRating);
            var participantOneResult = GetResultValue(result, Participant.ParticipantOne);
            var participantTwoResult = GetResultValue(result, Participant.ParticipantTwo);
            var kValue = GetKValue();

            var participantOneNewRating = CalculateNewRating(kValue, participantOneRating, participantOneResult, expectedScore.participantOneExpectedScore);
            var participantTwoNewRating = CalculateNewRating(kValue, participantTwoRating, participantTwoResult, expectedScore.participantTwoExpectedScore);

            return GetRatingResult(participantOneNewRating, participantOneRating, participantTwoNewRating, participantTwoRating);
        }

        private void CheckInput(decimal participantOneRating, decimal participantTwoRating)
        {
            CheckRating(participantOneRating, true);
            CheckRating(participantTwoRating, false);
        }

        private decimal CalculateNewRating(decimal kValue, decimal currentRating, decimal resultValue, decimal expectedScore)
        {
            return currentRating + (kValue * (resultValue - expectedScore));
        }

        private RatingResult GetRatingResult(decimal participantOneNewRating,
            decimal participantOneOldRating,
            decimal participantTwoNewRating,
            decimal participantTwoOldRating)
        {
            return new RatingResult(GetResult(participantOneOldRating, participantOneNewRating, Participant.ParticipantOne),
                GetResult(participantTwoOldRating, participantTwoNewRating, Participant.ParticipantTwo));
        }

        private RatingResultParticipant GetResult(decimal oldRating, decimal newRating, Participant participant)
        {
            var validRating = newRating < _minimumRating ? _minimumRating : newRating;

            return new RatingResultParticipant(participant, validRating, oldRating);
        }

        private decimal GetEloExpectedScore(Participant participant, decimal participantOneRating, decimal participantTwoRating)
        {
            var exponent = GetExponentForEloScore(participant, participantOneRating, participantTwoRating);
            var result = 1 / (1 + Math.Pow(10, exponent));

            return (decimal)result;
        }

        private double GetExponentForEloScore(Participant participant,
            decimal participantOneRating,
            decimal participantTwoRating)
        {
            if (participant == Participant.ParticipantOne)
            {
                return (double)(participantTwoRating - participantOneRating) / 400;
            }

            return (double)(participantOneRating - participantTwoRating) / 400;
        }

        private decimal GetResultValue(ResultType result, Participant participant)
        {
            if (result == ResultType.Draw)
            {
                return _drawResultValue;
            }
            else if ((result == ResultType.ParticipantOneWins && participant == Participant.ParticipantOne)
                || (result == ResultType.ParticipantTwoWins && participant == Participant.ParticipantTwo))
            {
                return _winResultValue;
            }

            return _loseResultValue;
        }

        private decimal GetKValue()
        {
            switch (_ratingSystemVariation)
            {
                case RatingSystemVariation.EloWithNoviceKRating:
                    return 40m;
                case RatingSystemVariation.EloWithIntermediateKRating:
                    return 20m;
                case RatingSystemVariation.EloWithMasterKRating:
                    return 10m;
                default:
                    return 40m;
            }
        }

        private static bool CheckRating(decimal rating, bool isParticipantOne)
        {
            if (rating < _minimumRating)
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
