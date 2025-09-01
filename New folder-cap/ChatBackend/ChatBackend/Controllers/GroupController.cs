using ChatBackend.Data;
using ChatBackend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class GroupController : ControllerBase 
{
    private readonly ApplicationDbContext _db;
    public GroupController(ApplicationDbContext db) { _db = db; }
    public record CreateGroupDto(string Name, List<string> MemberUserIds);
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateGroupDto dto)
    {
        var g = new Group { Name = dto.Name };
        _db.Groups.Add(g);
        await _db.SaveChangesAsync();
        foreach (var uid in dto.MemberUserIds.Distinct())
            _db.GroupMembers.Add(new GroupMember
            {
                GroupId = g.Id,
                UserId = uid,
                Role = "member"
            });
        await _db.SaveChangesAsync();
        return Ok(g);
    }
}
