using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
 
namespace MyChatAppApi.Models
{
    [PrimaryKey(nameof(Id))]
    public class User
    {
        
        public Guid Id;
        public string UserName;
        public string Password;
        public string Email;
        public string Type;
    }
}
