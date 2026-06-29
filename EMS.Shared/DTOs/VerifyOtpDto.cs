using System;
using System.Collections.Generic;
using System.Text;

namespace EMS.Shared.DTOs
{
    public class VerifyOtpDto
    {
        public string UserName { get; set; } = string.Empty;

        public string OtpCode { get; set; } = string.Empty;
    }
}
