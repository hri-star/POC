using Microsoft.Data.SqlClient;
using System.Data;

namespace POC.WebAPI.Models
{
    public class DatabaseContext
    {
        private static string _connectionString = "";
        public DatabaseContext(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("LAF_DB");// _context.GetAppSettings().GetConnectionString("");
        }
        public virtual DataTable UserInfos { get; set; }

     
        public DataTable GetData(string str)
        {
            DataTable objresutl = new DataTable();
            try
            {
                SqlDataReader myReader;

                using (SqlConnection myCon = new SqlConnection())
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(str, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        objresutl.Load(myReader);

                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return objresutl;

        }
        public int ExecuteData(string str, params IDataParameter[] sqlParams)
        {
            int rows = -1;
            try
            {

                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(str, conn))
                    {
                        if (sqlParams != null)
                        {
                            foreach (IDataParameter para in sqlParams)
                            {
                                cmd.Parameters.Add(para);
                            }
                            rows = cmd.ExecuteNonQuery();
                        }





                    }
                }
            }
            catch (Exception ex)
            {

            }


            return rows;


        }
    }
}
