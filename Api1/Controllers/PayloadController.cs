using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Api1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceBusMessaging;

namespace Api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayloadController : ControllerBase
    {
        private static readonly List<Payload> data = new List<Payload>
        {
            new Payload{ Id=1, Goals=3, Name="wow"},
            new Payload{ Id=2, Goals=4, Name="not so bad"},
        };

        private ServiceBusSender _serviceBusSender;

        public PayloadController(ServiceBusSender serviceBusSender)
        {
            _serviceBusSender = serviceBusSender;
        }


        [HttpPost]
        [ProducesResponseType(typeof(Payload), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Payload), StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Create([FromBody][Required]Payload request)
        {
            if (data.Any(d => d.Id == request.Id))
            {
                return Conflict($"data with id {request.Id} already exists");
            }

            data.Add(request);

            // Send this to the bus for the other services
            await _serviceBusSender.SendMessage(new MyPayload
            {
                Goals = request.Goals,
                Name = request.Name,
                Delete = false
            });

            return Ok(request);
        }
    }
}