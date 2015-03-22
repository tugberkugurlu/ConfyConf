using System;

namespace ConfyConf.Commands
{
    public class CreateUserCommand : Command
    {
        private readonly string _id;
        private readonly string _name;

        public CreateUserCommand(string id, string name)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            _id = id;
            _name = name;
        }

        public string Id
        {
            get { return _id; }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}