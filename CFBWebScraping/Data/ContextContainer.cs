using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class ContextContainer
    {
        public ContextContainer(string connectionString)
        {
            Context = new CFBContext(connectionString);
        }

        public CFBContext Context { get; set; }
    }
}
