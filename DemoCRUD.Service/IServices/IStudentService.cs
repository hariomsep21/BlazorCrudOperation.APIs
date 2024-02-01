using DemoCRUD.DTO.ModelDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCRUD.Service.IServices
{
    public interface IStudentService
    {
        Task<ResponseDto> GetStudentList();
        Task<ResponseDto> DeleteStudent(int Id);

        Task<ResponseDto> CreateStudent(StudentSave studentsDto);
        Task<ResponseDto> UpdateStudent(StudentSave studentsDto);
        Task<ResponseDto> GetStudentInfoById(int id);
    }
}
