using EloElo.Common;
using EloElo.Enums;

namespace EloElo.RatingSystems
{
    internal class EloRatingService : EloRatingBaseClass
    {
        private readonly RatingSystemVariation _ratingSystemVariation;

        public EloRatingService(decimal participantOneRating, decimal participantTwoRating, RatingSystemVariation ratingSystemVariation)
            : base(participantOneRating, participantTwoRating)
        {
            _ratingSystemVariation = ratingSystemVariation;
        }

        public override decimal GetExpectedScore(Participant participant) => GetEloExpectedScore(participant);

        public override RatingResult GetResultRating(ResultType result)
        {
            var participantOneRating = GetParticipantRating(Participant.ParticipantOne, result);
            var participantTwoRating = GetParticipantRating(Participant.ParticipantTwo, result);

            return new RatingResult(participantOneRating, participantTwoRating);
        }

        private RatingResultParticipant GetParticipantRating(Participant participant, ResultType result)
        {
            var expectedScore = GetExpectedScore(participant);
            var resultValue = GetResultValue(result, participant);
            var currentRating = GetParticipantRating(participant);
            var kValue = GetKValue();

            var newRating = currentRating.Rating + (kValue * (resultValue - expectedScore));

            return GetResult(newRating, participant);
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
    }
}
