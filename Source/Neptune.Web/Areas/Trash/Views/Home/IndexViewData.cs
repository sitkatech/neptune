﻿using Neptune.Web.Models;
using Neptune.Web.Views;

namespace Neptune.Web.Areas.Trash.Views.Home
{
    public class IndexViewData : NeptuneViewData
    {
        public IndexViewData(Person currentPerson) : base(currentPerson)
        {
        }
    }
}