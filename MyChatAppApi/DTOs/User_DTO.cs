﻿using System;

namespace MyChatAppApi.DTOs
{
    public class User_DTO
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string JWTToken { get; set; }
    }
}
