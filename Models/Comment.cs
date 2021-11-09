using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notta.Models
{
  /// <summary>
  /// 
  /// </summary>
  public class Comment
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
    public User User { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Post Post { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int PostId { get; set; }
  }
}
