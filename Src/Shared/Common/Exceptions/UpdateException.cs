using System;

namespace Common.Exceptions
{
    public class UpdateException : Exception
    {
        public UpdateException(object id, string message, Exception ex = null)
            : base($"Could not update entity.\nEntity id: {id}\nMessage: {message}", ex)
        {
        }

        public UpdateException(object id, Exception ex = null)
            : base($"Could not update entity.\nEntity id: {id}", ex)
        {
        }
    }
}