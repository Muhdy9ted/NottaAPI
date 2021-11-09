using Microsoft.EntityFrameworkCore;
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
  public class AuthRepository : IAuthRepository
  {
    private readonly ApplicationDbContext _context;


    /// <summary>
    /// AuthRepo constructor
    /// </summary>
    /// <param name="context"></param>
    public AuthRepository(ApplicationDbContext context)
    {
      _context = context;
    }


    /// <summary>
    /// API endpoint for user login
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<User> Login(string email, string password)
    {
      var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

      if (user == null)
      {
        return null;
      }

      if (!(VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt)))
      {
        return null;
      }

      // Auth succesfull
      return user;
    }


    /// <summary>
    /// verify if user's password matches with whats in the db 
    /// </summary>
    /// <returns></returns>
    public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
      {
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        for (int i = 0; i < computedHash.Length; i++)
        {
          if (computedHash[i] != passwordHash[i])
            return false;
        }
      }
      return true;
    }


    /// <summary>
    /// API endpoint for user registration
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<User> Register(User user, string password)
    {
      byte[] passwordHash, passwordSalt;

      CreatePasswordHash(password, out passwordHash, out passwordSalt);

      user.PasswordHash = passwordHash;
      user.PasswordSalt = passwordSalt;

      await _context.Users.AddAsync(user);
      await _context.SaveChangesAsync();

      return user;
    }


    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
      using (var hmac = new System.Security.Cryptography.HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }


    /// <summary>
    /// API endpoint to verify if a user with a particular email already exist 
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    public async Task<bool> UserExist(string email)
    {
      if (await _context.Users.AnyAsync(x => x.Email == email))
      {
        return true;
      }

      return false;
    }
  }
}
