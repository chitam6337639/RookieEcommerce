﻿using Microsoft.AspNetCore.Mvc;
using StandardLibrary.Product;
using WebAPIEcommerce.Interfaces;
using WebAPIEcommerce.Models.Entities;

namespace WebAPIEcommerce.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _productRepository.GetProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productRepository.GetProductId(id);

            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
		[HttpPost]
		public async Task<ActionResult<ProductDto>> CreateProduct(CreateProductDto createProductDto)
		{
			var addedProduct = await _productRepository.CreateProduct(createProductDto);
			return CreatedAtAction(nameof(GetProducts), new { id = addedProduct.ProductName }, addedProduct);
		}

		[HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, CreateProductDto createProductDto)
        {
            var result = await _productRepository.UpdateProduct(id, createProductDto);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productRepository.DeleteProduct(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

    }
}
