using APIMaisEventos.Contexts;
using APIMaisEventos.Interfaces;
using APIMaisEventos.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace APIMaisEventos.Repositories
{
    public class LoginRepository : ILoginRepository
    {

        private readonly MaisEventosContext ctx;
        public LoginRepository(MaisEventosContext _ctx)
        {
            ctx = _ctx;
        }

        public string Logar(string email, string senha)
        {
            //return ctx.Usuarios.Where(x => x.Email == email && x.Senha == senha).FirstOrDefault();

            var usuario = ctx.Usuarios.FirstOrDefault(x => x.Email == email);

            if(usuario is not null)
            {
                bool confere = BCrypt.Net.BCrypt.Verify(senha, usuario.Senha);

                if (confere)
                {
                    //Criar as credenciais do JWT

                    // Definimos as Claims
                    var minhasClaims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, usuario.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, usuario.Id.ToString()),
                        new Claim(ClaimTypes.Role, "Adm"),

                        new Claim("Cargo", "Adm")
                    };

                    // Criamos as chaves
                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("MaisEventos-chave-autenticacao"));

                    // Criamos as credenciais
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    // Geramos o token
                    var meuToken = new JwtSecurityToken(
                        issuer: "APIMaisEventos.webAPI",
                        audience: "APIMaisEventos.webAPI",
                        claims: minhasClaims,
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: creds
                    );

                    return new JwtSecurityTokenHandler().WriteToken(meuToken);
                }
            }

            return null;
        }
    }
}
