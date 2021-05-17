using patterns.strategy;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using web.api.demarcacao.terreno.Domain.Interfaces.Repository;

namespace web.api.demarcacao.terreno.Service.Application.Strategy
{
    public class AuthUserStrategy : IStrategy<AuthUserQuery, AuthUserQueryResponse>
    {
        private IUsuarioRepository UsuarioRepository { get; }
        public AuthUserStrategy(IUsuarioRepository usuarioRepository)
        {
            UsuarioRepository = usuarioRepository;
        }
        public async Task<AuthUserQueryResponse> HandleAsync(AuthUserQuery request, CancellationToken cancellationToken)
        {
            var usuarioFind = UsuarioRepository
                .Find(o => o.Login == request.Login && o.Password == request.Password)
                .Include(o => o.UsuarioInterfaces);
            AuthUserQueryResponse response = default;
            if (usuarioFind.Any())
            {
                var usuario = usuarioFind.FirstOrDefault();
                List<string> interfaces = new List<string>();
                foreach(var item in usuario.UsuarioInterfaces)
                {
                    interfaces.Add($"{item.Interface.Tag};{item.Interface.Descricao}");
                }

                response = new AuthUserQueryResponse(usuario.Id, interfaces.ToArray());
                return response;
            }
            else
            {
                return response;
            }
        }
    }
}
