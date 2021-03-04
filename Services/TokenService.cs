using Microsoft.IdentityModel.Tokens;
using Shop.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Services
{
    public class TokenService//Separar na pasta services,pq é algo q n interaje com tds diretamenta,é algo q faz alguma finalidade
    {
        public static string GenerateToken(User user)//metodo gera token
        {
            var tokenHandler = new JwtSecurityTokenHandler();//controlador token
            var key = Encoding.ASCII.GetBytes(Settings.Secret);// pegando chave secreta
            var tokenDescriptor = new SecurityTokenDescriptor//criando o que tera dentro do token,e configurando ele
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    //informações que registraremos denrto do token
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),//quanto tempo o token dura
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                //tipo de coficação de token
            
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);//token
            return tokenHandler.WriteToken(token);//cotrolador devolvendo o token
        }
    }
}
