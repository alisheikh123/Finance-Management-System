using Finance.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
 
 

namespace Finance.Controllers
{
    public class FirstLevelController : Controller
    {

        dbFinanceEntities dbEntities = new dbFinanceEntities();
        // GET: ChartOfAccount
        [HttpGet]
        public ActionResult FirstLevel()
        {
            tblFirstLevel fruit = new tblFirstLevel();
            fruit.FirstLevelDetails = dbEntities.tblFirstLevels.ToList();

            return View(fruit);
        }



        [HttpGet]
        // GET: FirstLevel
        public ActionResult FirstLevelEdit(int id = 0)
        {

            tblFirstLevel tblFirstLevel = dbEntities.tblFirstLevels.Find(id);
            tblFirstLevel.FirstLevelDetails = dbEntities.tblFirstLevels.ToList();
            return View("FirstLevel", tblFirstLevel);
        }


        [HttpPost]
        public ActionResult FirstLevel(tblFirstLevel firstLevelModel)
        {
            //if (dbEntities.tblFirstLevels.Any(x => x.firstLevelAccount == firstLevelModel.firstLevelAccount) && firstLevelModel.firstLevelId <= 0)
            //{
            //    ViewBag.DuplicateMsg = "Account name already exist!";
            //    firstLevelModel.FirstLevelDetails = dbEntities.tblFirstLevels.ToList();
            //    return View("FirstLevel", firstLevelModel);
            //}
            //else
            if (dbEntities.tblFirstLevels.Any(x => x.firstLevelAccount == firstLevelModel.firstLevelAccount && x.firstLevelId != firstLevelModel.firstLevelId))
            {
                ViewBag.DuplicateMsg = "Account name already exist!";
                firstLevelModel.FirstLevelDetails = dbEntities.tblFirstLevels.ToList();
                return View("FirstLevel", firstLevelModel);
            }
            else if (firstLevelModel.firstLevelId > 0)
            {
                dbEntities.Entry(firstLevelModel).State = EntityState.Modified;
                dbEntities.SaveChanges();
            }
            else
            {
                dbEntities.tblFirstLevels.Add(firstLevelModel);
                dbEntities.SaveChanges();
            }

            ModelState.Clear();
            ViewBag.SuccessMsg = "Save successfully!";

            tblFirstLevel fruit = new tblFirstLevel();
            fruit.FirstLevelDetails = dbEntities.tblFirstLevels.ToList();

            //FirstLevel();
            return View("FirstLevel", fruit);
        }



        [HttpGet]
        // GET: FirstLevel
        public ActionResult FirstLevelDelete(int id = 0)
        {
            tblFirstLevel tblFirstLevel = dbEntities.tblFirstLevels.Find(id);
            dbEntities.tblFirstLevels.Remove(tblFirstLevel);
            dbEntities.SaveChanges();

            ModelState.Clear();
            ViewBag.SuccessMsg = "Save successfully!";

            tblFirstLevel fruit = new tblFirstLevel();
            fruit.FirstLevelDetails = dbEntities.tblFirstLevels.ToList();

            return View("FirstLevel", fruit);

        }




    }
}