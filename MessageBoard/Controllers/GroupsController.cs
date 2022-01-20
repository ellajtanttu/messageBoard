using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using MessageBoard.Models;
using System.Collections.Generic;
using System.Linq;

namespace MessageBoard.Controllers
{
  public class GroupsController : Controller
  {
    private readonly MessageBoardContext _db;

    public GroupsController(MessageBoardContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Group> model = _db.Groups.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Group Group)
    {
      _db.Groups.Add(Group);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisGroup = _db.Groups
          .Include(Group => Group.JoinEntities)
          .ThenInclude(join => join.EndUser)
          .FirstOrDefault(Group => Group.GroupId == id);
      return View(thisGroup);
    }
    public ActionResult Edit(int id)
    {
      var thisGroup = _db.Groups.FirstOrDefault(Group => Group.GroupId == id);
      return View(thisGroup);
    }

    [HttpPost]
    public ActionResult Edit(Group Group)
    {
      _db.Entry(Group).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisGroup = _db.Groups.FirstOrDefault(Group => Group.GroupId == id);
      return View(thisGroup);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisGroup = _db.Groups.FirstOrDefault(Group => Group.GroupId == id);
      _db.Groups.Remove(thisGroup);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}