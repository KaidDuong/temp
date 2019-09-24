using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rikkonbi.Core.Entities;
using Rikkonbi.Core.Interfaces;
using Rikkonbi.Infrastructure.Identity;
using Rikkonbi.WebAPI.Extensions;
using Rikkonbi.WebAPI.Helpers;
using Rikkonbi.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rikkonbi.WebAPI.Controllers
{
    public class OrdersController : BaseApiController
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public OrdersController(IOrderRepository orderRepository,
            IOrderDetailRepository orderDetailRepository,
            IPaymentStatusRepository paymentStatusRepository,
            IProductRepository productRepository,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(Policy = Policies.ADMIN_OR_SALES)]
        public IActionResult Get()
        {
            try
            {
                // Get all Orders
                var orders = _orderRepository.ListOrder(x => true);

                if (orders.Count == 0) return Ok();

                var orderViewModels = _mapper.Map<List<OrderViewModel>>(orders);

                ApplicationUser user;
                foreach (var orderViewModel in orderViewModels)
                {
                    user = _userManager.FindByIdAsync(orderViewModel.UserId).Result;

                    if (user == null) continue;

                    orderViewModel.FullName = user.FullName;
                    orderViewModel.Email = user.Email;
                }

                return Ok(orderViewModels);
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
                // Get order by id  
                var order = _orderRepository.ListOrder(x => x.Id == id).FirstOrDefault();

                if (order == null) return NotFound();

                var orderViewModel = _mapper.Map<OrderViewModel>(order);

                ApplicationUser user = _userManager.FindByIdAsync(orderViewModel.UserId).Result;
                orderViewModel.FullName = user.FullName;
                orderViewModel.Email = user.Email;

                return Ok(orderViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("history")]
        public IActionResult History()
        {
            try
            {
                string userId = User.GetUserId();
                int maxOrderHistoryDays = 60;   // Only show order history for 60 days (2 months)
                DateTime minOrderDate = DateTime.Now.AddDays(maxOrderHistoryDays * -1);
                minOrderDate = new DateTime(minOrderDate.Year, minOrderDate.Month, minOrderDate.Day, 0, 0, 0);

                var orders = _orderRepository.ListOrder(x => x.UserId.Equals(userId) && x.OrderDate >= minOrderDate);

                if (orders.Count == 0) return Ok();

                var originalOrderViewModels = _mapper.Map<List<OrderHistoryViewModel>>(orders);

                // Remove the deleted order items
                foreach (var order in originalOrderViewModels)
                {
                    order.Items.RemoveAll(x => x.IsDeleted == true);
                }

                // Remove the orders if it has no items
                var orderViewModels = originalOrderViewModels.Where(x => x.Items.Count > 0).ToList();

                return Ok(orderViewModels);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] CreateOrderViewModel orderViewModel)
        {
            try
            {
                foreach (var orderItem in orderViewModel.Items)
                {
                    if (_productRepository.GetById(orderItem.ProductId) == null)
                    {
                        return BadRequest($"{orderItem.ProductName} does not exists!");
                    }
                }

                var order = _mapper.Map<Order>(orderViewModel);
                order.UserId = User.GetUserId();
                order.RegionId = 1;
                order.OrderDate = DateTime.Now;
                order.PaymentStatusId = (int)PAYMENT_STATUS.Unpaid;
                order.CreatedOn = DateTime.Now;
                order.CreatedBy = User.Identity.Name;

                _orderRepository.Add(order);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Policy = Policies.ADMIN_OR_SALES)]
        public IActionResult Put([FromBody] EditOrderViewModel orderViewModel)
        {
            try
            {
                var order = _orderRepository.ListOrder(x => x.Id == orderViewModel.Id).FirstOrDefault();

                if (order == null)
                {
                    return BadRequest($"The OrderId ({orderViewModel.Id}) does not exists!");
                }

                var newOrderDetails = _mapper.Map<List<OrderDetail>>(orderViewModel.Items);
                order.Items.Clear();

                foreach (var item in newOrderDetails)
                {
                    order.Items.Add(item);
                }

                order.UserId = orderViewModel.UserId;
                order.RegionId = orderViewModel.RegionId;
                order.OrderDate = orderViewModel.OrderDate;
                order.PaymentStatusId = orderViewModel.PaymentStatusId;
                order.UpdatedOn = DateTime.Now;
                order.UpdatedBy = User.Identity.Name;

                _orderRepository.Update(order);

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
                var order = _orderRepository.GetById(id);

                if (order == null)
                {
                    return BadRequest($"The OrderId ({id}) does not exists!");
                }

                _orderRepository.Delete(order);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("filter")]
        [Authorize(Policy = Policies.ADMIN_OR_SALES)]
        public IActionResult Filter([FromQuery] OrderFilterViewModel filterModel)
        {
            try
            {
                var ordersEntity = _orderRepository.ListOrder(x => (!filterModel.OrderDateTo.HasValue || x.OrderDate < filterModel.OrderDateTo.Value.AddDays(1))
                                                                    && (!filterModel.OrderDateFrom.HasValue || x.OrderDate >= filterModel.OrderDateFrom.Value)
                                                                    && (!filterModel.PaymentStatusId.HasValue || x.PaymentStatusId == filterModel.PaymentStatusId.Value)
                                                                    && (!filterModel.ProductId.HasValue || x.Items.Where(y => y.ProductId == filterModel.ProductId.Value).Any())
                                                                    && (!filterModel.OrderId.HasValue || x.Id == filterModel.OrderId.Value)
                                                            );

                if (ordersEntity.Count == 0) return NotFound();

                var orderViewModel = _mapper.Map<List<OrderViewModel>>(ordersEntity);

                return Ok(orderViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("history/{id}")]
        public IActionResult DeleteHistory(int id)
        {
            try
            {
                var orderDetail = _orderDetailRepository.GetById(id);

                if (orderDetail == null)
                {
                    return BadRequest($"The OrderDetailId ({id}) does not exists!");
                }

                orderDetail.IsDeleted = true;
                _orderDetailRepository.Update(orderDetail);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}