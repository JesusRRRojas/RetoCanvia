using CanviaTest.Data.Contratos;
using CanviaTest.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace CanviaTest.Data.Repositorios
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly string _conn;
        public ProductoRepositorio(IConfiguration configuration)
        {
            _conn = configuration.GetConnectionString("DBConexion");
        }

        public Producto Actualizar(Producto entidad)
        {
            try
            {
                int res;
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Actualizar_Producto";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@IdProducto", OleDbType.Integer).Value = entidad.IdProducto;
                        command.Parameters.Add("@Codigo", OleDbType.VarChar, 15).Value = entidad.Codigo;
                        command.Parameters.Add("@Nombre", OleDbType.VarChar, 15).Value = entidad.Nombre;
                        command.Parameters.Add("@Precio_Compra", OleDbType.Decimal).Value = entidad.Precio_Compra;
                        command.Parameters.Add("@Precio_Venta", OleDbType.Decimal).Value = entidad.Precio_Venta;

                        command.Connection = connection;


                        res = command.ExecuteNonQuery();
                        connection.Close();

                    }

                }
                if (res > 0)
                {
                    return entidad;
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Eliminar(int id)
        {
            try
            {
                int res;
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Eliminar_Producto";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@IdProducto", OleDbType.Integer).Value = id;


                        command.Connection = connection;


                        res = command.ExecuteNonQuery();
                        connection.Close();

                    }

                }
                if (res > 0)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Producto Insertar(Producto entidad)
        {
            try
            {
                int res;
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Insertar_Producto";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.Add("@Codigo", OleDbType.VarChar, 15).Value = entidad.Codigo;
                        command.Parameters.Add("@Nombre", OleDbType.VarChar, 15).Value = entidad.Nombre;
                        command.Parameters.Add("@Precio_Compra", OleDbType.Decimal).Value = entidad.Precio_Compra;
                        command.Parameters.Add("@Precio_Venta", OleDbType.Decimal).Value = entidad.Precio_Venta;

                        command.Connection = connection;


                        res = command.ExecuteNonQuery();
                        connection.Close();

                    }

                }
                if (res > 0)
                {
                    return entidad;
                }
                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Producto> Listar()
        {
            try
            {
                List<Producto> Lista = new List<Producto>();
                using (OleDbConnection connection = new OleDbConnection(_conn))
                {
                    using (OleDbCommand command = new OleDbCommand())
                    {
                        connection.Open();

                        command.CommandText = $"Listar_Producto";

                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Connection = connection;


                        OleDbDataReader reader = command.ExecuteReader();


                        while (reader.Read())
                        {
                            Lista.Add(new Producto()
                            {
                                IdProducto = reader.GetInt32(0),
                                Codigo = reader.GetString(1),
                                Nombre = reader.GetString(2),
                                Cantidad_Stock = reader.GetInt32(3),
                                Precio_Compra = reader.GetDecimal(4),
                                Precio_Venta = reader.GetDecimal(5),
                                Fecha_Registro = reader.GetDateTime(6)
                            });

                        }
                    }

                }
                return Lista;
            }
            catch (Exception ex)
            {
                return new List<Producto>();
            }
        }

        public (int totalRegistros, IEnumerable<Producto> registros) ObtenerPaginas(int paginaActual, int registrosPorPagina)
        {
            var registros = Listar();

            var totalRegistros = Listar().Count;

            var registrosPaginado = registros.Skip((paginaActual - 1) * registrosPorPagina).Take(registrosPorPagina).ToList();

            return (totalRegistros, registrosPaginado);
        }
    }
}
