using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Helpers;
using ProductAPI.Models;
using System.Security.Claims;

namespace ProductAPI.Controllers
{
    public class CheckoutController(DataContext _db, IResponseHelper _response) : BaseController
    {
        [HttpPost("start")]
        public async Task<ActionResult<ApiResponse<Checkout>>> StartCheckoutAsync(List<CheckoutItem> items)
        {
            if (items == null || items.Count == 0)
                return _response.CreateResponse<Checkout>(false, 400, "Empty checkout items.", null);

            var userId = GetUserId();

            var existing = await _db.Checkouts.FirstOrDefaultAsync(c => c.UserId == userId && !c.IsCompleted);

            if (existing != null)
                return _response.CreateResponse<Checkout>(false, 409, "Existing open checkout found.", null);

            var checkout = new Checkout { UserId = userId };

            foreach (var item in items)
            {
                var product = await _db.Products.FindAsync(item.ProductId);

                if (product == null)
                    return _response.CreateResponse<Checkout>(false, 404, $"Product ID {item.ProductId} not found.", null);

                if (item.Quantity <= 0 || item.Quantity > product.Quantity)
                    return _response.CreateResponse<Checkout>(false, 400, $"Invalid quantity for product {product.Name}.", null);

                checkout.Items.Add(new CheckoutItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
            }

            await _db.Checkouts.AddAsync(checkout);
            await _db.SaveChangesAsync();

            return _response.CreateResponse(true, 201, "Checkout started.", checkout);
        }

        [HttpPost("complete")]
        public async Task<ActionResult<ApiResponse<Checkout>>> CompleteCheckoutAsync()
        {
            var userId = GetUserId();
            var checkout = await _db.Checkouts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsCompleted);

            if (checkout == null)
                return _response.CreateResponse<Checkout>(false, 404, "No open checkout found.", null);

            foreach (var item in checkout.Items)
            {
                var product = await _db.Products.FindAsync(item.ProductId);
                if (product == null || product.Quantity < item.Quantity)
                    return _response.CreateResponse<Checkout>(false, 400, $"Product '{product?.Name}' no longer available in sufficient quantity.", null);

                product.Quantity -= item.Quantity;
            }

            checkout.IsCompleted = true;
            await _db.SaveChangesAsync();

            return _response.CreateResponse(true, 200, "Checkout completed.", checkout);
        }

        [HttpPut("modify")]
        public async Task<ActionResult<ApiResponse<Checkout>>> ModifyCheckoutAsync(List<CheckoutItem> updatedItems)
        {
            var userId = GetUserId();
            var checkout = await _db.Checkouts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsCompleted);

            if (checkout == null)
                return _response.CreateResponse<Checkout>(false, 404, "No open checkout found.", null);

            checkout.Items.Clear();

            foreach (var item in updatedItems)
            {
                var product = await _db.Products.FindAsync(item.ProductId);

                if (product == null || item.Quantity <= 0 || item.Quantity > product.Quantity)
                    return _response.CreateResponse<Checkout>(false, 400, $"Invalid item or quantity for product ID {item.ProductId}.", null);

                checkout.Items.Add(new CheckoutItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                });
            }

            await _db.SaveChangesAsync();
            return _response.CreateResponse(true, 200, "Checkout modified.", checkout);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<ApiResponse<bool>>> DeleteCheckoutAsync()
        {
            var userId = GetUserId();

            var checkout = await _db.Checkouts
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsCompleted);

            if (checkout == null)
                return _response.CreateResponse(false, 404, "No open checkout to delete.", false);

            _db.Checkouts.Remove(checkout);
            await _db.SaveChangesAsync();

            return _response.CreateResponse(true, 200, "Checkout deleted.", true);
        }

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
