using System;

namespace Neptune.API.Hangfire
{
    public class GeoprocessingException : Exception
    {
        public GeoprocessingException(string message) : base(message)
        {

        }
    }
}