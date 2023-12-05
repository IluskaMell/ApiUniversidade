using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiUniversidade.Model;
using apiUniversidade.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;
using ApiUniversidade.DTO;


namespace apiUniversidade.Controllers{
    [ApiController]
    [Route("[controller]")]
    public class AutorizaController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public AutorizaController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpGet]
            public ActionResult<string> Get(){
                return "AutorizaController :: Acessado em : "
                    + DateTime.Now.ToLongDateString(); 
            }
        
        [HttpPost("register")]
            public async Task<ActionResult> RegisterUser([FromBody]UsuarioDTO model){
                var user = new IdentityUser{
                    UserName = model.Email,
                    Email = model.Email,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if(!result.Succeeded)
                    return BadRequest(result.Errors);
                
                await _signInManager.SignInAsync(user, false);
                return Ok();
            }
        
        [HttpPost("login")]
            public async Task<ActionResult> login([FromBody] UsuarioDTO userInfo){

                var result = await _signInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password,
                    isPersistent: false, lockoutOnFailure: false);
                
                if(result.Succeeded)
                    return Ok();
                else{
                    ModelState.AddModelError(string.Empty,"Login Inválido...");
                    return BadRequest(ModelState);
                }
            } 
            
    }
}

    