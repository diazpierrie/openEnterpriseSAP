﻿using EE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DAL
{
    public class PatenteDal : ConnectionDal
    {
        public List<RolEe> ObtenerRolesPorFamiliaId(int id)
        {
            try
            {
                var query = new SqlCommand("SELECT p.id, p.nombre FROM patente p JOIN familia_patente fp ON fp.idPatente = p.id WHERE fp.idFamilia = @id", Conn);
                query.Parameters.AddWithValue("@id", id);
                Conn.Open();
                var data = query.ExecuteReader();
                var result = new List<RolEe>
                {
                    Capacity = 0
                };

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        result.Add(CastDto(data));
                    }
                }

                Conn.Close();
                return result;
            }
            catch (Exception e)
            {
                ErrorManagerDal.AgregarMensaje(e.ToString());
                return new List<RolEe>();
            }
        }

        public List<PatenteEe> Obtener(int id = 0)
        {
            try
            {
                var sqlQuery = "SELECT p.id, p.Nombre FROM patente p";

                if (id != 0)
                {
                    sqlQuery += $" WHERE id = {id}";
                }

                var query = new SqlCommand(sqlQuery, Conn);
                Conn.Open();
                var data = query.ExecuteReader();
                var result = new List<PatenteEe>();

                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        result.Add(CastDto(data));
                    }
                }

                Conn.Close();
                return result;
            }
            catch (Exception e)
            {
                ErrorManagerDal.AgregarMensaje(e.ToString());
                return new List<PatenteEe>();
            }
        }

        public int CrearRelacion(int familiaid, int patenteid)
        {
            string[] columns = { "idFamilia", "idPatente" };
            string[] values = { familiaid.ToString(), patenteid.ToString() };
            return Insert("familia_patente", columns, values);
        }

        private static PatenteEe CastDto(SqlDataReader data)
        {
            var result = new PatenteEe
            {
                Id = Convert.ToInt32(data["id"]),
                Nombre = data["Nombre"].ToString()
            };

            return result;
        }
    }
}