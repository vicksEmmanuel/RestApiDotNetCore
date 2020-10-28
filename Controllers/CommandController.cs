using System;
using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers {

    [Route("api/commands")]
    [ApiController]
    public class CommandController : ControllerBase {
        private readonly IMapper _mapper;
        private readonly ICommanderRepo _repository;

        public CommandController(ICommanderRepo repository, IMapper mapper) {
            _mapper = mapper;
            _repository = repository;
        }
        // private readonly MockCommanderRepo _repository =  new MockCommanderRepo();

        //GET api/command
        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommand() {
            var commandItems = _repository.GetAllCommand();
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        //GET api/command/{id}
        [HttpGet("{id}", Name="GetCommandById")]
        public ActionResult<CommandReadDto> GetCommandById(int id) {
            var comamndItem = _repository.GetCommandById(id);
            if (comamndItem == null) {
                return NotFound();
            }
            // return Ok(comamndItem);
            return Ok(_mapper.Map<CommandReadDto>(comamndItem));
        }

        //POST api/command/
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto) {
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges();

            var dtocommandModel = _mapper.Map<CommandReadDto>(commandModel);
            return CreatedAtRoute(nameof(GetCommandById), new {Id = dtocommandModel.Id}, dtocommandModel);
        }

        //PUT api/command/{id}
        [HttpPut("{id}")]
        public ActionResult<CommandReadDto> UpdateCommand(int id, CommandUpdateDto commandUpdateDto) {
            var commandModelFromRepo = _repository.GetCommandById(id);
            if (commandModelFromRepo == null) {
                return NotFound();
            }
            _mapper.Map(commandUpdateDto, commandModelFromRepo);
            _repository.UpdateCommand(commandModelFromRepo);
            _repository.SaveChanges();

            var dtocommandModel = _mapper.Map<CommandReadDto>(commandModelFromRepo);
            return Ok(dtocommandModel);
            // return NoContent();
        }

        // DELETE api/command/{id}
        [HttpDelete("{id}")]
        public ActionResult<DeleteDto> DeleteCommand(int id) {
            var commandExists = _repository.GetCommandById(id);
            if (commandExists == null) {
                return NotFound();
            }
            _repository.DeleteCommand(id);
            _repository.SaveChanges();

            return Ok(new DeleteDto{Status=200, Message="The result has been deleted"});
        }

        //PATCH api/command/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDocument) {
            var commandExists = _repository.GetCommandById(id);
            if (commandExists == null) {
                return NotFound();
            }

            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandExists);
            patchDocument.ApplyTo(commandToPatch, ModelState);
            if(!TryValidateModel(commandToPatch)) {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(commandToPatch, commandExists);
            _repository.UpdateCommand(commandExists);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}