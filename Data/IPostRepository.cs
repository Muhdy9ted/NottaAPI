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
  public interface IPostRepository
  {
    /// <summary>
    /// for adding a post
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    void Add<T>(T entity) where T : class;


    /// <summary>
    /// for deleting
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    void Delete<T>(T entity) where T : class;


    /// <summary>
    /// for saving changes
    /// </summary>
    /// <returns></returns>
    Task<bool> SaveAll();


    /// <summary>
    /// for returning a list of posts
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<Post>> GetPosts();


    /// <summary>
    /// for getting a particular post
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Post> GetPost(int id);

  }
}
