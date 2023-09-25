using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Neptune.EFModels.Entities
{
    [Table("NotificationType")]
    [Index("NotificationTypeDisplayName", Name = "AK_NotificationType_NotificationTypeDisplayName", IsUnique = true)]
    [Index("NotificationTypeName", Name = "AK_NotificationType_NotificationTypeName", IsUnique = true)]
    public partial class NotificationType
    {
        public NotificationType()
        {
            Notifications = new HashSet<Notification>();
        }

        [Key]
        public int NotificationTypeID { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string NotificationTypeName { get; set; }
        [Required]
        [StringLength(100)]
        [Unicode(false)]
        public string NotificationTypeDisplayName { get; set; }

        [InverseProperty("NotificationType")]
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
