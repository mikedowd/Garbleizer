using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace garble.Controllers
{
	/// <summary>
	/// API for garble requests
	/// </summary>
    [Route("api/[controller]")]
    public class GarbleController : Controller
    {
		/// <summary>
		/// Returns a garbled version of the word passed in.
		/// </summary>
		/// <param name="word">The word</param>
		/// <returns></returns>
        // GET api/values/5
        [HttpGet("{word}")]
        public string Get(string word)
        {
			return string.Format( $"oiuwq{word}erkjs" );
        }
    }
}
