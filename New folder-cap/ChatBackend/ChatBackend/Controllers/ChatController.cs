using ChatBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ChatBackend.Data;
using System;
using System.Security.Claims;

namespace ChatBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _um;

        public ChatController(ApplicationDbContext db, UserManager<User> um)
        {
            _db = db;
            _um = um;
        }

        // ✅ Fetch private messages between logged-in user and another user (by DisplayName)
        [HttpPost("private/{displayName}")]
        public async Task<IActionResult> GetPrivate(string displayName)
        {
            var meId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub")?.Value;
            if (meId == null) return Unauthorized();

            var otherUser = await _um.Users.FirstOrDefaultAsync(u => u.DisplayName == displayName);
            if (otherUser == null) return NotFound("User not found");

            var msgs = await _db.Messages
                .Where(m => (m.SenderId == meId && m.ReceiverId == otherUser.Id) ||
                            (m.SenderId == otherUser.Id && m.ReceiverId == meId))
                .OrderBy(m => m.SentAtUtc)
                .ToListAsync();

            var users = await _um.Users
                .Where(u => msgs.SelectMany(m => new[] { m.SenderId, m.ReceiverId }).Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.DisplayName);

            return Ok(msgs.Select(m => new
            {
                m.Id,
                Sender = users.ContainsKey(m.SenderId) ? users[m.SenderId] : null,
                Receiver = m.ReceiverId != null && users.ContainsKey(m.ReceiverId) ? users[m.ReceiverId] : null,
                m.Content,
                m.GroupId,
                m.SentAtUtc
            }));
        }

        // DTO for sending a message (✅ now uses DisplayName instead of UserId)
        public record SendDto(string? ReceiverDisplayName, int? GroupId, string Content);

        // ✅ Send a message (private or group)
        [HttpPost("send")]
        public async Task<IActionResult> Send([FromBody] SendDto dto)
        {
            var meId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub")?.Value;
            if (meId == null) return Unauthorized();

            string? receiverId = null;
            if (!string.IsNullOrEmpty(dto.ReceiverDisplayName))
            {
                var receiver = await _um.Users.FirstOrDefaultAsync(u => u.DisplayName == dto.ReceiverDisplayName);
                if (receiver == null) return NotFound("Receiver not found");
                receiverId = receiver.Id;
            }

            var msg = new Message
            {
                SenderId = meId,
                ReceiverId = receiverId,
                GroupId = dto.GroupId,
                Content = dto.Content,
                SentAtUtc = DateTime.UtcNow
            };

            await _db.Messages.AddAsync(msg);
            await _db.SaveChangesAsync();

            var sender = await _um.FindByIdAsync(meId);

            return Ok(new
            {
                msg.Id,
                Sender = sender?.DisplayName,
                Receiver = dto.ReceiverDisplayName,
                msg.GroupId,
                msg.Content,
                msg.SentAtUtc
            });
        }

        // ✅ Fetch messages for a group (returns DisplayNames)
        [HttpGet("group/{groupId:int}")]
        public async Task<IActionResult> GetGroup(int groupId)
        {
            var msgs = await _db.Messages
                .Where(m => m.GroupId == groupId)
                .OrderBy(m => m.SentAtUtc)
                .ToListAsync();

            var users = await _um.Users
                .Where(u => msgs.Select(m => m.SenderId).Contains(u.Id))
                .ToDictionaryAsync(u => u.Id, u => u.DisplayName);

            return Ok(msgs.Select(m => new
            {
                m.Id,
                Sender = users.ContainsKey(m.SenderId) ? users[m.SenderId] : null,
                Receiver = m.ReceiverId,
                m.GroupId,
                m.Content,
                m.SentAtUtc
            }));
        }
    }
}
