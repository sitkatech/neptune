using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace Hippocamp.EFModels.Entities
{
    [Keyless]
    [Table("LEGAL_LOTS_ATTRIBUTES_4326")]
    public partial class LEGAL_LOTS_ATTRIBUTES_4326
    {
        public int? OBJECTID { get; set; }
        [StringLength(256)]
        [Unicode(false)]
        public string LEGAL_DESCR { get; set; }
        [StringLength(12)]
        [Unicode(false)]
        public string RECORDER_NO { get; set; }
        [StringLength(102)]
        [Unicode(false)]
        public string MAIL_ADDRESS { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string ZONECLASS { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string DESCRIPTIO { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string CATEGORY { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string ZONE_AND_Housing { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Dist_Regs { get; set; }
        [StringLength(255)]
        [Unicode(false)]
        public string Link { get; set; }
        [StringLength(96)]
        [Unicode(false)]
        public string OWNER_NAMES { get; set; }
        [StringLength(102)]
        [Unicode(false)]
        public string SITE_ADDRESS { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string COLOR { get; set; }
        [StringLength(22)]
        [Unicode(false)]
        public string ASSESSMENT_NO { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string SITE_ADDR_NO { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string SITE_STREET_PREFIX { get; set; }
        [StringLength(52)]
        [Unicode(false)]
        public string SITE_STREET_NAME { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string SITE_STREET_SUFFIX { get; set; }
        [StringLength(56)]
        [Unicode(false)]
        public string SITE_CITY_STATE { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string SITE_ZIP5 { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string MAIL_ADDR_NO { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string MAIL_PREFIX { get; set; }
        [StringLength(52)]
        [Unicode(false)]
        public string MAIL_STREET { get; set; }
        [StringLength(56)]
        [Unicode(false)]
        public string MAIL_CITY_STATE { get; set; }
        [StringLength(6)]
        [Unicode(false)]
        public string MAIL_UNIT_NO { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string MAIL_SUFFIX { get; set; }
        [StringLength(5)]
        [Unicode(false)]
        public string MAIL_ZIP5 { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string LOT_SIZE { get; set; }
        [StringLength(14)]
        [Unicode(false)]
        public string DOC_REF_NO { get; set; }
        [StringLength(8)]
        [Unicode(false)]
        public string DOC_REF_DATE { get; set; }
        public int? FabricOID { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string USE_DQ_LANDUSE { get; set; }
        public DateTime? ModifyDate { get; set; }
        public int? SQFT_HOME { get; set; }
        public int? SQFT_LOT { get; set; }
        public double? SALE_AMT { get; set; }
        [StringLength(2)]
        [Unicode(false)]
        public string SALE_TYPE { get; set; }
        [StringLength(52)]
        [Unicode(false)]
        public string DOC_SELLER_NAME { get; set; }
        [StringLength(52)]
        [Unicode(false)]
        public string DOC_BUYER_NAME { get; set; }
        [StringLength(4)]
        [Unicode(false)]
        public string DOC_DEED_TYPE { get; set; }
        public DateTime? SALE_RECORD_DATE { get; set; }
        public double? ASSD_AMT { get; set; }
        public double? LAND_VAL { get; set; }
        public double? BLDG_VAL { get; set; }
        public double? SHAPE_Length { get; set; }
        public double? SHAPE_Area { get; set; }
        [Column(TypeName = "geometry")]
        public Geometry GEOM { get; set; }
    }
}
