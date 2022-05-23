using BaseStartup.Data;
using BaseStartup.Dtos;
using BaseStartup.Entities;
using BaseStartup.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BaseStartup.Controllers;

public class AccountController : BaseApiController
{
    private readonly UserManager<AppUser> _userManager;
    private readonly TokenService _tokenService;
    //private readonly DataContext _context;
    public AccountController(UserManager<AppUser> userManager, TokenService tokenService, DataContext context)
    {
        //_context = context;
        _tokenService = tokenService;
        _userManager = userManager;
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var user = await _userManager.FindByNameAsync(loginDto.Username);

        if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            return Unauthorized();

        return new UserDto
        {
            Email = user.Email,
            Token = await _tokenService.GenerateToken(user)
            // Basket = anonBasket != null ? anonBasket.MapBasketToDto() : userBasket?.MapBasketToDto()
        };
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto registerDto)
    {
        var user = new AppUser 
        { 
            UserName = registerDto.Username, 
            Email = registerDto.Email, 
            Avatar = registerDto.Avatar,
            NickName = registerDto.NickName,
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem();
        }

        await _userManager.AddToRoleAsync(user, "Member");

        return StatusCode(201);
    }

    [Authorize]
    [HttpGet("currentUser")]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await _userManager.FindByNameAsync(User.Identity?.Name);

        // var userBasket = await RetrieveBasket(User.Identity.Name);

        return new UserDto
        {
            Email = user.Email,
            Token = await _tokenService.GenerateToken(user),
        };
    }

    
}
