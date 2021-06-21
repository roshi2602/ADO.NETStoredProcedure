using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
// Stored Procedures are a block of code that is designed to perform a specific task whenever it is called. 
// create a stored procedure that will not take any input parameter but will return all the records from the  table. 
//to call stored procedure-


//for this -first go to STUDENTDB database and click-> tables->stored procedure->and copy the code in dbo.procedure.sql
/*CREATE PROCEDURE spGetStudentById
(
   @id INT
)
AS
BEGIN
     SELECT id, name, age, gender
  FROM STUDENT
  WHERE id = @id
END */

namespace ADO.NETStoredProcedure
{
    class Program
    {
        static void Main()
        {
            try
            {
                string ConString = "data source=.; database=STUDENTDB; integrated security=SSPI";
                using (SqlConnection con = new SqlConnection(ConString))
                {

                    //create command object
                    SqlCommand cmd = new SqlCommand()
                    {
                        CommandText = "spGetStudentById",      //name from stored procedure file
                        Connection = con,
                        CommandType = CommandType.StoredProcedure
                    };

                    //now set sql parameter
                    SqlParameter spa = new SqlParameter()
                    {
                        //define parameter which was in stored procedure file
                        ParameterName = "@id",
                        //data type of parameter
                        SqlDbType = SqlDbType.Int,
                        //passing value to parameter
                        Value = 1,
                        //now specify parameter as input
                        Direction = ParameterDirection.Input

                    };
                    //add paramter to sql command object
                    cmd.Parameters.Add(spa);

                    //now opening connection
                    con.Open();

                    //apply data reader instance
                    SqlDataReader sdr = cmd.ExecuteReader();

                    //apply while loop for reading
                    while (sdr.Read())
                    {
                        Console.WriteLine(sdr["id"] + "," + sdr["name"] +","+ sdr["age"] +","+ sdr["gender"]);
                    }
                }
            }
            catch (Exception er)
            {
                Console.WriteLine("not working" + er);

            }
            Console.ReadLine();
        }
    }
}