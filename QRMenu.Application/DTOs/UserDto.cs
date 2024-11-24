﻿using QRMenu.Core.Enums;

namespace QRMenu.Application.DTOs
{
    public class UserDto : BaseDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public UserRole Role { get; set; }
        public int? DealerId { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public string? LastLoginIp { get; set; }

    }
}
