using Notta.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Notta.Dto
{
  /// <summary>
  /// 
  /// </summary>
  public class PostForCreateDto
  {
    /// <summary>
    /// 
    /// </summary>
    [Required]
    [StringLength(120, ErrorMessage = "post cannot be longer than 120 characters")]
    public string Content { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int UserId { get; set; }

  }
}
