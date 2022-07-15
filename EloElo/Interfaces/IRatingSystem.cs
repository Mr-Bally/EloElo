using EloElo.Common;
using EloElo.Enums;

namespace EloElo.Interfaces
{
    public interface IRatingSystem
    {
        (decimal participantOneExpectedScore, decimal participantTwoExpectedScore) GetExpectedScore(decimal participantOneRating, decimal participantTwoRating);
        RatingResult GetResultRating(decimal participantOneRating, decimal participantTwoRating, ResultType result);
    }
}
