using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserAPI.Data;
using UserAPI.Models;
using DeviceAPI.Models;
using DeviceFrontend.Components.Pages;

namespace UserAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet(Name = "AllUsers")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers(string name = "", string role = "", int page = 1, int pageSize = 50)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(u => u.Name == name);
            }
            if (!string.IsNullOrEmpty(role))
            {
                query = query.Where(u => u.Role.ToLower() == role.ToLower());
            }

            var result = await query.Skip((page-1) * pageSize).Take(pageSize).ToListAsync(); // query ul nu se trimite catre baza de date pana nu se apeleaza .ToListAsync() deci nu se primeste niciun rezultat

            return Ok(result);
        }

        // GET: api/Users/5
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // GET: api/Users/Devices
        [HttpGet("/Devices/({id})")]
        public async Task<IEnumerable<Device>> GetUserDevices(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }
            
            var usersDevices = await _context.Devices.Where(d => d.UserId == user.Id).ToListAsync();
            return usersDevices;
        }

        // GET: users/devices/assigned
        [HttpGet("/devices/assigned")]
        public async Task<IEnumerable<Device>> GetAssignedDevices()
        {
            var ids = await _context.Users.Select(u => u.Id).ToListAsync();
            
            return await _context.Devices.Where(d => ids.Contains(d.UserId.Value) && d.UserId!=null).ToListAsync();
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}", Name = "UpdateUser")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (! user.Id.Equals(id))
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // PUT: api/Users/AddDevice/userId
        // method to assign a device to a specific user
        [HttpPut("/AddDevice/({userId})")]
        public async Task<IActionResult> AddDeviceToUser(Guid userId, Device device)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return NotFound();
            }

            device.UserId = user.Id;
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost(Name = "CreateUser")]
        public async Task<ActionResult<UserDTO>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}", Name = "DeleteUser")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            foreach(var device in user.Devices)
            {
                _context.Devices.Remove(device);
            }

            _context.Users.Remove(user);
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/users/devices/deleteall
        [HttpDelete("/devices/deleteall")]
        public async Task<IActionResult> DeleteDevicesAll()
        {
            var devices = await _context.Devices.ToListAsync();
            foreach (var device in devices)
            {
                _context.Devices.Remove(device);

            }
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // GET: api/users/devices/getall
        [HttpGet("/devices/getall")]
        public async Task<IEnumerable<Device>> GetDevicesAll()
        {
            return await _context.Devices.ToListAsync();
        }

        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id.Equals(id));
        }

       
    }
}
