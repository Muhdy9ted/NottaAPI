using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Notta.Models
{
  /// <summary>
  /// 
  /// </summary>
  public class Post
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
    /// a list of courses offered by the user
    /// </summary>
    public ICollection<Comment> Comments { get; set; }

    /// <summary>
    /// a list of courses offered by the student
    /// </summary>
    public ICollection<Like> Likes { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public Post()
    {
      Comments = new Collection<Comment>();

      Likes = new Collection<Like>();

    }
  }
}
