using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{   
    [ApiController]
    [Route("[controller]")]
    public class OrderController : Controller
    {
        public OrderController(ShopContext context)
        {
            _context = context;
        }

        public ShopContext _context { get; }

        [HttpGet]
        //[EnableCors("AnotherPolicy")] //Route
        [Route("GetAll")]
        public IActionResult GetAllOrders()
        {
            return new JsonResult(_context.Orders);
        }

        [HttpGet("{Id}")]
        [EnableCors]
        public IActionResult GetOrder(int Id)
        {

            var order = _context.Orders;

            var PTeamp = order.FirstOrDefault(p => p.Id == Id);

            if (PTeamp == null)
            {
                return HttpNotFound();
            }

            return new JsonResult(PTeamp);

        }

        [HttpPost("post")]
        [EnableCors("AnotherPolicy")]
        public IActionResult PostProduct([FromBody] Order order)
        {
            
            using (var PostOrder = _context)
            {

                if (PostOrder != null)
                {
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                    return Ok("Added Order");
                }
                else
                {
                    return NotFound("Not found");
                }
            }
        }

        [HttpDelete("{id}")]
        [EnableCors("AnotherPolicy")]
        public IActionResult DeleteProduct(int Id)
        {
            var DeleteProducts = _context.Orders.FirstOrDefault(p => p.Id == Id);
            if (DeleteProducts != null)
            {
                _context.Orders.Remove(DeleteProducts);
                _context.SaveChanges();
                return Ok("Removed Order");
            }
            else
            {
                return NotFound("Not found");
            }
        }


        private IActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }
    }
}
