using EloElo.Enums;

namespace EloElo.RatingSystems
{
    internal abstract class EloRatingBaseClass : RatingBaseClass
    {
        protected const decimal _drawResultValue = 0.5m;
        protected const decimal _winResultValue = 1m;
        protected const decimal _loseResultValue = 0;

        protected EloRatingBaseClass(decimal participantOneRating, decimal participantTwoRating) : base(participantOneRating, participantTwoRating)
        {
        }

        protected decimal GetEloExpectedScore(Participant participant)
        {
            var exponent = GetExponentForEloScore(participant);
            var result = 1 / (1 + Math.Pow(10, exponent));

            return (decimal)result;
        }

        private double GetExponentForEloScore(Participant participant)
        {
            if (participant == Participant.ParticipantOne)
            {
                return (double)(_participantTwoCurrentRating - _participantOneCurrentRating) / 400;
            }

            return (double)(_participantOneCurrentRating - _participantTwoCurrentRating) / 400;
        }

        protected decimal GetResultValue(ResultType result, Participant participant)
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
    }
}
