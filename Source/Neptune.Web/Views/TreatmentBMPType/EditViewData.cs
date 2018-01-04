/*-----------------------------------------------------------------------
<copyright file="EditViewData.cs" company="Tahoe Regional Planning Agency and Sitka Technology Group">
Copyright (c) Tahoe Regional Planning Agency and Sitka Technology Group. All rights reserved.
<author>Sitka Technology Group</author>
</copyright>

<license>
This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License <http://www.gnu.org/licenses/> for more details.

Source code is available upon request via <support@sitkatech.com>.
</license>
-----------------------------------------------------------------------*/

using System.Collections.Generic;
using System.Linq;
using Neptune.Web.Common;
using Neptune.Web.Controllers;
using Neptune.Web.Models;

namespace Neptune.Web.Views.TreatmentBMPType
{
    public class EditViewData : NeptuneViewData
    {
        public ViewDataForAngular ViewDataForAngular { get; } 
        public string TreatmentBMPTypeIndexUrl { get; }
        public string SubmitUrl { get; }


        public EditViewData(Person currentPerson, List<Models.ObservationType> observationTypes, string submitUrl) : base(currentPerson)
        {
            PageTitle = $"{Models.FieldDefinition.TreatmentBMPType.GetFieldDefinitionLabel()}";
            ViewDataForAngular = new ViewDataForAngular(observationTypes);
            TreatmentBMPTypeIndexUrl = SitkaRoute<TreatmentBMPTypeController>.BuildUrlFromExpression(x => x.Index());
            SubmitUrl = submitUrl;
        }
    }

    public class ViewDataForAngular
    {
       
        public List<ObservationTypeSimple> ObservationTypeSimples { get; }
       

        public ViewDataForAngular(List<Models.ObservationType> observationTypes)
        {
            ObservationTypeSimples = observationTypes.Select(x => new ObservationTypeSimple(x)).ToList();
           
        }
    }

}
