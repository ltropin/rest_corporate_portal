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

            return Ok(products.Select(x => {
                //var fullName = Constans.ApiUrl + Constans.FileDownloadPart + 
                var image = _context.Files.SingleOrDefault(y => x.ImageUrl.Contains(y.Name));
                var isFavorite = x.FavoriteProductsWorkers.Any(y => y.WorkerId == currentWorker.Id && y.FavoriteProductId == x.Id);

                return ResponseProductList.FromApiProduct(x, image: image, isFavorite: isFavorite);
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

        //// POST: api/Product
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Product>> PostProduct(Product product)
        //{
        //    _context.Products.Add(product);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        //}

        //// DELETE: api/Product/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteProduct(int id)
        //{
        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Products.Remove(product);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ProductExists(int id)
        //{
        //    return _context.Products.Any(e => e.Id == id);
        //}
    }
}
