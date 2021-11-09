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
  public class CommentRepository : ICommentRepository
  {
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// repo constructor
    /// </summary>
    /// <param name="context"></param>
    public CommentRepository(ApplicationDbContext context)
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
    public async Task<IEnumerable<Comment>> GetComments()
    {
      var comments = await _context.Comments.ToListAsync();
      return comments;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Comment>> GetCommentsInPost(int id)
    {
      var comments = await _context.Comments.Where(x => x.PostId == id).ToListAsync();
      return comments;
    }



    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<bool> SaveAll()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Comment> GetComment(int id)
    {
      var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
      return comment;
    }
  }
}
