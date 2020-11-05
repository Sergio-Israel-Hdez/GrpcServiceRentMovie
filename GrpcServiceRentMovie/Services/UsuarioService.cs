using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using GrpcServiceRentMovie.Models;
namespace GrpcServiceRentMovie
{
    public class UsuarioService : UsuarioGRUD.UsuarioGRUDBase
    {
        private readonly ILogger<GreeterService> _logger;
        private Models.DB.RENTMOVIEContext _db = new Models.DB.RENTMOVIEContext();
        public UsuarioService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<UsuariosProto> SelectAll(Empty request, ServerCallContext context)
        {
            UsuariosProto responseData = new UsuariosProto();
            var query = ListarUsuarios(null);
            responseData.Items.AddRange(query);
            return Task.FromResult(responseData);
        }
        public override Task<UsuarioProto> SelectByID(UsuarioProtoFilter requestData,ServerCallContext context)
        {
            var data = ListarUsuarios(requestData.UsuarioID);
            return Task.FromResult(data.First());
        }
        public override Task<Empty> Insert(UsuarioProto requestData,ServerCallContext context)
        {
            var usuario = new Models.DB.Usuario()
            {
                Idusuario = requestData.Id,
                Nombre = requestData.Nombre,
                Apellido = requestData.Apellido,
                Email = requestData.Email,
                Password = requestData.Password
            };
            this._db.Usuario.Add(usuario);
            this._db.SaveChanges();
            return Task.FromResult(new Empty());
        }
        public override Task<Empty> Update(UsuarioProto requestData, ServerCallContext context)
        {
            var usuario = new Models.DB.Usuario()
            {
                Idusuario = requestData.Id,
                Nombre = requestData.Nombre,
                Apellido = requestData.Apellido,
                Email = requestData.Email,
                Estado = requestData.Estado,
                Password = requestData.Password
            };
            this._db.Usuario.Update(usuario);
            this._db.SaveChanges();
            return Task.FromResult(new Empty());
        }
        public override Task<Empty> Delete(UsuarioProtoFilter requestData, ServerCallContext context)
        {
            var result = _db.Usuario.Find(requestData.UsuarioID);
            _db.Usuario.Remove(result);
            _db.SaveChanges();
            return Task.FromResult(new Empty());
        }
        public IEnumerable<UsuarioProto> ListarUsuarios(int? filterId)
        {
            var result = from user in _db.Usuario
                         select new UsuarioProto()
                         {
                             Id = user.Idusuario,
                             Nombre = user.Nombre,
                             Apellido = user.Apellido,
                             Email = user.Email,
                             Rol = Convert.ToInt32(user.Rol),
                             Estado = Convert.ToInt32(user.Estado),
                             Password = user.Password
                         };
            if (filterId!=null)
            {
                result = result.Where(x => x.Id == filterId);
            }
            return result.AsEnumerable();
        }
    }
}
