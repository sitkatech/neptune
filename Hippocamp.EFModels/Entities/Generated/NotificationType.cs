using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Hippocamp.EFModels.Entities
{
    [Table("NotificationType")]
    [Index(nameof(NotificationTypeDisplayName), Name = "AK_NotificationType_NotificationTypeDisplayName", IsUnique = true)]
    [Index(nameof(NotificationTypeName), Name = "AK_NotificationType_NotificationTypeName", IsUnique = true)]
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
        public string NotificationTypeName { get; set; }
        [Required]
        [StringLength(100)]
        public string NotificationTypeDisplayName { get; set; }

        [InverseProperty(nameof(Notification.NotificationType))]
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
