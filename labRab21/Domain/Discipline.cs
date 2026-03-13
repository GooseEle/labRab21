using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labRab21.Domain
{
    internal class Discipline
    {
        public Discipline() { }
        public Discipline(string name, Status status)
        {
            this.Name = name;
            this.Status = status;
        }

        public string Name { get; set; }
        public Status Status { get; set; }
    }
}
