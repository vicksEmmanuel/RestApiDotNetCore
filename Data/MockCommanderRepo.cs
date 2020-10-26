using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data {
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommand()
        {
            var commands = new List<Command>(){
                new Command{Id= 0, HowTo="Boil an egg", Line="Boil water", Platform="Kettle & Pan"},
                new Command{Id= 1, HowTo="Cut Bread", Line="Get a Knife", Platform="Knife and chopping board"},
                new Command{Id= 2, HowTo="Make cup of tea", Line="Place teabag in cup", Platform="Kettle & Cup"}
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command{Id= 0, HowTo="Boil an egg", Line="Boil water", Platform="Kettle & Pan"};
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}