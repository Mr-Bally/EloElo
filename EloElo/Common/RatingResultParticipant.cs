using EloElo.Enums;

namespace EloElo.Common
{
    public class RatingResultParticipant
    {
        public Participant Participant { get; private set; }

        public decimal NewRating { get; private set; }
        
        public decimal OldRating { get; private set; }

        public decimal RatingChange => NewRating - OldRating;

        internal RatingResultParticipant(Participant participant, decimal newRating, decimal oldRating)
        {
            Participant = participant;
            NewRating = newRating;
            OldRating = oldRating;
        }
    }
}
