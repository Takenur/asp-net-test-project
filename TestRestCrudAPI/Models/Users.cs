using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestRestCrudAPI.Models
{
    public class Users : IdentityUser
    {
        [Required]
        [MaxLength(255)]
        public string name { get; set; }
        
    }
}
