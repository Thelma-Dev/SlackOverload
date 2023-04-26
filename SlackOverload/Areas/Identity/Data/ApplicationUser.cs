using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SlackOverload.Models;

namespace SlackOverload.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    [Required(AllowEmptyStrings = false)]
    [StringLength(150, ErrorMessage ="Display Name must be between 2 and 150 characters")]
    [Display(Name ="Display Name")]
    public string DisplayName { get; set; }

    public int Reputation { get; set; } = 0;

    public virtual HashSet<Question> Questions { get; set; } = new HashSet<Question>();

    public virtual HashSet<Answer> Answers { get; set; } = new HashSet<Answer>();
}

