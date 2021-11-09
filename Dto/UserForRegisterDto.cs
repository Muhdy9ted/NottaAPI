using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Notta.Dto
{
  /// <summary>
  /// Dto for user registration
  /// </summary>
  public class UserForRegisterDto
  {
    /// <summary>
    /// maps to the email field of the user entity
    /// </summary>
    [Required]
    [StringLength(20, ErrorMessage = "email address cannot be longer than 20 characters")]
    public string Email { get; set; }

    /// <summary>
    /// stores the user's password which is then used in generating the passwordhash field in the user entity
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "You must specify a password of minimum 8 characters")]
    public string Password { get; set; }

    /// <summary>
    /// maps to the Name field of the user entity
    /// </summary>
    [Required]
    [StringLength(30)]
    public string Name { get; set; }

    /// <summary>
    /// maps to the dateJoined field of the student entity
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// class constructor
    /// </summary>
    public UserForRegisterDto()
    {
      CreatedAt = DateTime.Now;
    }
  }
}
