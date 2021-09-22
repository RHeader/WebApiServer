using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

using WebApiServer.Models;
using WebApiServer.Services;

namespace WebApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExchangeData : Controller
    {
        IDataServices data;
        public ExchangeData(IDataServices data)
        {
            this.data = data;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Message>> GetMessages()
        {
            return data.GetMessages().OrderByDescending(x => x.messageSendTime).ToList();
        }
        [HttpPost]
        public ActionResult<Message> PostMessage(Message message)
        {
            message.id = new Guid();
            if (message == null) { return BadRequest(); }
            data.Add(message);
            return Ok(message);
        }
    }
}
