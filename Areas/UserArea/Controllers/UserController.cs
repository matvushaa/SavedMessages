using SavedMessages.BusinessLogic.Managers;
using SavedMessages.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SavedMessages.Areas.UserArea.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public IManager<User> usermanager;
        public IManager<Message> messagemanager;
        public IManager<Sticker> stickermanager;
        public IManager<FileLocation> filelockmanager;
        private Guid myguid = (Guid)System.Web.HttpContext.Current.Session["UserId"];

        public UserController() { }

        public UserController(IManager<Message> mesmanager, IManager<Sticker> smanager,
            IManager<User> simplemanager, IManager<FileLocation> fmanager)
        {
            messagemanager = mesmanager;
            stickermanager = smanager;
            usermanager = simplemanager;
            filelockmanager = fmanager;
        }

        [HttpGet]
        public ActionResult MyDetails()
        {
            return View(usermanager.GetById(myguid));
        }

        [HttpGet]
        public ActionResult StickerDetails(int id)
        {
            return View(stickermanager.GetById(id));
        }

        [HttpGet]
        public ActionResult DeleteMessage(int id)
        {
            return View(messagemanager.GetById(id));
        }

        [HttpPost]
        public ActionResult DeleteMessage(int id, Message message)
        {
            if (DateTime.Now <= message.Time.AddMinutes(15.0))
            {
                try
                {
                    messagemanager.Delete(id);
                    // TODO: Add delete logic here
                    return RedirectToAction("GetMyMessages");
                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }
            else
                return View("TimeIsOver");
        }

        [HttpGet]
        public ActionResult EditMessage(int id)
        {
            return View(messagemanager.GetById(id));
        }

        [HttpPost]
        public ActionResult EditMessage(int id, Message message)
        {
            if (DateTime.Now <= message.Time.AddMinutes(15.0))
            {
                try
                {
                    message.UserId = myguid;
                    message.IsSaved = false;
                    messagemanager.Edit(id, message);
                    // TODO: Add delete logic here
                    return RedirectToAction("GetMyMessages");
                }
                catch
                {
                    return RedirectToAction("Index");
                }
            }
            else
                return View("TimeIsOver");
        }
        [HttpGet]
        public ActionResult DeleteFile(int id)
        {
            return View(filelockmanager.GetById(id));
        }

        [HttpPost]
        public ActionResult DeleteFile(int id, FileLocation o)
        {
            if (DateTime.Now <= o.Time.AddMinutes(15.0))
            {
                try
                {
                    filelockmanager.Delete(id);
                    // TODO: Add delete logic here
                    return RedirectToAction("GetAllFiles");
                }
                catch
                {
                    return RedirectToAction("MyDetails");
                }
            }
            else
                return View("TimeIsOver");
        }
        [HttpGet]
        public ActionResult DeleteSticker(int id)
        {
            return View(stickermanager.GetById(id));
        }

        [HttpPost]
        public ActionResult DeleteSticker(int id, Sticker sticker)
        {
            try
            {
                stickermanager.Delete(id);
                // TODO: Add delete logic here
                return RedirectToAction("GetStickers");
            }
            catch
            {
                return RedirectToAction("MyDetails");
            }
        }

        [HttpGet]
        public ActionResult GetMyMessages()
        {
            return View(messagemanager.GetAll().Where(x => x.UserId == myguid));
        }
        [HttpGet]
        public ActionResult GetStickers()
        {
            return View(stickermanager.GetAll().Where(x => x.UserId == myguid));
        }
        [HttpGet]
        public ActionResult GetAllFiles()
        {
            return View(filelockmanager.GetAll().Where(x => x.UserId == myguid));
        }
        
        [HttpGet]
        public ActionResult CreateMessage()
        {
            var message = new Message();
            return View(message);
        }
        [HttpPost]
        public ActionResult CreateMessage(Message message, FileLocation file)//MyModel)
        {
            message.Time = DateTime.Now;
            message.IsSaved = false;
            message.UserId = myguid;
            messagemanager.Create(message);
            return RedirectToAction("GetMyMessages");
        }
        [HttpGet]
        public ActionResult CreateFile()
        {
            var file = new FileLocation();
            return View(file);
        }
        [HttpPost]
        public ActionResult CreateFile(FileLocation file, HttpPostedFileBase uploadImage)
        {
            file.Time = DateTime.Now;
            file.UserId = myguid;
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                file.File = imageData;

                filelockmanager.Create(file);
                return RedirectToAction("GetAllFiles");
            }
            return View(file);
        }
        [HttpGet]
        public ActionResult CreateSticker()
        {
            var file = new Sticker();
            return View(file);
        }
        [HttpPost]
        public ActionResult CreateSticker(Sticker sticker, HttpPostedFileBase uploadImage)
        {

            sticker.UserId = myguid;
            if (ModelState.IsValid && uploadImage != null)
            {
                byte[] imageData = null;
                // считываем переданный файл в массив байтов
                using (var binaryReader = new BinaryReader(uploadImage.InputStream))
                {
                    imageData = binaryReader.ReadBytes(uploadImage.ContentLength);
                }
                // установка массива байтов
                sticker.Stickers = imageData;

                stickermanager.Create(sticker);
                return RedirectToAction("GetStickers");
            }
            return View(sticker);
        }
    }
}