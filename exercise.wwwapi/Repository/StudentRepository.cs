
using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using exercise.wwwapi.Models.Payload;
using Microsoft.EntityFrameworkCore;


namespace exercise.wwwapi.Repository {

    public class StudentRepository : IStudentRepository
    {
        private readonly StudentDb _dbContext;
        public StudentRepository(StudentDb dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Student> GetAllStudents()
        {
            return _dbContext.Students.ToList();
        }

        public Student AddStudent(string FirstName, string LastName)
        {
            Student student = new Student(FirstName, LastName);
            _dbContext.Students.Add(student);
            _dbContext.SaveChanges();
            return student;
        }

        public Student GetStudent(string id)
        {
            int studentId = ConvertStringToInt(id);
            var stud = _dbContext.Students.FirstOrDefault(s => s.Id == studentId);
            return stud;
        }

        private int ConvertStringToInt(string id) {
            int i = 0;
            bool s = int.TryParse(id, out i);
            return i;
        }

        public async Task DeleteStudent(string id)
        {
            Student student = GetStudent(id);
            if (student == null)
            {
                return;
            }
             _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();        
        }

        public async Task<Student?> UpdateStudent(string FirstName, StudentUpdatePayload updatePayload)
{
            Student student = await _dbContext.Students.FindAsync(FirstName);
            if (student == null)
            {
                return null;
            }

            _dbContext.Entry(student).State = EntityState.Added;

            student.FirstName = updatePayload.FirstName;
            student.LastName = updatePayload.LastName;

            await _dbContext.SaveChangesAsync();

            return student;
        }
    }
}