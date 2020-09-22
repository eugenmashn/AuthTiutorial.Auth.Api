using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTiutorial.Auth.Api.Models
{
    public class Account
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Passwword { get; set; }
        public Role[] Roles { get; set; }

       
    } 
    public enum Role
    {
        User,
        Admin
    }
}
