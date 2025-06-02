using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Data;
using ProductAPI.Helpers;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    public class UserController(IResponseHelper _response, DataContext _db, TokenHelper _tokenHelper) : BaseController
    {
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<ApiResponseWithToken<User>>> RegisterAsync(User model)
        {
            if (model == null)
                return _response.CreateResponseWithToken<User>(false, 400, "Invalid request", null, "");

            model.Email = model.Email.ToLower();

            var existingUser = await _db.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (existingUser != null)
                return _response.CreateResponseWithToken<User>(false, 409, "Duplicate record found.", null, "");

            model.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            var newRecord = await _db.Users.AddAsync(model);

            await _db.SaveChangesAsync();

            var userEntity = newRecord.Entity;

            var token = _tokenHelper.GetJwtToken(userEntity.Id);

            return _response.CreateResponseWithToken(true, 201, "User created successfully.", userEntity, token);
        }
    }
}
