using System;

namespace Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(object id, string message, Exception ex = null)
            : base($"Entity not found.\nEntity id: {id}\nMessage: {message}", ex)
        {
        }

        public NotFoundException(object id, Exception ex = null)
            : base($"Entity not found.\nEntity id: {id}", ex)
        {
        }
    }
}
