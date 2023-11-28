using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiUniversidade.DTO;

public class UsuarioToken
{
    public bool Authenticated { get; set; }
    public DataTime Expiration { get; set; }
    public string Token { get; set; }
    public string Messege { get; set; }
}