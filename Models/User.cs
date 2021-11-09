using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Notta.Models
{
  /// <summary>
  /// the student model
  /// </summary>
  public class User
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
    /// field for saving the user's password in hashed format
    /// </summary>
    public byte[] PasswordHash { get; set; }


    /// <summary>
    /// the salt key for hashing the password
    /// </summary>
    public byte[] PasswordSalt { get; set; }

    /// <summary>
    /// The date the student created his/her account
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// a list of courses offered by the user
    /// </summary>
    public ICollection<Post> Posts { get; set; }

    /// <summary>
    /// a list of courses offered by the user
    /// </summary>
    public ICollection<Like> Likes { get; set; }

    /// <summary>
    /// a list of courses offered by the user
    /// </summary>
    public ICollection<Comment> Comments { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public User()
    {
      Posts = new Collection<Post>();

      Comments = new Collection<Comment>();

      Likes = new Collection<Like>();

      CreatedAt = DateTime.Now;
    }
  }
}
