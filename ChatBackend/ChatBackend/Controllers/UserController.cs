using ChatBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatBackend.Data;
using System;
using System.Linq;
using System.Security.Claims;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly UserManager<User> _um;
    private readonly ApplicationDbContext _context;
    public UsersController(UserManager<User> um, ApplicationDbContext context)
    {
        _um = um;
        _context = context;
    }

    [HttpGet("status")]
    public IActionResult Status()
        => Ok(_um.Users.Select(u => new
        {
            u.Id,
            u.Email,
            u.DisplayName,
            u.Status,
            u.LastSeenUtc
        }));

    [HttpGet("find/{username}")]
    public IActionResult FindByUsername(string username)
    {
        var user = _um.Users.FirstOrDefault(u => u.DisplayName == username);
        if (user == null) return NotFound();
        return Ok(new { user.Id, user.DisplayName });
    }

    [HttpGet("all-messages")]
    public async Task<IActionResult> GetAllMessages()
    {
        var meId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");

        // Get IDs of groups this user belongs to
        var myGroupIds = await _context.GroupMembers
            .Where(gm => gm.UserId == meId)
            .Select(gm => gm.GroupId)
            .ToListAsync();


        // Get all messages: personal + groups this user is part of
        var messages = await _context.Messages
            .Where(m =>
                (m.SenderId == meId || m.ReceiverId == meId) // personal messages
                || (m.GroupId != null && myGroupIds.Contains(m.GroupId.Value)) // group messages
            )
            .OrderBy(m => m.SentAtUtc)
            .ToListAsync();

        var userIds = messages.SelectMany(m => new[] { m.SenderId, m.ReceiverId })
                              .Where(id => id != null)
                              .Distinct()
                              .ToList();

        var users = await _um.Users
            .Where(u => userIds.Contains(u.Id))
            .ToDictionaryAsync(u => u.Id, u => u.DisplayName);

        var result = messages.Select(m => new
        {
            m.Id,
            meId,
            m.SenderId,
            m.ReceiverId,
            Sender = users[m.SenderId],
            Receiver = m.ReceiverId != null ? users[m.ReceiverId] : null,
            m.GroupId,
            m.Content,
            m.AttachmentUrl,
            m.SentAtUtc

        });

        return Ok(result);


    }

}