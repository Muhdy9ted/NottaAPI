using Notta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notta.Data
{
  /// <summary>
  /// implementing the Repository pattern for AuthController. 
  /// </summary>
  public interface IAuthRepository
  {
    /// <summary>
    /// register method for the auth controller
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<User> Register(User user, string password);


    /// <summary>
    /// login method for the auth controller
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<User> Login(string email, string password);


    /// <summary>
    /// verify user exist method for the auth controller
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<bool> UserExist(string email);
  }
}
