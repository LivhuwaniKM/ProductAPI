﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Helpers;
using ProductAPI.Models;
using System.Security.Claims;

namespace ProductAPI.Controllers
{
    public class ProductController(DataContext _db, IResponseHelper _response) : BaseApiController
    {
        [HttpPost("create")]
        public async Task<ActionResult<ApiResponse<Product>>> AddProductAsync(Product model)
        {
            if (string.IsNullOrWhiteSpace(model.Name) || model.Price <= 0 || model.Quantity < 0)
                return _response.CreateResponse<Product>(false, 400, "Invalid product data.", null);

            model.UserId = GetUserId();

            await _db.Products.AddAsync(model);
            await _db.SaveChangesAsync();

            return _response.CreateResponse(true, 201, "Product added successfully.", model);
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<ApiResponse<Product>>> UpdateProductAsync(int id, Product updated)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null || product.UserId != GetUserId())
                return _response.CreateResponse<Product>(false, 404, "Product not found or access denied.", null);

            product.Name = updated.Name;
            product.Price = updated.Price;
            product.Quantity = updated.Quantity;

            await _db.SaveChangesAsync();

            return _response.CreateResponse(true, 200, "Product updated.", product);
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<ApiResponse<string>>> DeleteProductAsync(int id)
        {
            var product = await _db.Products.FindAsync(id);
            if (product == null || product.UserId != GetUserId())
                return _response.CreateResponse<string>(false, 404, "Product not found or access denied.", null);

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return _response.CreateResponse(true, 200, "Product deleted.", "Product deleted");
        }

        [HttpGet("list")]
        public async Task<ActionResult<ApiResponse<List<Product>>>> GetAllProductsAsync()
        {
            var products = await _db.Products.ToListAsync();
            return _response.CreateResponse(true, 200, "Products retrieved.", products);
        }

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
