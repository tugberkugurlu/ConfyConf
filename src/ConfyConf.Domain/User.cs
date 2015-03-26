using System;

namespace ConfyConf.Domain
{
    public class User : AggregateRoot
    {
        public User(string id, string name)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }

            if (name == null)
            {
                throw new ArgumentNullException("name");
            }

            Id = id;
            Name = name;
        }

        public string Name { get; private set; }
    }
}