using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers.SQLServer
{
    /// <summary>
    /// 
    /// </summary>
    public class SupplierDAL : _BaseDAL, ISupplierDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public SupplierDAL(string connectionString) : base(connectionString)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public int Add(Supplier data)
        {
            int supplierID = 0;

            using(SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"INSERT INTO Suppliers 
                                    (
                                        SupplierName, ContactName, Address, City, PostalCode, Country, Phone
                                    )
                                    VALUES
                                    (
                                        @supplierName, @contactName, @address, @city, @postalCode, @country, @phone
                                    );
                                    SELECT @@IDENTITY";
                cmd.Parameters.AddWithValue("@supplierName", data.SupplierName);
                cmd.Parameters.AddWithValue("@contactName", data.ContactName);
                cmd.Parameters.AddWithValue("@address", data.Address);
                cmd.Parameters.AddWithValue("@city", data.City);
                cmd.Parameters.AddWithValue("@postalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@country", data.Country);
                cmd.Parameters.AddWithValue("@phone", data.Phone);

                supplierID = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }

            return supplierID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public int Count(string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";

            int result = 0;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT COUNT(*) FROM  Suppliers
                                    WHERE (@searchValue = '')
                                        OR (
			                                    SupplierName LIKE @searchValue
                                            OR SupplierID LIKE @searchValue
                                            OR City LIKE @searchValue
			                                OR ContactName LIKE @searchValue
			                                OR Address LIKE @searchValue
			                                OR Phone LIKE @searchValue
			                                )";
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                // nếu kết quả trả về 1 kết quả (1 dòng, 1 cột) thì dùng ExecuteScalar
                result = Convert.ToInt32(cmd.ExecuteScalar());

                cn.Close();
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public bool Delete(int supplierID)
        {
            bool result = false;

            using(SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"DELETE FROM Suppliers
                                    WHERE SupplierID = @supplierID AND NOT EXISTS(SELECT * FROM Products WHERE SupplierID = Suppliers.Supplierid)";

                cmd.Parameters.AddWithValue("@supplierID", supplierID);

                // Sử dụng ExecuteNonQuery khi câu lệnh không trả về kết quả
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        public Supplier Get(int supplierID)
        {
            Supplier data = null;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Suppliers WHERE SupplierID = @supplierID";

                cmd.Parameters.AddWithValue("@supplierID", supplierID);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    if (dbReader.Read())
                    {
                        data = new Supplier()
                        {
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            SupplierName = Convert.ToString(dbReader["SupplierName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Phone = Convert.ToString(dbReader["Phone"])
                        };

                    }
                }

                cn.Close();
            }

            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public List<Supplier> List(int page, int pageSize, string searchValue)
        {
            if (searchValue != "")
                searchValue = "%" + searchValue + "%";
            List<Supplier> data = new List<Supplier>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM (
	                                    SELECT *, ROW_NUMBER() OVER(ORDER BY SupplierID) AS RowNumber
	                                    FROM Suppliers
	                                    WHERE (@searchValue = '')
		                                    OR (
			                                       SupplierName LIKE @searchValue
                                                OR SupplierID LIKE @searchValue
                                                OR City LIKE @searchValue
			                                    OR ContactName LIKE @searchValue
			                                    OR Address LIKE @searchValue
			                                    OR Phone LIKE @searchValue
			                                    )
	                                    ) AS s
                                    WHERE s.RowNumber BETWEEN (@page -1)*@pageSize + 1 AND @page*@pageSize";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;

                cmd.Parameters.AddWithValue("@page", page);
                cmd.Parameters.AddWithValue("@pageSize", pageSize);
                cmd.Parameters.AddWithValue("@searchValue", searchValue);

                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Supplier()
                        {
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            SupplierName = Convert.ToString(dbReader["SupplierName"]),
                            ContactName = Convert.ToString(dbReader["ContactName"]),
                            Address = Convert.ToString(dbReader["Address"]),
                            City = Convert.ToString(dbReader["City"]),
                            PostalCode = Convert.ToString(dbReader["PostalCode"]),
                            Country = Convert.ToString(dbReader["Country"]),
                            Phone = Convert.ToString(dbReader["Phone"])

                        });
                    }
                }

                cn.Close();
            }
            return data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Update(Supplier data)
        {
            bool result = false;

            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = cn.CreateCommand();
                cmd.CommandText = @"UPDATE Suppliers
                                    SET SupplierName = @supplierName,
	                                    ContactName = @contactName,
	                                    Address = @address,
	                                    City = @city,
	                                    PostalCode = @postalCode,
	                                    Country = @country,
	                                    Phone = @phone
                                    WHERE SupplierID = @supplierID;";

                cmd.Parameters.AddWithValue("@supplierID", data.SupplierID);
                cmd.Parameters.AddWithValue("@supplierName", data.SupplierName);
                cmd.Parameters.AddWithValue("@contactName", data.ContactName);
                cmd.Parameters.AddWithValue("@address", data.Address);
                cmd.Parameters.AddWithValue("@city", data.City);
                cmd.Parameters.AddWithValue("@postalCode", data.PostalCode);
                cmd.Parameters.AddWithValue("@country", data.Country);
                cmd.Parameters.AddWithValue("@phone", data.Phone);

                // Sử dụng ExecuteNonQuery khi câu lệnh không trả về kết quả
                result = cmd.ExecuteNonQuery() > 0;

                cn.Close();
            }

            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Supplier> List()
        {
            List<Supplier> data = new List<Supplier>();
            using (SqlConnection cn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"SELECT *
                                    FROM Suppliers
	                                   ";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = cn;


                using (SqlDataReader dbReader = cmd.ExecuteReader(CommandBehavior.CloseConnection))
                {
                    while (dbReader.Read())
                    {
                        data.Add(new Supplier()
                        {
                            
                            SupplierID = Convert.ToInt32(dbReader["SupplierID"]),
                            SupplierName = Convert.ToString(dbReader["SupplierName"])

                        });
                    }
                }

                cn.Close();
            }
            return data;
        }
    }
}
