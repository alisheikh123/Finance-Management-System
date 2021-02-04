using Finance.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Finance.Controllers
{
    public class SecondLevelController : Controller
    {
        dbFinanceEntities dbEntities = new dbFinanceEntities();
        vSecondLevel fruit = new vSecondLevel();

        // GET: ChartOfAccount
        [HttpGet]
        public ActionResult SecondLevel()
        {
            //   tblSecondLevel sec = new tblSecondLevel();
            //  sec.SecLevelDetails = dbEntities.tblSecondLevels.ToList();
             fruit.SecLevelDetails = dbEntities.vSecondLevels.ToList();

            List<tblFirstLevel> deptlist = dbEntities.tblFirstLevels.ToList();


            ViewBag.deptlist = new SelectList(deptlist, "firstLevelId", "firstLevelAccount");
            return View(fruit);
        }
        [HttpPost]
        public ActionResult SecondLevel(vSecondLevel second)
        {
            if (dbEntities.tblSecondLevels.Any(x => x.secondLevelAccount == second.secondLevelAccount && x.firstLevelId==second.firstLevelId))
            {
                ViewBag.DuplicateMsg = "Account name already exist!";
                second.SecLevelDetails = dbEntities.vSecondLevels.ToList();
                second.SecLevelDetails = dbEntities.vSecondLevels.ToList();

                List<tblFirstLevel> deptlists = dbEntities.tblFirstLevels.ToList();


                ViewBag.deptlist = new SelectList(deptlists, "firstLevelId", "firstLevelAccount");


                return View("SecondLevel", second);
            }
            else if (second.secondLevelId > 0)
            {
                
                tblSecondLevel s = new tblSecondLevel();
                s.firstLevelId = second.firstLevelId;
                s.secondLevelId = second.secondLevelId;
                s.secondLevelAccount = second.secondLevelAccount;
                s.secondLevelDesc = second.secondLevelDesc;

                dbEntities.Entry(s).State = EntityState.Modified;
                dbEntities.SaveChanges();
            }
            else
            {
                tblSecondLevel s = new tblSecondLevel();
                s.firstLevelId = second.firstLevelId;
                s.secondLevelId = second.secondLevelId;
                s.secondLevelAccount = second.secondLevelAccount;
                s.secondLevelDesc = second.secondLevelDesc;

                dbEntities.tblSecondLevels.Add(s);
                dbEntities.SaveChanges();
            }

            ModelState.Clear();
            ViewBag.SuccessMsg = "Save successfully!";
            vSecondLevel fruit = new vSecondLevel();
            fruit.SecLevelDetails = dbEntities.vSecondLevels.ToList();

            List<tblFirstLevel> deptlist = dbEntities.tblFirstLevels.ToList();


            ViewBag.deptlist = new SelectList(deptlist, "firstLevelId", "firstLevelAccount");
            return View("SecondLevel", fruit);
        }
        [HttpGet]
        public ActionResult SecondLevelEdit(int id = 0)
        {
           

            vSecondLevel secondLevel = new vSecondLevel();  
            secondLevel.SecLevelDetails = dbEntities.vSecondLevels.Where(x => x.secondLevelId == id).ToList();
       

            foreach (var item in secondLevel.SecLevelDetails)
            {
                secondLevel.secondLevelAccount = item.secondLevelAccount;
                secondLevel.secondLevelDesc = item.secondLevelDesc;
                secondLevel.secondLevelId = id;
                secondLevel.firstLevelId = item.firstLevelId; 
            }


            List<tblFirstLevel> deptlist = dbEntities.tblFirstLevels.ToList();
ViewBag.deptlist = new SelectList(deptlist, "firstLevelId", "firstLevelAccount");
            return View("SecondLevel", secondLevel);
        }

        [HttpGet]
        // GET: FirstLevel
        public ActionResult SecondLevelDelete(int id = 0)
        {
            tblSecondLevel tblsecLevel = dbEntities.tblSecondLevels.Find(id);
           
            dbEntities.tblSecondLevels.Remove(tblsecLevel);
            dbEntities.SaveChanges();

            ModelState.Clear();
            ViewBag.SuccessMsg = "Save successfully!";
            fruit.SecLevelDetails = dbEntities.vSecondLevels.ToList();

            List<tblFirstLevel> deptlist = dbEntities.tblFirstLevels.ToList();


            ViewBag.deptlist = new SelectList(deptlist, "firstLevelId", "firstLevelAccount");
            return View("SecondLevel", fruit);
        

        }
    }
}