using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BranchAndChicken.Api.Models;
using BranchAndChicken.Api.DataAccess;
using BranchAndChicken.Api.Commands;

namespace BranchAndChicken.Api.Controllers
{
    [Route("api/trainers")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<Trainer>> GetAllTrainers()
        {
            var repo = new TrainerRepository();
            return repo.GetAll();
        }

        [HttpGet("{name}")]
        public ActionResult<Trainer> GetByName(string name)
        {
            var repo = new TrainerRepository();
            return repo.Get(name);
        }

        //[HttpGet("{specialty}")]
        //public ActionResult<Trainer> GetBySpecialty(string specialty)
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpDelete("{name}")]
        //public IActionResult DeleteTrainer(string name)
        //{
        //    var repo = new TrainerRepository();
        //    repo.Remove(name);

        //    return Ok();
        //}

        //[HttpPut("{id}")]
        //public IActionResult UpdateTrainer(UpdateTrainerCommand updatedTrainerCommand, Guid id)
        //{
        //    var repo = new TrainerRepository();

        //    var updatedTrainer = new Trainer
        //    {
        //        FullName = updatedTrainerCommand.FullName,
        //        YearsOfExperience = updatedTrainerCommand.YearsOfExperience,
        //        Specialty = updatedTrainerCommand.Specialty
        //    };

        //    var trainerThatGotUpdated = repo.Update(updatedTrainer, id);

        //    return Ok(trainerThatGotUpdated);
        //}

        //[HttpPost]
        //public IActionResult CreateTrainer(AddTrainerCommand newTrainerCommand)
        //{
        //    var newTrainer = new Trainer
        //    {
        //        Id = Guid.NewGuid(),
        //        FullName = newTrainerCommand.FullName,
        //        YearsOfExperience = newTrainerCommand.YearsOfExperience,
        //        Specialty = newTrainerCommand.Specialty,
        //    };

        //    var repo = new TrainerRepository();
        //    var trainerThatGotCreated = repo.Add(newTrainer);

        //    return Created($"api/trainers/{trainerThatGotCreated.FullName}", trainerThatGotCreated);
        //}
    }
}