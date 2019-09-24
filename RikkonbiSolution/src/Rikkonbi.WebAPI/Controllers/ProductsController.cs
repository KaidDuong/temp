using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rikkonbi.Core.Entities;
using Rikkonbi.Core.Interfaces;
using Rikkonbi.Core.SharedKernel;
using Rikkonbi.WebAPI.Helpers;
using Rikkonbi.WebAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rikkonbi.WebAPI.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductsController(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                // Get all products
                var products = _productRepository.ListAll();

                if (products.Count == 0) return Ok();

                var productViewModels = _mapper.Map<List<ProductViewModel>>(products);
                return Ok(new { Total = productViewModels.Count, Items = productViewModels });
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
                // Get product by id
                var product = _productRepository.GetById(id);

                if (product == null) return NotFound();

                var productViewModel = _mapper.Map<ProductViewModel>(product);
                return Ok(productViewModel);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Authorize(Policy = Policies.ADMIN_OR_SALES)]
        public IActionResult Post([FromBody] CreateProductViewModel productViewModel)
        {
            try
            {
                productViewModel.Name = productViewModel.Name.Trim();

                if (_productRepository.List(p => p.Name.Equals(productViewModel.Name, StringComparison.OrdinalIgnoreCase)).Any())
                {
                    return BadRequest("Duplicate product name!");
                }

                var product = _mapper.Map<Product>(productViewModel);
                product.MaxOrderQuantity = 10;
                product.CreatedOn = DateTime.Now;
                product.CreatedBy = User.Identity.Name;

                _productRepository.Add(product);

                string qrCodeFileName = $"P{product.Id}_{Guid.NewGuid().ToString().Replace("-", "")}.png";
                string qrCodeContent = "{\"objectType\":\"" + AssetClassifications.PRODUCT + "\",\"objectId\":" + product.Id + "}";

                product.QrCodeContent = qrCodeContent;
                product.QrCodeImageUrl = QRCodeHelper.GenerateQrCodeImage(qrCodeFileName, qrCodeContent);

                _productRepository.Update(product);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Authorize(Policy = Policies.ADMIN_OR_SALES)]
        public IActionResult Put([FromBody] EditProductViewModel productViewModel)
        {
            try
            {
                var product = _productRepository.GetById(productViewModel.Id);

                if (product == null)
                {
                    return BadRequest($"The ProductId ({productViewModel.Id}) does not exists!");
                }

                productViewModel.Name = productViewModel.Name.Trim();

                if (!productViewModel.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase)
                    && _productRepository.List(p => p.Name.Equals(productViewModel.Name, StringComparison.OrdinalIgnoreCase)).Any())
                {
                    return BadRequest("Duplicate product name!");
                }

                product.Name = productViewModel.Name;
                product.Description = productViewModel.Description;
                product.Price = productViewModel.Price;
                product.ImageUrl = productViewModel.ImageUrl;
                product.CategoryId = productViewModel.DeviceCategoryId;
                product.UpdatedOn = DateTime.Now;
                product.UpdatedBy = User.Identity.Name;

                _productRepository.Update(product);

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
                var product = _productRepository.GetById(id);

                if (product == null) return NotFound();

                _productRepository.Delete(product);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}