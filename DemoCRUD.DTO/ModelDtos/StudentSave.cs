using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCRUD.DTO.ModelDtos
{
    public class StudentSave:StudentsDto
    {
        public int GenderID { get; set; }
        public int StateID { get; set; }
    }
}
