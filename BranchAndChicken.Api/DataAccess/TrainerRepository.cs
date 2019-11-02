using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BranchAndChicken.Api.Models;
using System.Data.SqlClient;
using Dapper;

namespace BranchAndChicken.Api.DataAccess
{
    public class TrainerRepository
    {     
        string _connectionString = "Server=localhost;Database=BranchAndChicken;Trusted_Connection=True;";
        public List<Trainer> GetAll()
        {
            using (var db = new SqlConnection(_connectionString))
            {
                db.Open();

                var trainers = db.Query<Trainer>("Select * from Trainer");

                return trainers.AsList();
            }
        }

        public Trainer Get(string name)
        {
            using (var db = new SqlConnection(_connectionString))
            {

                var sql = @"select * 
                            from Trainer 
                            where Trainer.Name = @trainerName";

                var parameters = new { trainerName = name };

                var trainer = db.QueryFirst<Trainer>(sql, parameters);

                return trainer;
            }
        }
    
        public bool Remove(string name)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"delete 
                            from Trainer 
                            where [name] = @name";

                return db.Execute(sql, new {name}) == 1;                             
            }            
        }

        public Trainer Update(Trainer updatedTrainer, int id)
        {
            using (var db = new SqlConnection(_connectionString))
            {

                var sql = @"UPDATE [Trainer]
                            SET [Name] = @name
                            ,[YearsOfExperience] = @yearsOfExperience
                            ,[Specialty] = @specialty
                            output inserted.*
                            WHERE id = @id";

                //var parameters = new
                //{
                //    Id = id,
                //    updatedTrainer.Name,
                //    updatedTrainer.YearsOfExperience,
                //    updatedTrainer.Specialty
                //};

                var trainer = db.QueryFirst<Trainer>(sql, updatedTrainer);

                return trainer;

            }
        }
        
        public Trainer Add(Trainer newTrainer)
        {
            using (var db = new SqlConnection(_connectionString))
            {
                var sql = @"INSERT INTO [Trainer]
                                   ([Name]
                                   ,[YearsOfExperience]
                                   ,[Specialty])
                            output inserted.*
                             VALUES
                                   (@name
                                   ,@yearsOfExperience
                                   ,@specialty)";

                return db.QueryFirst<Trainer>(sql, newTrainer);

            }                     
        }
    }
}
