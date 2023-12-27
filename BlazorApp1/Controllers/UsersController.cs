using BlazorApp1.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UsersController(ApplicationDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public async Task<List<Users>> GetUsers()
        {
            var Users = await _db.Users.Where(i => i.IsDeleted == false || i.IsDeleted == null).ToListAsync();

            return await Task.FromResult(Users);
        }
        public IActionResult GetUserById(int Id)
        {
            var Users = _db.Users.Where(i => i.Id == Id && i.IsDeleted == false || i.IsDeleted == null).FirstOrDefaultAsync();
            if (Users != null)
            {
                return Ok(Users);
            }
            else
            {
                return NotFound();

            }
        }
        public async void DeleteUserById(int Id)
        {
            var users =await _db.Users.Where(i => i.Id == Id && i.IsDeleted == false || i.IsDeleted == null).FirstOrDefaultAsync();
            if (users != null)
            {
                users.IsDeleted = true;
                await _db.SaveChangesAsync();
                Ok("User Delete SuccessFully..");
            }
            else
            {
                NotFound("User Not Found");
            }
        }
        [HttpPost]
        public async void CreateUpdateUser(Users users, int Id)
        {
            if (Id == null)
            {
                Users Users = new Users();
                Users = users;
                await _db.Users.AddAsync(Users);
                await _db.SaveChangesAsync();
                Ok(Users);
            }
            else
            {
                var User = await _db.Users.Where(i => i.Id == Id && i.IsDeleted == false || i.IsDeleted == null).FirstOrDefaultAsync();
                if (User != null)
                {
                    User.Name = users.Name;
                    User.Email = users.Email;
                    await _db.SaveChangesAsync();
                    Ok(User);
                }
                else
                {
                    BadRequest($"User With Id ${Id} Not Found");
                }
            }
        }
    }
}
