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

        //CRUD Methods

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
                contact.FechaRegistro = DateTime.Now;
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
                return Ok();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("update")]
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

        [HttpDelete("delete")]
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

        //Import from CSV File
        [HttpPost("ImportCSV")]
        public async Task<IActionResult> importCSV([FromBody]Contact contact)
        {
            return null;
            //List<Contact> lista= new List<Contact>();

            //System.IO.StreamReader archivo = new System.IO.StreamReader(fileLocation);

            //string separador = ",";
            //string linea;

            //archivo.ReadLine();

            //while ((linea = archivo.ReadLine()) != null)
            //{
            //    string[] fila = linea.Split(separador);
                
            //    Contact contact = new Contact();
            //    contact.Nombre = Convert.ToString(fila[0]);
            //    contact.Direccion = Convert.ToString(fila[1]);
            //    contact.Telefono = Convert.ToString(fila[2]);
            //    contact.CURP = Convert.ToString(fila[3]);
            //    contact.FechaRegistro = DateTime.Now;
            //    lista.Add(contact);
            //}

            //try
            //{
            //    foreach (Contact contact in importData) {
            //        _context.Contacts.Add(contact);
            //    }
            //    await _context.SaveChangesAsync();
            //    return Ok(importData);
            //}
            //catch(Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }


        //Search methods

        [HttpGet("SearchById")]
        public async Task<IActionResult> SearchById(int id)
        {
            List<Contact> lista = new List<Contact>();
            try
            {
                await using (_context)
                {
                    lista = (from contact in _context.Contacts
                             where contact.Id == id
                             select new Contact
                             {
                                 Id = contact.Id,
                                 CURP = contact.CURP,
                                 Direccion = contact.Direccion,
                                 FechaRegistro = contact.FechaRegistro,
                                 Nombre = contact.Nombre,
                                 Telefono = contact.Telefono
                             }).ToList();
                }
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SearchByNombre")]
        public async Task<IActionResult> SearchByNombre(string nombre)
        {
            List<Contact> lista = new List<Contact>();
            try
            {
                await using (_context)
                {
                    lista = (from contact in _context.Contacts
                             where contact.Nombre == nombre
                             select new Contact
                             {
                                 Id = contact.Id,
                                 CURP = contact.CURP,
                                 Direccion = contact.Direccion,
                                 FechaRegistro = contact.FechaRegistro,
                                 Nombre = contact.Nombre,
                                 Telefono = contact.Telefono
                             }).ToList();
                }
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SearchByDireccion")]
        public async Task<IActionResult> SearchByDireccion(string direccion)
        {
            List<Contact> lista = new List<Contact>();
            try
            {
                await using (_context)
                {
                    lista = (from contact in _context.Contacts
                             where contact.Direccion == direccion
                             select new Contact
                             {
                                 Id = contact.Id,
                                 CURP = contact.CURP,
                                 Direccion = contact.Direccion,
                                 FechaRegistro = contact.FechaRegistro,
                                 Nombre = contact.Nombre,
                                 Telefono = contact.Telefono
                             }).ToList();
                }
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SearchByTelefono")]
        public async Task<IActionResult> SearchByTelefono(string telefono)
        {
            List<Contact> lista = new List<Contact>();
            try
            {
                await using (_context)
                {
                    lista = (from contact in _context.Contacts
                             where contact.Telefono == telefono
                             select new Contact
                             {
                                 Id = contact.Id,
                                 CURP = contact.CURP,
                                 Direccion = contact.Direccion,
                                 FechaRegistro = contact.FechaRegistro,
                                 Nombre = contact.Nombre,
                                 Telefono = contact.Telefono
                             }).ToList();
                }
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SearchByCurp")]
        public async Task<IActionResult> SearchByCurp(string curp)
        {
            List<Contact> lista = new List<Contact>();
            try
            {
                await using (_context)
                {
                    lista = (from contact in _context.Contacts
                             where contact.CURP == curp
                             select new Contact
                             {
                                 Id = contact.Id,
                                 CURP = contact.CURP,
                                 Direccion = contact.Direccion,
                                 FechaRegistro = contact.FechaRegistro,
                                 Nombre = contact.Nombre,
                                 Telefono = contact.Telefono
                             }).ToList();
                }
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("SearchByFechaRegistro")]
        public async Task<IActionResult> SearchByFecharegistro(DateTime fecha)
        {
            List<Contact> lista = new List<Contact>();
            try
            {
                await using (_context)
                {
                    lista = (from contact in _context.Contacts
                             where contact.FechaRegistro == fecha
                             select new Contact
                             {
                                 Id = contact.Id,
                                 CURP = contact.CURP,
                                 Direccion = contact.Direccion,
                                 FechaRegistro = contact.FechaRegistro,
                                 Nombre = contact.Nombre,
                                 Telefono = contact.Telefono
                             }).ToList();
                }
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
