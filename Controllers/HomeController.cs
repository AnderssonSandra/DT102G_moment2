using DT102G_moment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace DT102G_moment2.Controllers
{
    public class HomeController : Controller
    {
        string filePath = @"data.json"; // filepath to json-file

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Blog()
        {
            string name = HttpContext.Session.GetString("name");

            if(name != null)
            {
                ViewBag.IntroText = "Hej " + name + "! Här kan du läsa dina och andras blogginlägg";
            } else
            {
                ViewBag.IntroText = "Här kan du läsa alla blogginlägg";
            }
            var blogPostList = JsonDeserialize();
            return View(blogPostList);
        }

        public IActionResult Rules()
        {
            return View();
        }

        public IActionResult WriteBlogPost()
        {
            return View();
        }

        [HttpPost]
        public IActionResult WriteBlogPost(BlogPostModel model)
        {
            //set add current date to model.
            model.Date = DateTime.Now;

            //check if form data is correct
            if (ModelState.IsValid)
            {
                //set session cookie
                HttpContext.Session.SetString("name", model.Name);

                //spara i JSON skicka ok meddelande
                var blogPostList = JsonDeserialize();
                blogPostList.Add(model);

                JsonSerialize(blogPostList);

                ModelState.Clear();

                //get name
                string name = HttpContext.Session.GetString("name");

                ViewBag.PostCreated = "Tack " + name + "! Ditt inlägg är nu publicerat.";
            }
            return View();
        }

        //deserialise data
        public List<BlogPostModel> JsonDeserialize()
        {
            //check if JSON file exist and create if not
            if (!System.IO.File.Exists(filePath))
            {
                return new List<BlogPostModel>();
            }
            else
            {
                //read json file
                var jsonData = System.IO.File.ReadAllText(filePath);

                // Deserialise json file to C# object and create list
                var blogPostList = JsonConvert.DeserializeObject<List<BlogPostModel>>(jsonData)
                ?? new List<BlogPostModel>();

                //order list by date
                var sortedList = blogPostList.OrderByDescending(blogPost => blogPost.Date).ToList();

                //return list
                return sortedList;
            }
        }

        //serialize data
        public void JsonSerialize(List<BlogPostModel> blogPostList)
        {
            if (!System.IO.File.Exists(filePath))
            {
                FileStream fs = System.IO.File.Create(filePath);
            }
            string jsonData = JsonConvert.SerializeObject(blogPostList);
            System.IO.File.WriteAllText(filePath, jsonData);
        }

        public IActionResult SubMenu()
        {
            return PartialView("SubMenu");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
