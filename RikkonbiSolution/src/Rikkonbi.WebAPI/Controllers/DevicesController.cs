using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rikkonbi.Core.Entities;
using Rikkonbi.Core.Interfaces;
using Rikkonbi.Core.SharedKernel;
using Rikkonbi.WebAPI.Helpers;
using Rikkonbi.WebAPI.ViewModels;

namespace Rikkonbi.WebAPI.Controllers
{
    public class DevicesController : BaseApiController
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDeviceCategoryRepository _deviceCategoryRepository;
        private readonly IMapper _mapper;

        public DevicesController(IDeviceRepository deviceRepository,
                                 IDeviceCategoryRepository deviceCategoryRepository,
                                 IMapper mapper)
        {
            _deviceRepository = deviceRepository;
            _deviceCategoryRepository = deviceCategoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<DeviceViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public IActionResult Get()
        {
            try
            {
                // Get all Devices
                var devices = _deviceRepository.GetMany(d => d.IsDeleted == false).OrderByDescending(d => d.CreatedOn).ToList();

                if (devices.Count == 0) return NotFound();

                var deviceViewModels = _mapper.Map<List<DeviceViewModel>>(devices);
                return Ok(new { Total = deviceViewModels.Count, Items = deviceViewModels });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                //get device by id
                var device = _deviceRepository.GetById(id);

                if (device == null) return NotFound();

                var deviceViewModel = _mapper.Map<DeviceViewModel>(device);
                return Ok(deviceViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = Policies.ADMIN_OR_SALES)]
        public IActionResult Post([FromBody] CreateDeviceViewModel deviceViewModel)
        {
            try
            {
                deviceViewModel.Name = deviceViewModel.Name.Trim();

                if (_deviceRepository.List(d => d.Name.Equals(deviceViewModel.Name, StringComparison.OrdinalIgnoreCase)).Any())
                {
                    return BadRequest("Duplicate device name!");
                }

                var device = _mapper.Map<Device>(deviceViewModel);

                device.IsDeleted = false;
                device.IsBorrowed = false;
                device.CreatedOn = DateTime.Now;
                device.CreatedBy = User.Identity.Name;

                _deviceRepository.Add(device);

                string qrCodeFileName = $"P{device.Id}_{Guid.NewGuid().ToString().Replace("-", "")}.png";
                string qrCodeContent = "{\"objectType\":\"" + AssetClassifications.DEVICE + "\",\"objectId\":" + device.Id + "}";

                device.QrCodeImageUrl = QRCodeHelper.GenerateQrCodeImage(qrCodeFileName, qrCodeContent);
                device.QrCodeContent = qrCodeContent;

                _deviceRepository.Update(device);
                return Ok();
            }
            catch (Exception EX)
            {
                return StatusCode(500, EX.Message);
            }
        }

        [HttpPut]
        [Authorize(Policy = Policies.ADMIN_OR_SALES)]
        public IActionResult Put([FromBody] EditDeviceViewModel deviceViewModel)
        {
            try
            {
                var device = _deviceRepository.GetById(deviceViewModel.Id);

                if (device == null)
                {
                    return BadRequest($"The deviceId ({deviceViewModel.Id}) does not exists!");
                }

                deviceViewModel.Name = deviceViewModel.Name.Trim();

                if (!deviceViewModel.Name.Equals(device.Name, StringComparison.OrdinalIgnoreCase)
                    && _deviceRepository.List(p => p.Name.Equals(deviceViewModel.Name, StringComparison.OrdinalIgnoreCase)).Any())
                {
                    return BadRequest("Duplicate device name!");
                }

                device.Name = deviceViewModel.Name;
                device.Description = deviceViewModel.Description;
                device.ImageUrl = deviceViewModel.ImageUrl;
                device.DeviceCategoryId = deviceViewModel.DeviceCategoryId;
                device.UpdatedOn = DateTime.Now;
                device.UpdatedBy = User.Identity.Name;

                _deviceRepository.Update(device);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = Policies.ADMIN_OR_SALES)]
        public IActionResult Delete(int id)
        {
            try
            {
                var device = _deviceRepository.GetSingleByCondition(d => d.Id == id && d.IsDeleted == false);

                if (device == null) return NotFound();

                device.IsDeleted = true;

                _deviceRepository.Update(device);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}