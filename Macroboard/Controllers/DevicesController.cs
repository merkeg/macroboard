using System.Threading.Tasks;
using Macroboard.Driver;
using Macroboard.Extensions;
using Macroboard.Models;
using Macroboard.Models.Devices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Macroboard.Controllers
{
    /// <summary>
    /// Devices controller
    /// </summary>
    [ApiController]
    [Route("api/devices")]
    public class DevicesController: Controller
    {
        private readonly IDeviceDriver _deviceDriver;

        public DevicesController(IDeviceDriver deviceDriver)
        {
            _deviceDriver = deviceDriver;
        }
        
        /// <summary>
        /// Get all devices the system is connected to
        /// </summary>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse<DeviceInfo[]>), StatusCodes.Status200OK)]
        public IActionResult GetList()
        {
            return Ok(_deviceDriver.ListDevices());
        }

        /// <summary>
        /// Get information about a specific device
        /// </summary>
        /// <param name="device_id"></param>
        /// <returns></returns>
        [HttpGet("{device_id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse<DeviceInfo>), StatusCodes.Status200OK)]
        public IActionResult GetDeviceInfo(uint device_id)
        {
            return Ok();
        }

        /// <summary>
        /// Add a new device
        /// </summary>
        /// <returns></returns>
        [HttpPost()]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse<DeviceInfo>), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddDevice(NewDeviceRequest deviceRequest)
        {
            DeviceInfo deviceInfo = await this._deviceDriver.ConnectToDevice(deviceRequest.Port);
            if (deviceInfo == null)
            {
                return BadRequest(this.ConstructErrorResponse("Port used or non existent"));
            }

            deviceInfo.Name = deviceRequest.Name;
            return Created(deviceInfo.Id.ToString(), (DeviceInfo) deviceInfo);
        }
        
    }
}