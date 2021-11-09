using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notta.Dto
{
  /// <summary>
  /// 
  /// </summary>
  public class UserForLoginDto
  {
    /// <summary>
    /// maps to the matNo field in the student entity
    /// </summary>
    public string Email { get; set; }


    /// <summary>
    /// stores the student's password which is then used in generating the passwordhash field which is then validated against the stored passwordHarsh
    /// </summary>
    public string Password { get; set; }
  }
}
