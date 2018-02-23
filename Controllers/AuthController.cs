using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using login_webapi.DbContext;
using login_webapi.Entities;
using login_webapi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NETCore.Encrypt;

namespace login_webapi.Controllers {
    public class AuthController : Controller {
        private IConfiguration _config;
        private LoginDbContext _context;
        private IMapper _mapper;

        public AuthController (IConfiguration config, LoginDbContext context, IMapper mapper) {
            this._config = config;
            this._context = context;
            this._mapper = mapper;
        }

        [HttpPost ("/api/signup")]
        public async Task<IActionResult> Register ([FromBody] UserModel _user) {
            if (!ModelState.IsValid) {
                var errors = ModelState.Select (x => x.Value.Errors).Where (e => e.Count > 0).ToList ();
                return BadRequest (errors);
            }
            // Encrypt the password
            var key = this._config["PasswordKey"];
            var passwordEncrypted = EncryptProvider.AESEncrypt (_user.Password, key);
            // Save password on property of.
            _user.Password = passwordEncrypted;
            // Map the model to entity
            User userMapped = this._mapper.Map<User> (_user);
            // Save on database
            this._context.Users.Add (userMapped);
            await this._context.SaveChangesAsync ();

            return Ok ();
        }

        [HttpPost ("/api/signin")]
        public IActionResult Login ([FromBody] LoginModel _user) {
            if (!ModelState.IsValid) {
                var errors = ModelState.Select (x => x.Value.Errors).Where (e => e.Count > 0).ToList ();
                return BadRequest (errors);
            }
            // Map the model to entity
            User userMapped = this._mapper.Map<User> (_user);
            // Validate user
            User userFound = this._context.Users.SingleOrDefault (u => u.Email == userMapped.Email);
            var key = this._config["PasswordKey"];
            var passwordEncrypted = EncryptProvider.AESEncrypt (_user.Password, key);
            if (userFound == null || userFound.Password != passwordEncrypted) {
                return BadRequest ("Invalid user/password.");
            }
            // Returning token
            return Ok (this.GenerateToken (userFound));
        }

        private dynamic GenerateToken (User user) {
            var claims = new List<Claim> {
                new Claim (JwtRegisteredClaimNames.Iss, this._config["Token:Issuer"]),
                new Claim (JwtRegisteredClaimNames.Aud, this._config["Token:Audience"]),
                new Claim (JwtRegisteredClaimNames.Email, user.Email),
                new Claim (JwtRegisteredClaimNames.Jti, user.Id.ToString ()),
                new Claim (JwtRegisteredClaimNames.Sub, this._config["Token:Subject"])
            };

            var key = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (this._config["Token:Key"]));
            var creds = new SigningCredentials (key, SecurityAlgorithms.HmacSha256);

            var tokenObg = new JwtSecurityToken (
                issuer : this._config["Token:Issuer"],
                audience : this._config["Token:Audience"],
                claims : claims,
                expires : DateTime.Now.AddMinutes (2),
                signingCredentials : creds
            );

            return new {
                token = new JwtSecurityTokenHandler ().WriteToken (tokenObg),
                    expiration = DateTime.Now.AddMinutes (2)
            };
        }
    }
}