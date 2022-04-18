namespace EloElo.Common
{
    public class RatingResult
    {
        public RatingResultParticipant ParticipantOne { get; private set; }
        
        public RatingResultParticipant ParticipantTwo { get; private set; }

        internal RatingResult(RatingResultParticipant participantOne, RatingResultParticipant participantTwo)
        {
            ParticipantOne = participantOne;
            ParticipantTwo = participantTwo;
        }
    }
}
