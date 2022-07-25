using Microsoft.AspNetCore.Mvc;
using Tamagatchi.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Tamagatchi.Controllers
{
  public class PlayController : Controller
  {
    [HttpGet("/play")]
    public ActionResult Index()
    {
      List<BaseTamagatchi> allMinions = BaseTamagatchi.GetAll();
      return View(allMinions);
    }

    [HttpGet("/play/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/play")]
    public ActionResult Index(string name)
    {

      int hunger = BaseTamagatchi.GetStat(1, 10);
      int happy = BaseTamagatchi.GetStat(1, 10);
      int training = BaseTamagatchi.GetStat(1, 10);
      int test = BaseTamagatchi.GetStat(1,10);
      //pos expected -> hunger, happy, training, discipline, height, weight, age
      //pos 0 - 7 -> hunger, skip, training, discipline, height, age, skipped, weight
      BaseTamagatchi user = new BaseTamagatchi(name,2,10,1,2,3,4,5);
      //pos expected -> id, hunger, happy, training, discipline, height, weight, age
      //pos 0 - 8 -> skip, hunger, skip, training, discipline, height, age, skip, weight
      user.Save();
      return RedirectToAction("Index");
    }

    [HttpPost("/play/{id}/update")]        //6
    public ActionResult Show(int id)
    { 
      BaseTamagatchi foundMinion = BaseTamagatchi.Find(id);
      foundMinion.Feed();
      foundMinion.MakeHappy();
      foundMinion.GetAge();
      return View(foundMinion);
    }

    [HttpGet("/play/{id}")]
    public ActionResult Show(int id, string button)
    {
      BaseTamagatchi foundMinion = BaseTamagatchi.Find(id);
      foundMinion.Timer();
      return View(foundMinion);
    }

  }
}