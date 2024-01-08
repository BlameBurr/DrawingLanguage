using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AES
{
    public abstract class Command
    {
        private string name, description;
        private List<string> parameters;

        public Command(string name, List<string> parameters, string description)
        {
            Name = name;
            Description = description;
            Parameters = parameters;
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public List<string> Parameters
        {
            get
            {
                return parameters;
            }
            
            set
            {
                parameters = value;
            }
        }

    }
}
