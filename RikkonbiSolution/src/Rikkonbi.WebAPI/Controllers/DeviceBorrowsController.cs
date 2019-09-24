using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Rikkonbi.Core.Entities;
using Rikkonbi.Core.Interfaces;
using Rikkonbi.WebAPI.Helpers;
using Rikkonbi.WebAPI.ViewModels;

namespace Rikkonbi.WebAPI.Controllers
{
    public class DeviceBorrowsController : ControllerBase
    {
        private IBorrowRepository _borrowRepository;
        private IDeviceRepository _deviceRepository;
        private IMapper _mapper;
        private IUnitOfWork _unitOfWork;

        public DeviceBorrowsController(
            IBorrowRepository borrowRepository,
            IDeviceRepository deviceRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _borrowRepository = borrowRepository;
            _deviceRepository = deviceRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        public IActionResult Get(string userId)

        {
            try
            {
                // Get history of user

                var deviceBorrows = _borrowRepository.GetDeviceBorrowsBy(userId);

                var deviceBorrowViewModels = _mapper.Map<List<DeviceBorrowViewModel>>(deviceBorrows);
                return Ok(new { UserName = User.Identity.Name, Total = deviceBorrowViewModels.Count, Items = deviceBorrowViewModels });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /*
         Borrow
         */

        [HttpPost]
        public IActionResult Post([FromBody] CreateDeviceBorrowViewModel deviceBorrowViewModel)
        {
            try
            {
                var device = _deviceRepository.GetSingleByCondition(d =>
                    d.Id == deviceBorrowViewModel.DeviceId
                    && d.IsBorrowed == false
                );

                if (device == null) return StatusCode(200, "The current device is unavailable !");

                var borrow = _mapper.Map<Borrow>(deviceBorrowViewModel);
                borrow.CreatedBy = User.Identity.Name;
                borrow.CreatedOn = DateTime.Now;
                borrow.BorrowOn = DateTime.Now;
                borrow.IsDeleted = false;
                borrow.Status = true;
                borrow.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

                _borrowRepository.Add(borrow);

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /*
 Return
     */

        [HttpPut("{deviceId}")]
        public IActionResult Put(int deviceId)
        {
            try
            {
                var borrow = _borrowRepository.GetSingleByCondition(b => b.IsDeleted == false && b.DeviceId == deviceId && b.Status == true);

                if (borrow == null) return NotFound();

                borrow.ReturnOn = DateTime.Now;
                borrow.Status = false;

                _borrowRepository.UpdateAndNotSave(borrow);

                var device = _deviceRepository.GetById(deviceId);

                device.IsBorrowed = false;

                _deviceRepository.UpdateAndNotSave(device);

                _unitOfWork.Commit();

                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /*
             * delete borrow by user
             */

        [HttpDelete("{borrowId}")]
        [Authorize(Policy = Policies.ADMIN_OR_SALES)]
        public IActionResult Delete(int borrowId)
        {
            try
            {
                var borrow = _borrowRepository.GetSingleByCondition(b => b.Id == borrowId && b.IsDeleted == false && b.Status == false);

                if (borrow == null) return NotFound();

                borrow.IsDeleted = true;
                borrow.UpdatedBy = User.Identity.Name;
                borrow.UpdatedOn = DateTime.Now;

                _borrowRepository.Update(borrow);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        /*
         * get all borrowing device's historys
         */
        // [HttpGet]
        // [Authorize(Policy = Policies.ADMIN_OR_SALES)]
        // public IActionResult Get()
        //{
        //    var histories= _borrowRepository.Get
        //}
    }
}