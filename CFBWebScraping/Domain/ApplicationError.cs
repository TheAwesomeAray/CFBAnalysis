using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class ApplicationError
    {
        public int Id { get; set; }

        public string Area { get; set; }

        public string InnerException { get; set; }
    }
}
