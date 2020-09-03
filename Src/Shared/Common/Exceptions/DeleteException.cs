using System;

namespace Common.Exceptions
{
    public class DeleteException : Exception
    {
        public DeleteException(object id, string message, Exception ex = null)
            : base($"Could not delete entity.\nEntity id: {id}\nMessage: {message}", ex)
        {
        }

        public DeleteException(object id, Exception ex = null)
            : base($"Could not delete entity.\nEntity id: {id}", ex)
        {
        }
    }
}