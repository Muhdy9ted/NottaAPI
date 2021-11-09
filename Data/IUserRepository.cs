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
  public interface IUserRepository
  {
    /// <summary>
    /// for adding a user
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="entity"></param>
    void Add<T>(T entity) where T : class;


    /// <summary>
    /// for deleting a user
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
    /// for returning a list of students
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<User>> GetUsers();


    /// <summary>
    /// for getting a particular student
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<User> GetUser(int id);
  }
}
