using DemoCRUD.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCRUD.DTO.ModelDtos
{
    public class StudentGet:StudentsDto
    {
        public string GenderName { get; set; }
        public string StateName { get; set; }
        public int GenderID {  get; set; }
        public int StateID { get; set; }
    }
}
