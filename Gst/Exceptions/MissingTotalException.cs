using System;

namespace Gst.Exceptions
{
    public class MissingTotalException : Exception
    {
        public MissingTotalException()
            : base("Total is missing")
        {
        }
    }
}
