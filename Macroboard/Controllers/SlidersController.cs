using System.IO.Ports;
using Macroboard.Models;
using Macroboard.Models.Devices;
using MacroboardDriver.Device.Modules;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Macroboard.Controllers
{
    [ApiController]
    [Route("api/devices/")]
    public class SlidersController : Controller
    {

        /// <summary>
        /// Get all sliders for the specified device
        /// </summary>
        /// <param name="device_id"></param>
        /// <returns></returns>
        [HttpGet("{device_id}/sliders")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse<Slider[]>), StatusCodes.Status200OK)]
        public IActionResult GetList(uint device_id)
        {
            return Ok();
        }
        
        /// <summary>
        /// Get information about a specific slider
        /// </summary>
        /// <param name="device_id"></param>
        /// <returns></returns>
        [HttpGet("{device_id}/sliders/{slider_id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse<Slider[]>), StatusCodes.Status200OK)]
        public IActionResult GetSlider(uint device_id, uint slider_id)
        {
            return Ok();
        }
    }
}