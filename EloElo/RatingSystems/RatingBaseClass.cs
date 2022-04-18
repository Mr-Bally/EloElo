using EloElo.Common;
using EloElo.Enums;
using EloElo.Interfaces;

namespace EloElo.RatingSystems
{
    internal abstract class RatingBaseClass : IRatingSystem
    {
        protected const decimal _minimumRating = 100;

        protected decimal _participantOneCurrentRating;
        protected decimal _participantTwoCurrentRating;

        public abstract decimal GetExpectedScore(Participant participant);
        public abstract RatingResult GetResultRating(ResultType result);

        public RatingBaseClass(decimal participantOneRating, decimal participantTwoRating)
        {
            _participantOneCurrentRating = participantOneRating;
            _participantTwoCurrentRating = participantTwoRating;
        }

        public ParticipantRating GetParticipantRating(Participant participant)
        {
            var rating = participant == Participant.ParticipantOne ? _participantOneCurrentRating : _participantTwoCurrentRating;

            return new ParticipantRating(participant, rating);
        }

        protected RatingResultParticipant GetResult(decimal rating, Participant participant)
        {
            var validRating = rating < _minimumRating ? _minimumRating : rating;
            var isParticipantOne = participant == Participant.ParticipantOne;
            var oldRating = _minimumRating;

            if (isParticipantOne)
            {
                oldRating = _participantOneCurrentRating;
                _participantOneCurrentRating = validRating;
            }
            else
            {
                oldRating = _participantTwoCurrentRating;
                _participantTwoCurrentRating = validRating;
            }

            return new RatingResultParticipant(participant, validRating, oldRating);
        }
    }
}
