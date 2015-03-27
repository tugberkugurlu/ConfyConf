using System;

namespace ConfyConf.Domain.Events
{
    public class UserCreatedEvent : IDomainEvent
    {
        private readonly string _id;
        private readonly string _name;

        public UserCreatedEvent(string id, string name)
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
