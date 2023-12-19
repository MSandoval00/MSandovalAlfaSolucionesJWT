using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private IConfiguration _config;
        public AlumnoController(IConfiguration config)
        {
            _config = config;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Login(string email, string pass)
        {
            ML.Alumno alumno = new ML.Alumno();
            alumno.Email = email;
            alumno.Password = pass;
            IActionResult response = Unauthorized();
            var alumnoInfo = AuthenticateUser(alumno);
            if (alumnoInfo != null)
            {
                var tokenStr = GenerateJSONWebToken(alumnoInfo);
                response = Ok(new { token = tokenStr });
            }
            return response;
        }
        private ML.Alumno AuthenticateUser(ML.Alumno alumno)
        {
            ML.Alumno usuarioAlumno = null;
            if (alumno.Email == "ashproghelp" && alumno.Password == "123") //checar en el mail si pertenece al mismo
            {
                usuarioAlumno = new ML.Alumno { Email = "ashproghelp@gmail.com", Password = "123" };
            }
            return usuarioAlumno;
        }
        private string GenerateJSONWebToken(ML.Alumno alumnoInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Email,alumnoInfo.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            var encodeToken = new JwtSecurityTokenHandler().WriteToken(token);
            return encodeToken;
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Alumno.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [Route("")]
        public IActionResult Add([FromBody]ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.Add(alumno);
            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut]
        [Route("{IdAlumno}")]
        public IActionResult Update(int IdAlumno,[FromBody]ML.Alumno alumno)
        {
            ML.Result result = BL.Alumno.Update(alumno);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpGet]
        [Route("{IdAlumno}")]
        public IActionResult GetById(int IdAlumno)
        {
            ML.Result result = BL.Alumno.GetById(IdAlumno);
            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("{IdAlumno}")]
        public IActionResult Delete(int IdAlumno)
        {
            ML.Result result = BL.Alumno.Delete(IdAlumno);
            if (result.Correct)
            {
                return StatusCode(200, result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
