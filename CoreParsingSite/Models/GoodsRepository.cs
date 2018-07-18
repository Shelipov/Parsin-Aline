using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CoreParsingSite.Models
{
    public class GoodsRepository:IGoodsRepository
    {
        string connectionString = null;
        public GoodsRepository(string conn)
        {
            connectionString = conn;
        }

        

        public List<Goods> GetGoods()
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Goods>("SELECT * FROM Goods").ToList();
            }
        }

        public Goods Get(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                return db.Query<Goods>("SELECT * FROM Goods WHERE Id = @id", new { id }).FirstOrDefault();
            }
        }

        public void Create(Goods goods)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "INSERT INTO Goods (Foto, Description,Name,Price,Stock) VALUES(@Foto,@Description,@Name,@Price,@Stock)";
                db.Execute(sqlQuery, goods);
                
            }
        }

        public void Update(Goods goods)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "UPDATE Goods SET Foto=@Foto,Description=@Description,Name=@Name,Price=@Price,Stock=@Stock";
                db.Execute(sqlQuery, goods);
            }
        }

        public void Delete(int id)
        {
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                var sqlQuery = "DELETE FROM Goods WHERE Id = @id";
                db.Execute(sqlQuery, new { id });
            }
        }
        

    }
}
