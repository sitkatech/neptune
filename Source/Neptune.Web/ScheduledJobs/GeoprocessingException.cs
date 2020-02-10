using System;

namespace Neptune.Web.ScheduledJobs
{
    public class GeoprocessingException : Exception
    {
        public GeoprocessingException(string message) : base(message)
        {

        }
    }
}