using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notta.Models
{
  /// <summary>
  /// 
  /// </summary>
  public class Like
  {
    /// <summary>
    /// 
    /// </summary>
    public int LikerId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public int PostLikedId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public User Liker { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Post PostLiked { get; set; }
  }
}
