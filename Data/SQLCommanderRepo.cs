using System;
using System.Collections.Generic;
using System.Linq;
using Commander.Models;

namespace Commander.Data {
    public class SQLCommanderRepo : ICommanderRepo
    {
        private CommanderContext _context;

        public SQLCommanderRepo(CommanderContext context)
        {
            _context = context;
        }

        public void CreateCommand(Command cmd)
        {
            if(cmd == null) {
                throw new ArgumentNullException(nameof(cmd));
            }

            _context.Commands.Add(cmd);
        }

        public void DeleteCommand(int id)
        {
            _context.Commands.Remove(_context.Commands.FirstOrDefault(p => p.Id == id));
        }

        public IEnumerable<Command> GetAllCommand()
        {
            return _context.Commands.ToList();
        }

        public Command GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void UpdateCommand(Command cmd)
        {
            _context.Commands.Update(cmd);
        }
    }
}