using Microsoft.EntityFrameworkCore;
using Notta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notta.Data
{
  /// <summary>
  /// the context session that interfaces with our database to provide us several operations and functionalities
  /// </summary>
  public class ApplicationDbContext : DbContext
  {
    /// <summary>
    /// accessing the instance of the DbContext options
    /// </summary>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


    /// <summary>
    /// course model entity
    /// </summary>
    public DbSet<User> Users { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DbSet<Post> Posts { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DbSet<Comment> Comments { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DbSet<Like> Likes { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Like>().HasKey(k => new { k.LikerId, k.PostLikedId });
      builder.Entity<Like>().HasOne(u => u.PostLiked).WithMany(u => u.Likes).HasForeignKey(u => u.PostLikedId).OnDelete(DeleteBehavior.Restrict);
      builder.Entity<Like>().HasOne(u => u.Liker).WithMany(u => u.Likes).HasForeignKey(u => u.LikerId).OnDelete(DeleteBehavior.Restrict);

      builder.Entity<Comment>().HasOne(c => c.Post).WithMany(p => p.Comments).HasForeignKey(p => p.PostId).OnDelete(DeleteBehavior.Restrict);
      builder.Entity<Comment>().HasOne(c => c.User).WithMany(p => p.Comments).HasForeignKey(p => p.UserId).OnDelete(DeleteBehavior.Restrict);
    }
  }
}
