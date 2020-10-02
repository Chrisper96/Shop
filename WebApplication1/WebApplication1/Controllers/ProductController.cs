﻿using Microsoft.AspNetCore.Cors;
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
    public class ProductController : Controller
    {
        public ProductController(ShopContext context)
        {
            _context = context;
        }

        public ShopContext _context { get; }

        [HttpGet]
        //[EnableCors("AnotherPolicy")] //Route
        [Route("GetAll")]
        public IActionResult GetAllProducts()
        {
            return new JsonResult(_context.Products);
        }

        [HttpGet("{Id}")]
        [EnableCors]
        public IActionResult GetProdut(int Id)
        {

            var product = _context.Products;

            var PTeamp = product.FirstOrDefault(p => p.Id == Id);

            if (PTeamp == null)
            {
                return HttpNotFound();
            }

            return new JsonResult(PTeamp);

        }

        [HttpPost("post")]
        //[EnableCors("AnotherPolicy")]
        public IActionResult PostProduct([FromBody] Product product)
        {
            
            using (var PostProduct = _context)
            {

                if (PostProduct != null)
                {
                    _context.Products.Add(product);
                    _context.SaveChanges();
                    return Ok("Added Product");
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
            var DeleteProducts = _context.Products.FirstOrDefault(p => p.Id == Id);
            if (DeleteProducts != null)
            {
                _context.Products.Remove(DeleteProducts);
                _context.SaveChanges();
                return Ok("Removed Product");
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
