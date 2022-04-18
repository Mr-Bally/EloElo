using EloElo.Common;
using EloElo.Enums;

namespace EloElo.Interfaces
{
    public interface IRatingSystem
    {
        decimal GetExpectedScore(Participant participant);
        ParticipantRating GetParticipantRating(Participant participant);
        RatingResult GetResultRating(ResultType result);
    }
}
