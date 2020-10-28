
using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data {
    public interface ICommanderRepo {
        IEnumerable<Command> GetAllCommand();
        Command GetCommandById(int id);
        void CreateCommand(Command cmd);
        void UpdateCommand(Command cmd);
        void DeleteCommand(int id);
        bool SaveChanges();
        
    }
}