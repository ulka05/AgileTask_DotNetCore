using AgileInfoTask.DataAccess;
using AgileInfoTask.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AgileInfoTask.Controllers
{
    /// <summary>
    /// Task 7 we used to here to show methods,reponse,parameters etc.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        public readonly ProductDbContext _context;

        public ProductController(ProductDbContext context)
        {
            _context = context;
        }

        //Returns all records
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>>GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return await _context.Products.ToListAsync();
        }

        //Returns specific records by id
       
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProductById(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }


        //Inserts data into the database and returns details of inserted Product ID.
        
        [HttpPost]
        public async Task<ActionResult<ProductModel>> InsertProduct(ProductModel product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(InsertProduct), new { id = product.ProductID }, product);
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductModel product)
        {
            var user = HttpContext.Items["User"] as UserModel;
            if (id != product.ProductID)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified; // Which means we are going to update our database table

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!ProductAvailable(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // This method will tell us whether id exists in DB table or not
        private bool ProductAvailable(int id)
        {
            return (_context.Products?.Any(p => p.ProductID == id)).GetValueOrDefault();
        }


        //This method will check and delete the data from database
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);

            await _context.SaveChangesAsync();

            return Ok();
        }

        
    }
}
