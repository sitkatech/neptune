﻿namespace Neptune.Models.DataTransferObjects
{
    public class OVTAResultsDto
    {
        public double PLUSumAcresWhereOVTAIsA { get; set; }
        public double PLUSumAcresWhereOVTAIsB { get; set; }
        public double PLUSumAcresWhereOVTAIsC { get; set; }
        public double PLUSumAcresWhereOVTAIsD { get; set; }
        public double ALUSumAcresWhereOVTAIsA { get; set; }
        public double ALUSumAcresWhereOVTAIsB { get; set; }
        public double ALUSumAcresWhereOVTAIsC { get; set; }
        public double ALUSumAcresWhereOVTAIsD { get; set; }
        public DateTime? LastUpdateDate { get; set; }
    }
}