using Microsoft.EntityFrameworkCore;
using Notta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notta.Data
{
  /// <summary>
  /// 
  /// </summary>
  public class PostRepository : IPostRepository
  {
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// repo constructor
    /// </summary>
    /// <param name="context"></param>
    public PostRepository(ApplicationDbContext context)
    {
      _context = context;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    public void Add<T>(T entity) where T : class
    {
      _context.Add(entity);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    public void Delete<T>(T entity) where T : class
    {
      _context.Remove(entity);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Post>> GetPosts()
    {
      var posts = await _context.Posts.Include(u => u.Comments).Include(u => u.User).ToListAsync();
      return posts;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Post> GetPost(int id)
    {
      var post = await _context.Posts.Include(u => u.Comments).Include(u => u.User).FirstOrDefaultAsync(x => x.Id == id);
      return post;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<bool> SaveAll()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}
