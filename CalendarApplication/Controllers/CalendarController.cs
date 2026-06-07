using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using CalendarBL;
using CalendarModels;

namespace CalendarApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly CalendarAppService _appservice;

        public CalendarController()
        {
            _appservice = new CalendarAppService();
        }

        [HttpGet]
        public ActionResult<List<EventModels>> Get()
        {
                var events = _appservice.GetEventModels();
                return Ok(events);
        }

        [HttpGet("{name}")]
        public ActionResult<EventModels> Get(string name)
        {
            var eventModel = _appservice.GetEventModels(name);
            if (eventModel == null)
            {
                return NotFound();
            }
            return Ok(eventModel);
        }

        [HttpPost]
        public ActionResult Add(EventModels eventModel)
        {
            _appservice.AddEvent(eventModel.name, eventModel.date);
            return CreatedAtAction(nameof(Get), new { name = eventModel.name }, eventModel);
        }
    }
}   
