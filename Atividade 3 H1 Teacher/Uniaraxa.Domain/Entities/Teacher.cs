using System;
using System.Collections.Generic;
using System.Text;

namespace Uniaraxa.Domain.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string urlLattes { get; set; }
        public DateOnly DataAdmissao { get; set; }
    }
}
