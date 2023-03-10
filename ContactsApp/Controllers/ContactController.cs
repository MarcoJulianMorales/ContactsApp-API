using ContactsApp.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ContactsApp.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/[controller]")]
    public class ContactController : Controller
    {
        private ResponseAPI responseAPI = new ResponseAPI();
        private ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context= context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Table")]
        //[Authorize]
        public async Task<IActionResult> GetTable()
        {
            try
            {
                var contactsList = await _context.Contacts.ToListAsync();
                return Ok(contactsList);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("New_Contact")]
        public async Task<IActionResult> AddContact([FromBody] Contact contact)
        {
            try
            {
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, [FromBody] Contact contact)
        {
            try
            {
                if(id!= contact.Id)
                {
                    Console.WriteLine("Error");
                    return NotFound();
                }

                _context.Contacts.Update(contact);
                await _context.SaveChangesAsync();
                return Ok(new { message = "The contact has been updated successfully" });
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            try
            {
                var contact = await _context.Contacts.FindAsync(id);
                
                if(contact == null)
                {
                    return NotFound();
                }

                _context.Contacts.Remove(contact);
                await _context.SaveChangesAsync();
                return Ok(new { message="The contact has been deleted successfully"});

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
