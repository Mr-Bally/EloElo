using EloElo.Enums;

namespace EloElo.Common
{
    public class ParticipantRating
    {
        public Participant Participant { get; private set; }

        public decimal Rating { get; private set; }

        internal ParticipantRating(Participant participant, decimal rating)
        {
            Participant = participant;
            Rating = rating;
        }
    }
}
