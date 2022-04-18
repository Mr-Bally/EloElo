namespace EloElo.Exceptions
{
    public class InvalidRatingException : Exception
    {
        public InvalidRatingException() : base() 
        { 
        }

        public InvalidRatingException(string message) : base(message)
        {
        }
    }
}
