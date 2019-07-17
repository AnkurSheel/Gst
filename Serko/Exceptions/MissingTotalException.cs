using System;

namespace Serko.Exceptions
{
    public class MissingTotalException : Exception
    {
        public MissingTotalException() :base("Total is missing")
        {
        }
    }
}