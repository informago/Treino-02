using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Treino_REST_02.Models
{

    /// <summary>
    /// Dados do Usuário
    /// </summary>
    public class Usuario : IdentityUser { }

    /// <summary>
    /// DbContext para manipulação de usuários
    /// </summary>
    public class UsuarioDbContext : IdentityDbContext<Usuario>
    {
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="options"></param>
        public UsuarioDbContext(DbContextOptions<UsuarioDbContext> options) : base(options) { }
    }

}
