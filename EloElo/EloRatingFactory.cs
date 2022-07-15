using EloElo.Enums;
using EloElo.Interfaces;
using EloElo.RatingSystems;

namespace EloElo
{
    public static class EloRatingFactory
    {
        public static IRatingSystem GetRatingSystem(RatingSystemVariation variation)
        {
            return new EloRatingService(variation);
        }
    }
}