using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MessageBoard.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace MessageBoard.Controllers
{
  // [Authorize]
  public class EndUsersController : Controller
  {
    private readonly MessageBoardContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public EndUsersController(UserManager<ApplicationUser> userManager, MessageBoardContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [Authorize]
    public async Task<ActionResult> Index()
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      var userEndUsers = _db.EndUsers.Where(entry => entry.User.Id == currentUser.Id).ToList();
      return View(userEndUsers);
    }

    public ActionResult Create()
    {
      ViewBag.GroupId = new SelectList(_db.Groups, "GroupId", "Name");
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(EndUser endUser, int GroupId)
    {
      var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var currentUser = await _userManager.FindByIdAsync(userId);
      endUser.User = currentUser;
      _db.EndUsers.Add(endUser);
      _db.SaveChanges();
      if (GroupId != 0)
      {
          _db.Messages.Add(new Message() { GroupId = GroupId, EndUserId = endUser.EndUserId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisEndUser = _db.EndUsers
          .Include(endUser => endUser.JoinEntities)
          .ThenInclude(join => join.Group)
          .FirstOrDefault(endUser => endUser.EndUserId == id);
      return View(thisEndUser);
    }

    public ActionResult Edit(int id)
    {
      var thisEndUser = _db.EndUsers.FirstOrDefault(endUser => endUser.EndUserId == id);
      ViewBag.GroupId = new SelectList(_db.Groups, "GroupId", "Name");
      return View(thisEndUser);
    }

    [HttpPost]
    public ActionResult Edit(EndUser endUser, int GroupId)
    {
      if (GroupId != 0)
      {
        _db.Messages.Add(new Message() { GroupId = GroupId, EndUserId = endUser.EndUserId });
      }
      _db.Entry(endUser).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddGroup(int id)
    {
      var thisEndUser = _db.EndUsers.FirstOrDefault(endUser => endUser.EndUserId == id);
      ViewBag.GroupId = new SelectList(_db.Groups, "GroupId", "Name");
      return View(thisEndUser);
    }

    [HttpPost]
    public ActionResult AddGroup(EndUser endUser, int GroupId)
    {
      if (GroupId != 0)
      {
      _db.Messages.Add(new Message() { GroupId = GroupId, EndUserId = endUser.EndUserId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisEndUser = _db.EndUsers.FirstOrDefault(endUser => endUser.EndUserId == id);
      return View(thisEndUser);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisEndUser = _db.EndUsers.FirstOrDefault(endUser => endUser.EndUserId == id);
      _db.EndUsers.Remove(thisEndUser);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteGroup(int joinId)
    {
      var joinEntry = _db.Messages.FirstOrDefault(entry => entry.MessageId == joinId);
      _db.Messages.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}