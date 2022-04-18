using EloElo.Common;
using EloElo.Enums;

namespace EloElo.RatingSystems
{
    internal class EloWithoutKRating : EloRatingBaseClass
    {
        public EloWithoutKRating(decimal participantOneRating, decimal participantTwoRating) : base(participantOneRating, participantTwoRating)
        {
        }

        public override decimal GetExpectedScore(Participant participant)
        {
            return GetEloExpectedScore(participant);
        }

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

            var newRating = currentRating.Rating + (expectedScore - resultValue);

            return GetResult(newRating, participant);
        }
    }
}
