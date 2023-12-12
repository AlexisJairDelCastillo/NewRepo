using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Login(string email)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloExamenEksContext context = new DL.AdelCastilloExamenEksContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"LoginUsuario '{email}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Contrasenia = query.Contrasenia;
                        usuario.Telefono = query.Telefono;
                        usuario.Acceso = new ML.Acceso();
                        usuario.Acceso.IdAcceso = query.IdAcceso.Value;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar el registro." + ex.Message;
            }

            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloExamenEksContext context = new DL.AdelCastilloExamenEksContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll").ToList();

                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DL.Usuario resultUsuario in query)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = resultUsuario.IdUsuario;
                            usuario.Nombre = resultUsuario.Nombre;
                            usuario.ApellidoPaterno = resultUsuario.ApellidoPaterno;
                            usuario.ApellidoMaterno = resultUsuario.ApellidoMaterno;
                            usuario.ApellidoMaterno = resultUsuario.ApellidoMaterno;
                            usuario.Email = resultUsuario.Email;
                            usuario.Contrasenia = resultUsuario.Contrasenia;
                            usuario.Telefono = resultUsuario.Telefono;
                            usuario.Acceso = new ML.Acceso();
                            usuario.Acceso.IdAcceso = resultUsuario.IdAcceso.Value;
                            usuario.Acceso.Tipo = resultUsuario.Tipo;

                            result.Objects.Add(usuario);
                        }

                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar los registros." + ex.Message;
            }

            return result;
        }

        public static ML.Result GetById(int idUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using(DL.AdelCastilloExamenEksContext context = new DL.AdelCastilloExamenEksContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById {idUsuario}").AsEnumerable().FirstOrDefault();

                    if(query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();
                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Contrasenia = query.Contrasenia;
                        usuario.Telefono = query.Telefono;
                        usuario.Acceso = new ML.Acceso();
                        usuario.Acceso.IdAcceso = query.IdAcceso.Value;
                        usuario.Acceso.Tipo = query.Tipo;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al mostrar el registro." + ex.Message;
            }

            return result;
        }

        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloExamenEksContext context = new DL.AdelCastilloExamenEksContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioAdd '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.Contrasenia}', '{usuario.Telefono}', {usuario.Acceso.IdAcceso}");

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "El registro se inserto correctamente.";
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un problema al insertar el registro." + ex.Message;
            }

            return result;
        }

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloExamenEksContext context = new DL.AdelCastilloExamenEksContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioUpdate {usuario.IdUsuario}, '{usuario.Nombre}', '{usuario.ApellidoPaterno}', '{usuario.ApellidoMaterno}', '{usuario.Email}', '{usuario.Contrasenia}', '{usuario.Telefono}', {usuario.Acceso.IdAcceso}");

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "El registro se actualizo correctamente.";
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al actualizar el registro." + ex.Message;
            }

            return result;
        }

        public static ML.Result Delete(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AdelCastilloExamenEksContext context = new DL.AdelCastilloExamenEksContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"UsuarioDelete {usuario.IdUsuario}");

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "El registro se elimino correctamente.";
                    }
                }
            }catch(Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "Ocurrio un error al eliminar el registro." + ex.Message;
            }

            return result;
        }
    }
}
