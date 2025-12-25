using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Application.Dto
{
    public class UserNotificationProjectDto : BaseDto
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public bool EnableEmail { get; set; }
        public bool EnableSignalR { get; set; }
    }
}
