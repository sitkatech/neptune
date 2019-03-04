using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Ajax.Utilities;

namespace Neptune.Web.Models
{
    public static class NetworkCatchmentModelExtensions
    {
        public static List<int> TraceUpstreamCatchmentsReturnIDList(this NetworkCatchment networkCatchment)
        {
            var idList = new List<int>();

            var lookingAt = networkCatchment.NetworkCatchmentsWhereYouAreTheOCSurveyDownstreamCatchment;
            while (lookingAt.Any())
            {
                var ints = lookingAt.Select(x => x.NetworkCatchmentID);
                idList.AddRange(ints);
                lookingAt = (ICollection<NetworkCatchment>) lookingAt.SelectMany(x =>
                    x.NetworkCatchmentsWhereYouAreTheOCSurveyDownstreamCatchment);
            }

            return idList;
        }
    }
}