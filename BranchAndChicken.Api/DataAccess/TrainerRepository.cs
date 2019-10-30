using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BranchAndChicken.Api.Models;
using System.Data.SqlClient;

namespace BranchAndChicken.Api.DataAccess
{
    public class TrainerRepository
    {
        //static List<Trainer> _trainers = new List<Trainer>
        //{
        //    new Trainer
        //    {
        //        Id = Guid.NewGuid(),
        //        FullName = "Nathan",
        //        Specialty = Specialty.TaeCluckDo,
        //        YearsOfExperience = 0
        //    },

        //    new Trainer
        //    {
        //        Id = Guid.NewGuid(),
        //        FullName = "Martin",
        //        Specialty = Specialty.Chudo,
        //        YearsOfExperience = 12
        //    },

        //    new Trainer
        //    {
        //        Id = Guid.NewGuid(),
        //        FullName = "Adam",
        //        Specialty = Specialty.ChravBacaw,
        //        YearsOfExperience = 5
        //    }
        //};

        string _connectionString = "Server=localhost;Database=BranchAndChicken;Trusted_Connection=True;";
        public List<Trainer> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = "Select * From Trainer";

                var dataReader = cmd.ExecuteReader();

                var trainers = new List<Trainer>();

                while (dataReader.Read())
                {
                    trainers.Add(GetTrainerFromDataReader(dataReader));
                }

                return trainers;

            }
        }

        public Trainer Get(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = @"select * 
                                  from Trainer 
                                  where Trainer.Name = @trainerName";

                // parameterization
                cmd.Parameters.AddWithValue("trainerName", name);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return GetTrainerFromDataReader(reader);
                }
            }

            return null;

            //var trainer = _trainers.FirstOrDefault(t => t.FullName == name);
            //return trainer;
        }


        Trainer GetTrainerFromDataReader(SqlDataReader reader)
        {
            // explicit cast - will throw exception
            var id = (int)reader["Id"];
            // implicit cast - nullable
            var name = reader["Name"] as string;
            // convert to - will throw exception
            var yearsOfExperience = Convert.ToInt32(reader["YearsOfExperience"]);
            // try parse --> output variable
            Enum.TryParse<Specialty>(reader["specialty"].ToString(), out var specialty);

            var trainer = new Trainer
            {
                Specialty = specialty,
                Id = id,
                Name = name,
                YearsOfExperience = yearsOfExperience
            };

            return trainer;
        }

        public bool Remove(string name)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = @"delete 
                                    from Trainer 
                                    where [name] = @name";

                cmd.Parameters.AddWithValue("name", name);

                return cmd.ExecuteNonQuery() == 1;
            }

            //var trainer = Get(name);
            //_trainers.Remove(trainer);
        }

        public Trainer Update(Trainer updatedTrainer, int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = @"
                                    UPDATE [Trainer]
                                        SET [Name] = @name
                                          ,[YearsOfExperience] = @yearsOfExperience
                                          ,[Specialty] = @specialty
                                    output inserted.*
                                     WHERE id = @id";

                cmd.Parameters.AddWithValue("name", updatedTrainer.Name);
                cmd.Parameters.AddWithValue("yearsOfExperience", updatedTrainer.YearsOfExperience);
                cmd.Parameters.AddWithValue("specialty", updatedTrainer.Specialty);
                cmd.Parameters.AddWithValue("id", id);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return GetTrainerFromDataReader(reader);
                }

                return null;
            }

            //var trainerToUpdate = _trainers.First(trainer => trainer.Id == id);
            //trainerToUpdate.FullName = updatedTrainer.FullName;
            //trainerToUpdate.YearsOfExperience = updatedTrainer.YearsOfExperience;
            //trainerToUpdate.Specialty = updatedTrainer.Specialty;
            //return trainerToUpdate;
        }

        public Trainer Add(Trainer newTrainer)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var cmd = connection.CreateCommand();
                cmd.CommandText = @"INSERT INTO [Trainer]
                                   ([Name]
                                   ,[YearsOfExperience]
                                   ,[Specialty])
                            output inserted.*
                             VALUES
                                   (@name
                                   ,@yearsOfExperience
                                   ,@specialty)";

                cmd.Parameters.AddWithValue("name", newTrainer.Name);
                cmd.Parameters.AddWithValue("yearsOfExperience", newTrainer.YearsOfExperience);
                cmd.Parameters.AddWithValue("specialty", newTrainer.Specialty);

                var reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    return GetTrainerFromDataReader(reader);
                };

                return null;
            }
            //_trainers.Add(newTrainer);
            //return newTrainer;                
        }
    }
}
