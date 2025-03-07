using Microsoft.AspNetCore.Mvc;
using GoodsManagementAPI.Data;
using GoodsManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GoodsManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase
    {
        private readonly GoodsDbContext _context;

        public GoodsController(GoodsDbContext context)
        {
            _context = context;
        }

        // Read All
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Goods>>> GetAllGoods()
        {
            return await _context.Goods.ToListAsync();
        }

        // Read by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Goods>> GetGoods(string id)
        {
            var goods = await _context.Goods.FindAsync(id);
            if (goods == null) return NotFound();
            return goods;
        }

        // Create
        [HttpPost]
        public async Task<IActionResult> CreateGoods([FromBody] Goods goods)
        {
            if (goods == null) return BadRequest("Dữ liệu không hợp lệ.");

            try
            {
                _context.Goods.Add(goods);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetGoods), new { id = goods.MaHangHoa }, goods);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi Server: {ex.Message}");
            }
        }

        // Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGoods(string id, Goods goods)
        {
            if (id != goods.MaHangHoa) return BadRequest();
            _context.Entry(goods).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGoods(string id)
        {
            var goods = await _context.Goods.FindAsync(id);
            if (goods == null) return NotFound();
            _context.Goods.Remove(goods);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
