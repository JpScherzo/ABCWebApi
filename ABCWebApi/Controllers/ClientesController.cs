using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ABCWebApi.Models;
using System.IO;
using Newtonsoft.Json;

namespace ABCWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly ABCContext _context;

        public ClientesController(ABCContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public IEnumerable<Cliente> GetCliente()
        {
            return _context.Cliente;
        }

        // GET: api/Clientes/5
        [HttpGet("{CPF}")]
        public async Task<IActionResult> GetCliente([FromRoute] Int64 CPF)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = await _context.Cliente.FindAsync(CPF);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/Clientes/5
        [HttpPut("{CPF}")]
        public async Task<IActionResult> PutCliente([FromRoute] Int64 CPF, [FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (CPF != cliente.CPF)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(CPF))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Clientes
        [HttpPost]
        public async Task<IActionResult> PostCliente([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.CPF }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{CPF}")]
        public async Task<IActionResult> DeleteCliente([FromRoute] Int64 CPF)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cliente = await _context.Cliente.FindAsync(CPF);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return Ok(cliente);
        }

        [HttpPost("/api/[controller]/autenticar")]
        public async Task<IActionResult> AutenticaCliente()
        {
            var Auth = new Autentica();
            string strAutentica = "";

            // salva a mensagem bruta (raw)
            using (StreamReader reader = new StreamReader(Request.Body, System.Text.Encoding.UTF8))
            {
                strAutentica = reader.ReadToEnd();
            }
            Auth = JsonConvert.DeserializeObject<Autentica>(strAutentica);

            var cliente =  await _context.Cliente.SingleOrDefaultAsync(e => (e.Email == Auth.Email && e.Senha == Auth.Senha));
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }


        [HttpPost("/api/[controller]/verificar")]
        public async Task<IActionResult> VerificarEmail()
        {
            var Auth = new Autentica();
            string strAutentica = "";

            // salva a mensagem bruta (raw)
            using (StreamReader reader = new StreamReader(Request.Body, System.Text.Encoding.UTF8))
            {
                strAutentica = reader.ReadToEnd();
            }
            Auth = JsonConvert.DeserializeObject<Autentica>(strAutentica);

            var cliente = await _context.Cliente.SingleOrDefaultAsync(e => (e.Email == Auth.Email));
            if (cliente == null)
            {
                return NotFound();
            }

            return NoContent();
        }


        private bool ClienteExists(Int64 CPF)
        {
            return _context.Cliente.Any(e => e.CPF == CPF);
        }


    }
}