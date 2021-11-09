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
  public class UserForDetailedDto
  {
    /// <summary>
    /// the primary key of the table
    /// </summary>
    public int Id { get; set; }


    /// <summary>
    /// user's Name
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// user's Email address
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// The date the user created his/her account
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// a list of posts created by the user
    /// </summary>
    public ICollection<Post> Posts { get; set; }
  }
}
