using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ToDoIdentity.Models;

namespace ToDoIdentity.Controllers
{
    [Authorize]
    public class TopicsController: Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            var db = new ToDoContext();
            var topics = db.Topics.ToList();
            return View(topics);
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create()
        {
            var topic = new Topic();

            return View(topic);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Create(Topic model)
        {
            if (!ModelState.IsValid)
                return View(model);
            var db = new ToDoContext();
            

            db.Topics.Add(model);
            db.SaveChanges();


            return RedirectPermanent("/Topics/Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Delete(int id)
        {
            var db = new ToDoContext();
            var topic = db.Topics.FirstOrDefault(x => x.Id == id);
            if (topic == null)
                return RedirectPermanent("/Topics/Index");

            db.Topics.Remove(topic);
            db.SaveChanges();

            return RedirectPermanent("/Topics/Index");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(int id)
        {
            var db = new ToDoContext();
            var topic = db.Topics.FirstOrDefault(x => x.Id == id);
            if (topic == null)
                return RedirectPermanent("/Topics/Index");

            return View(topic);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public ActionResult Edit(Topic model)
        {

            var db = new ToDoContext();
            var topic = db.Topics.FirstOrDefault(x => x.Id == model.Id);
            if (topic == null)
            {
                ModelState.AddModelError("Id", "Тема не найдена");
            }
            if (!ModelState.IsValid)
                return View(model);

            MappingTopic(model, topic,db);


            db.Entry(topic).State = EntityState.Modified;
            db.SaveChanges();


            return RedirectPermanent("/Topics/Index");
        }

        private void MappingTopic(Topic sourse, Topic destination, ToDoContext db)
        {
            destination.Name = sourse.Name;
            
        }
    }
}