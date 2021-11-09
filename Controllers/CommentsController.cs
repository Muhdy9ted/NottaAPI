using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Notta.Data;
using Notta.Dto;
using Notta.Models;

namespace Notta.Controllers
{
  /// <summary>
  /// 
  /// </summary>
  [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class CommentsController : ControllerBase
  {
    private readonly ICommentRepository _repo;
    private readonly IMapper _mapper;
    private readonly IUserRepository _Urepo;
    private readonly IPostRepository _Prepo;


    /// <summary>
    /// the courses controller constructor
    /// </summary>
    /// <param name="repo"></param>
    /// <param name="mapper"></param>
    /// <param name="Urepo"></param>
    /// <param name="Prepo"></param>
    public CommentsController(ICommentRepository repo, IMapper mapper, IUserRepository Urepo, IPostRepository Prepo)
    {
      _repo = repo;
      _mapper = mapper;
      _Urepo = Urepo;
      _Prepo = Prepo;
    }

    // GET: api/Posts
    /// <summary>
    /// API endpoint for retrieving all the available posts
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetComments()
    {
      var comments = await _repo.GetComments();
      return Ok(comments);
    }

    // GET: api/posts/5
    /// <summary>
    /// API endpoint for retrieving a particular post 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetComment(int id)
    {
      var comment = await _repo.GetComment(id);
      return Ok(comment);
    }

    // GET: api/posts/5
    /// <summary>
    /// API endpoint for retrieving a particular post 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("post-comments/{id}")]
      public async Task<IActionResult> GetCommentsInPost(int id)
      {
        var comments = await _repo.GetCommentsInPost(id);
        return Ok(comments);
      }


    // POST: api/posts
    /// <summary>
    /// API endpoint for creating a post
    /// </summary>
    /// <param name="commentForCreateDto"></param>
    /// <returns></returns>
    [HttpPost]
      public async Task<IActionResult> CreateComment(CommentForCreateDto commentForCreateDto)
      {
        int userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        int postId = commentForCreateDto.PostId;
        commentForCreateDto.User = await _Urepo.GetUser(userId);
        commentForCreateDto.Post = await _Prepo.GetPost(postId);
        //postForCreateDto.UserId = userId;
        var commentToCreate = _mapper.Map<Comment>(commentForCreateDto);
        _repo.Add(commentToCreate);
        if (await _repo.SaveAll())
        {
          return CreatedAtAction("GetComment", new { id = commentToCreate.Id }, commentToCreate);
        }
        throw new Exception($"creating post {commentToCreate.Content} failed to create");
      }

      // Delete: api/posts
      /// <summary>
      /// API endpoint for creating a post
      /// </summary>
      /// <param name="id"></param>
      /// <returns></returns>
      [HttpDelete("{id}")]
      public async Task<IActionResult> DeleteComment(int id)
      {
        var comment = await _repo.GetComment(id);
        if (comment.UserId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
        {
          return Unauthorized();
        }
        _repo.Delete(comment);
        if (await _repo.SaveAll())
        {
          return Ok();
        }
        throw new Exception($"deleting comment {comment.Content} failed on delete");
      }
    }
}