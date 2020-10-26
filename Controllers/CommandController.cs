using System;
using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
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
        public ActionResult<IEnumerable<Command>> GetAllCommand() {
            var commandItems = _repository.GetAllCommand();
            return Ok(commandItems);
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
    }
}