using System;
using System.Threading.Tasks;
using Macroboard.Extensions;
using Macroboard.Models;
using Macroboard.Models.Devices;
using MacroboardDriver.Device;
using MacroboardDriver.Device.Modules;
using MacroboardDriver.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Macroboard.Controllers
{
    /// <summary>
    /// Devices controller
    /// </summary>
    [ApiController]
    [Route("api/devices")]
    public class DevicesController: Controller
    {

        
        /// <summary>
        /// Get all devices the system is connected to
        /// </summary>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BaseResponse<DeviceInfo[]>), StatusCodes.Status200OK)]
        public IActionResult GetList()
        {
            return Ok();
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
        [ProducesResponseType(typeof(BaseResponse<Device>), StatusCodes.Status201Created)]
        public async Task<IActionResult> AddDevice(NewDeviceRequest deviceRequest)
        {
            Device device = Device.InstantiateDeviceFromPort(deviceRequest.Port, 115200);
            
            if (device == null)
            {
                return BadRequest(this.ConstructErrorResponse("Port used or not existing"));
            }

            device.EntityChangeEvent += this.HandleChange;
            device.StartMessageLoop();

            device.Add(Capability.Name, deviceRequest.Name);
            return Created(device.Uid.ToString(), null);
        }

        private void HandleChange(ModuleType type, byte entityId, byte value)
        {
            Log.Information("< Type: {0} ID: {1} Value: {2}", type, entityId, value);
        }
        
    }
}