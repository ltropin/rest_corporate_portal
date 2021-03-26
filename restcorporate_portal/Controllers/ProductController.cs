using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restcorporate_portal.Exceptions;
using restcorporate_portal.Models;
using restcorporate_portal.ResponseModels;
using restcorporate_portal.Utils;
using Swashbuckle.AspNetCore.Annotations;

namespace restcorporate_portal.Controllers
{
    static class ProductErrorsMessages
    {
        public const string NotEnoughCoins = "NOT_ENOUGH_COINS";
        public const string ProductAlreadyFavorite = "PRODUCT_ALREADY_FAVORITE";
        public const string ProductNotContainsFavorite = "PRODUCT_NOT_CONTAINS_IN_FAVORITES";
    }

    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly corporateContext _context;

        public ProductController(corporateContext context)
        {
            _context = context;
        }

        // GET: api/products
        [SwaggerOperation(
            Summary = "Получить список всех товаров",
            Tags = new string[] { "Магазин" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно", type: typeof(List<ResponseProductList>))]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ResponseProductList>>> GetProducts()
        {
            var email = User.Identity.Name;
            var currentWorker = await _context.Workers.SingleOrDefaultAsync(x => x.Email == email);

            var products = await _context.Products
                .Include(x => x.FavoriteProductsWorkers)
                .ToListAsync();

            return Ok(products.Select(x =>
            {
                //var fullName = Constans.ApiUrl + Constans.FileDownloadPart + 
                var image = _context.Files.SingleOrDefault(y => x.ImageUrl.Contains(y.Name));
                var isFavorite = x.FavoriteProductsWorkers.Any(y => y.WorkerId == currentWorker.Id && y.FavoriteProductId == x.Id);

                return ResponseProductList.FromApiProduct(x, image: image, isFavorite: isFavorite, isCanBuy: currentWorker.Balance >= x.Price);
            }).ToList());
        }

        //// PUT: api/Product/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutProduct(int id, Product product)
        //{
        //    if (id != product.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(product).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ProductExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/products/buy/
        [SwaggerOperation(
            Summary = "Купить товар",
            Tags = new string[] { "Магазин" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("buy/{productId}")]
        public async Task<IActionResult> BuyProduct(int productId)
        {
            var email = User.Identity.Name;
            var currentProduct = await _context.Products.SingleAsync(x => x.Id == productId);
            var currentUser = await _context.Workers.SingleAsync(x => x.Email == email);
            _context.PreviousProductsWorkers.Add(new PreviousProductsWorker { PreviousProductId = productId, WorkerId = currentUser.Id });
            var newRecords = await _context.SaveChangesAsync();

            if (currentUser.Balance > currentProduct.Price && newRecords > 0)
            {
                currentUser.Balance -= currentProduct.Price;
                _context.Workers.Update(currentUser);
                await _context.SaveChangesAsync();
                //_context.Entry(currentUser).State = EntityState.Modified;
                return Ok();
            }
            else
            {
                return NotFound(new ExceptionInfo
                {
                    Message = ProductErrorsMessages.NotEnoughCoins,
                    Description = "Чел ты бомж, держу в курсе"
                });
            }
        }

        // POST: api/products/favorite/
        [SwaggerOperation(
            Summary = "Добавить в избранное",
            Tags = new string[] { "Магазин" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost("favorite/{productId}")]
        public async Task<IActionResult> AddToFavoriteProduct(int productId)
        {
            var email = User.Identity.Name;
            var currentProduct = await _context.Products.SingleAsync(x => x.Id == productId);
            var currentUser = await _context.Workers.SingleAsync(x => x.Email == email);
            var productAlreadyContains = _context.FavoriteProductsWorkers
                .Any(x => x.FavoriteProductId == productId && x.WorkerId == currentUser.Id);

            if (!productAlreadyContains)
            {
                _context.FavoriteProductsWorkers.Add(new FavoriteProductsWorker { FavoriteProductId = productId, WorkerId = currentUser.Id });
                await _context.SaveChangesAsync();

                return Ok();
            }

            else
            {
                return NotFound(new ExceptionInfo
                {
                    Message = ProductErrorsMessages.ProductAlreadyFavorite,
                    Description = "Товар уже в избранном"
                });
            }
        }

        // DELETE: api/products/5
        [SwaggerOperation(
            Summary = "Удалить из избранного",
            Tags = new string[] { "Магазин" }
        )]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Ошибка", type: typeof(ExceptionInfo))]
        [SwaggerResponse(StatusCodes.Status200OK, "Успешно")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpDelete("favorite/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var email = User.Identity.Name;
            var currentProduct = await _context.Products.SingleAsync(x => x.Id == productId);
            var currentUser = await _context.Workers.SingleAsync(x => x.Email == email);
            var productAlreadyContains = _context.FavoriteProductsWorkers
                .Any(x => x.FavoriteProductId == productId && x.WorkerId == currentUser.Id);

            if (productAlreadyContains)
            {
                _context.Products.Remove(currentProduct);
                await _context.SaveChangesAsync();
                return Ok();
            }
            else
            {
                return NotFound(new ExceptionInfo
                {
                    Message = ProductErrorsMessages.ProductNotContainsFavorite,
                    Description = "Товара уже нет в избранном"
                });
            }
        }
    }
}
