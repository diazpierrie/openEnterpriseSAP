﻿using System;
using System.Data.SqlClient;
using System.IO;

namespace DAL
{
    public class BackupDal : ConnectionDal
    {
        public bool BackUp()
        {
            try
            {
                var query = new SqlCommand("BACKUP DATABASE aWords TO  DISK = @bkpPath", Conn);
                var bkpPath = Directory.GetCurrentDirectory() + "\\..\\..\\..\\BackUps\\bkp" + GetTimestamp(DateTime.Now) + ".bak";
                File.Delete(bkpPath);
                query.Parameters.AddWithValue("@bkpPath", bkpPath);

                Conn.Open();
                query.ExecuteNonQuery();
                Conn.Close();

                return true;
            }
            catch (Exception e)
            {
                ErrorManagerDal.AgregarMensaje(e.ToString());
                return false;
            }
        }

        public bool Restore(string nombreArchivo)
        {
            try
            {
                var singleUser = new SqlCommand("ALTER DATABASE aWords SET Single_User WITH Rollback Immediate", Conn);
                var query = new SqlCommand("USE master; RESTORE DATABASE aWords FROM DISK = @bkpPath WITH REPLACE;", Conn);
                var multiUser = new SqlCommand("ALTER DATABASE aWords SET Multi_User", Conn);

                query.Parameters.AddWithValue("@bkpPath", nombreArchivo);

                Conn.Open();
                singleUser.ExecuteNonQuery();
                query.ExecuteNonQuery();
                multiUser.ExecuteNonQuery();
                Conn.Close();

                return true;
            }
            catch (Exception e)
            {
                ErrorManagerDal.AgregarMensaje(e.ToString());
                return false;
            }
        }
	}
}