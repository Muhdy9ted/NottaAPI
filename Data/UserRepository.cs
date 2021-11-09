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
  public class UserRepository : IUserRepository
  {
    private readonly ApplicationDbContext _context;

    /// <summary>
    /// the student controller repo constructor
    /// </summary>
    /// <param name="context"></param>
    public UserRepository(ApplicationDbContext context)
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
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<User> GetUser(int id)
    {
      var user = await _context.Users.Include(x => x.Posts).FirstOrDefaultAsync(u => u.Id == id);
      return user;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<User>> GetUsers()
    {
      var users = await _context.Users.ToListAsync();
      return users;
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
