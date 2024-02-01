using AutoMapper;
using DemoCRUD.Data.DataContext;
using DemoCRUD.DTO.ModelDtos;
using DemoCRUD.Model.Models;
using DemoCRUD.Service.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoCRUD.Service.Services
{
    public class StudentService:IStudentService
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;


        public StudentService(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;

        }
        /// <summary>
        /// This  method is used to get list of student from database
        /// </summary>

        public async Task<ResponseDto> GetStudentList()
        {
            try
            {
                var studentInfo = await _db.Students
                                    .Include(p => p.Gender)
                                    .Include(p => p.State).ToListAsync();

                if (studentInfo == null || !studentInfo.Any())
                {
                    return new ResponseDto { Success = false, Message = "No list of student found" };
                }

                var students = _mapper.Map<IEnumerable<StudentGet>>(studentInfo);

                return new ResponseDto
                {
                    Success = true,
                    Data = students
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto
                {
                    Success = false,
                    Message = $"Internal Server Error:{ex.Message}",
                };
            }
        }



        /// <summary>
        /// This method is used to delete student from database
        /// </summary>
        /// <param name="Id"></param>

        public async Task<ResponseDto> DeleteStudent(int Id)
        {
            try
            {

                if (Id <= 0)
                {
                    throw new ArgumentException("Id must be a positive integer", nameof(Id));
                }

                var existingUser = await _db.Students
                    .Include(s => s.Gender)
                    .Include(s => s.State)
                    .FirstOrDefaultAsync(s => s.Id == Id);

                if (existingUser == null)
                {
                    return new ResponseDto
                    {
                        Success = false,
                        Message = $"User with Id '{Id}' not found"
                    };
                }

                _db.Students.Remove(existingUser);
                await _db.SaveChangesAsync();

                return new ResponseDto
                {
                    Success = true,
                    Message = $"User with Id '{Id}' deleted successfully",
                    Data = existingUser

                };
            }
            catch (Exception ex)
            {

                return new ResponseDto
                {
                    Success = false,
                    Message = $"Internal Server Error:{ex.Message}"
                };
            }
        }

        /// <summary>
        /// this is used to create  student in database
        /// </summary>
        /// <param name="studentsDto"></param>
        public async Task<ResponseDto> CreateStudent(StudentSave studentsDto)
        {
            try
            {
                if (studentsDto == null)
                {
                    throw new ArgumentNullException(nameof(studentsDto));
                }

                // Check for existing user with the same username
                var existingUser = await _db.Students.Include(s => s.Gender)
              .Include(s => s.State).FirstOrDefaultAsync(s => s.UserName == studentsDto.UserName);

                if (existingUser != null)
                {
                    return new ResponseDto
                    {
                        Success = false,
                        Message = "Username already exists"
                    };
                }

                // Map to entity model
                var newStudent = _mapper.Map<Students>(studentsDto);

                // Add new user
                _db.Students.Add(newStudent);
                await _db.SaveChangesAsync();

                var newUserInfoDtoModel = _mapper.Map<StudentSave>(newStudent);
                return new ResponseDto
                {
                    Success = true,
                    Data = newUserInfoDtoModel
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto
                {
                    Success = false,
                    Message = $"Internal Server Error:{ex.Message}"
                };
            }
        }

        


        /// <summary>
        /// this method is used to update  the database 
        /// </summary>
        /// <param name="studentsDto"></param>

        public async Task<ResponseDto> UpdateStudent(StudentSave studentsDto)
        {
            try
            {
                if (studentsDto == null)
                {
                    throw new ArgumentNullException(nameof(studentsDto));
                }

                if (studentsDto.Id <= 0) // Ensure ID is valid
                {
                    throw new ArgumentException("Id must be a positive integer", nameof(studentsDto.Id));
                }

                var existingUser = await _db.Students
                    .Include(s => s.Gender)
                    .Include(s => s.State)
                    .FirstOrDefaultAsync(u => u.Id == studentsDto.Id); 

                if (existingUser == null)
                {
                    return new ResponseDto
                    {
                        Success = false,
                        Message = "User not found"
                    };
                }

                // Check for username conflict with other users (excluding the current user)
                var conflictingUser = await _db.Students.FirstOrDefaultAsync(u => u.UserName == studentsDto.UserName && u.Id != studentsDto.Id);

                if (conflictingUser != null)
                {
                    return new ResponseDto
                    {
                        Success = false,
                        Message = "Username already exists"
                    };
                }

                // Map to entity model
                _mapper.Map(studentsDto, existingUser);

                // Update existing user
                try
                {
                   await _db.SaveChangesAsync();
                }
                catch (DbUpdateException ex)
                {
                    throw new Exception("Error updating user information: " + ex.InnerException.Message);
                }

                var updatedUserInfoDtoModel = _mapper.Map<StudentSave>(existingUser);
                return new ResponseDto
                {
                    Success = true,
                    Data = updatedUserInfoDtoModel
                };
            }
            catch (Exception ex)
            {

               
                return new ResponseDto
                {
                    Success = false,
                    Message = $"Internal Server Error:{ex.Message}"
                };
            }
        }
        /// <summary>
        /// this method is used to get detail of the  student by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseDto> GetStudentInfoById(int id)
        {
            try
            {
                var studentInfo = await _db.Students
              .Include(s => s.Gender)
              .Include(s => s.State)
              .FirstOrDefaultAsync(s => s.Id == id);


                if (studentInfo == null)
                {
                    return new ResponseDto
                    {
                        Success = false,
                        Message = $"User with ID {id} not found"
                    };
                }

                var studentDetail = _mapper.Map<StudentGet>(studentInfo);
                return new ResponseDto
                {
                    Success = true,
                    Data = studentDetail
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    Success = false,
                    Message = $"Internal Server Error:{ex.Message}"
                };
            }
        }
    }
}
    

