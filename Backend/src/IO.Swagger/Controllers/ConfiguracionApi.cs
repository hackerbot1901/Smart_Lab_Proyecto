using IO.Swagger.LaboratorioDB;
using IO.Swagger.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace IO.Swagger.Controllers
{
    [ApiController]
    [Route("/api/smart-lab-ra-api/v1/")]
    public class ConfiguracionApi : ControllerBase
    {
        private readonly IConfiguration _config;

        public ConfiguracionApi(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Obtiene la información del paciente en base al código de barras proporcionado
        /// </summary>
        /// <param name="codigoQr">Código QR que contiene la información necesaria</param>
        /// <returns>Objeto creado para la configuración de la Aplicación</returns>
        [HttpPost]
        [Route("configuracion")]
        [SwaggerResponse(statusCode: 200, type: typeof(ExitoCreacionConfiguracion), description: "Objeto creado para la configuración de la Aplicación")]
        [SwaggerResponse(statusCode: 400, type: typeof(Error400), description: "Solicitud Incorrecta")]
        [SwaggerResponse(statusCode: 500, type: typeof(Error500), description: "Error interno del servidor")]
        public IActionResult Configuracion([FromBody, Required] CodigoQr codigoQr)
        {
            try
            {
                if (!EsModeloQrValido(codigoQr))
                {
                    return BadRequest(new Error400 { Mensaje = "Código QR no válido." });
                }

                using var _dbContext = new LaboratorioDbContext();

                if (!ExisteQrBD(_dbContext, codigoQr))
                {
                    return BadRequest(new Error400 { Mensaje = "Los parámetros del código QR no coinciden con los esperados." });

                }

                string api_token = GenerarToken(codigoQr);
                var exitoResponse = new ExitoCreacionConfiguracion
                {
                    Mensaje = "Objeto creado para la configuración de subdominio y token de la aplicación",
                    Subdominio = codigoQr.Subdominio,
                    Api_token = api_token
                };

                return Ok(exitoResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Error500 { Mensaje = "Ocurrió un error al procesar la solicitud." + ex.Message });
            }
        }


        private static bool ExisteQrBD(LaboratorioDbContext dbContext, CodigoQr codigoQr)
        {
            var modeloValidoQr =  dbContext.Laboratorista
                        .Where(l => (l.Nombre + " " + l.Apellido) == codigoQr.nombre_usuario &&
                                    l.Sucursal.subdominio == codigoQr.Subdominio &&
                                    l.Sucursal.NombreSucursal == codigoQr.Nombre_tenant)
                        .Select(l => new CodigoQr
                        {
                            nombre_usuario = codigoQr.nombre_usuario,
                            Subdominio = codigoQr.Subdominio,
                            Nombre_tenant = codigoQr.Nombre_tenant
                        })
                        .FirstOrDefault();

            return modeloValidoQr != null &&
                       modeloValidoQr.Subdominio == codigoQr.Subdominio &&
                       modeloValidoQr.Nombre_tenant == codigoQr.Nombre_tenant;
        }

        private static bool EsModeloQrValido(CodigoQr codigoQr)
        {
            return codigoQr != null &&
                   !string.IsNullOrWhiteSpace(codigoQr.Subdominio) &&
                   !string.IsNullOrEmpty(codigoQr.nombre_usuario) &&
                   !string.IsNullOrEmpty(codigoQr.Nombre_tenant);
        }

        private string GenerarToken(CodigoQr codigoQr)
        {
            var claims = new[]
            {
                new Claim("nombre_laboratorista", codigoQr.nombre_usuario),
                new Claim("sucursal", codigoQr.Nombre_tenant),
                new Claim("subdominio", codigoQr.Subdominio)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenOptions = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpirationMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        }
    }
}
