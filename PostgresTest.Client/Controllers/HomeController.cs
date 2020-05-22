using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PostgresTest.Business.Interfaces;
using PostgresTest.Domain.Models;

namespace PostgresTest.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostBusiness _postBusiness;
        private readonly ICommentBusiness _commentBusiness;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IPostBusiness postBusiness, ICommentBusiness commentBusiness)
        {
            _postBusiness = postBusiness;
            _commentBusiness = commentBusiness;
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Posts> posts;

            try
            {
                posts = _postBusiness.GetPosts().ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return View(posts);
        }

        public IActionResult Post()
        {
            var posts = _postBusiness.GetPosts();

            return View(posts);
        }

        public IActionResult Maintenance(int id = 0)
        {
            Posts post = _postBusiness.GetPost(id);

            return View(post);
        }

        [HttpPost]
        public IActionResult Maintenance(Posts entity)
        {
            entity.Publish_Date = DateTime.Now;

            if (entity.Id > 0)
            {
                _postBusiness.UpdatePost(entity);
            }
            else 
            {
                _postBusiness.CreatePost(entity);
            }

            return RedirectToAction("Post");
        }

        public IActionResult ReadPost(int id)
        {
            var post = _postBusiness.GetCompletePost(id);

            return View(post);
        }

        public IActionResult CreateComment(int id)
        {
            ViewBag.IdPost = id;

            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(Comments entity)
        {
            entity.Comment_Date = DateTime.Now;
            _commentBusiness.CreateComment(entity);

            return RedirectToAction("ReadPost", new { id = entity.IdPost });
        }
    }
}
