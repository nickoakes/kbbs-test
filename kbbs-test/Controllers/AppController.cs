using System;
using System.Collections.Generic;
using System.Linq;
using kbbs_test.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace kbbs_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController : ControllerBase
    {
        [HttpGet]
        public List<Book> Get(int page)
        {
            try
            {
                if (HttpContext.Session.GetString("Books") == null)
                {
                    List<Book> allBooks = Helper.GenerateBooks(250);

                    HttpContext.Session.SetString("Books", JsonConvert.SerializeObject(allBooks)); //To keep the same overall set of books between requests
                }

                int index = page * 25;

                List<Book> books = JsonConvert.DeserializeObject<List<Book>>(HttpContext.Session.GetString("Books")).GetRange(index, 25);

                return books;
            } 
            catch (Exception ex)
            {
                Helper.LogException(ex);

                return new List<Book>();
            }
        }

        [HttpGet("BookCount")]
        public int BookCount()
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Book>>(HttpContext.Session.GetString("Books")).Count;
            } 
            catch(Exception ex)
            {
                Helper.LogException(ex);

                return 0;
            }
        }

        [HttpGet("BookSearch")]
        public List<Book> BookSearch(string searchTerm, string searchType)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<Book>>(HttpContext.Session.GetString("Books")).Where(x => x.GetType()
                                                                                                                     .GetProperty(searchType)
                                                                                                                     .GetValue(x)
                                                                                                                     .ToString()
                                                                                                                     .ToLower()
                                                                                                                     .Contains(searchTerm)).ToList();
            }
            catch (Exception ex)
            {
                Helper.LogException(ex);

                return new List<Book>();
            }
        }
    }
}
