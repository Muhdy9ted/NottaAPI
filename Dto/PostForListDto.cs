using Notta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notta.Dto
{
  /// <summary>
  /// 
  /// </summary>
  public class PostForListDto
  {
    /// <summary>
    /// 
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public UserForListDto User { get; set; }

    /// <summary>
    /// a list of courses offered by the user
    /// </summary>
    public ICollection<Comment> Comments { get; set; }

    /// <summary>
    /// a list of courses offered by the student
    /// </summary>
    public ICollection<Like> Likes { get; set; }

  }
}
