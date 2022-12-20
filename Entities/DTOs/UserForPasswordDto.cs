using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    public class UserForPasswordDto
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string RepeatNewPassword{ get; set; }
    }
}
