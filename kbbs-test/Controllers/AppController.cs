using System.Collections.Generic;
using kbbs_test.Models;
using Microsoft.AspNetCore.Mvc;

namespace kbbs_test.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return Helper.GenerateBooks(25);
        }
    }
}
