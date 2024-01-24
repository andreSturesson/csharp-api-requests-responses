

using exercise.wwwapi.Models;
using exercise.wwwapi.Models.Payload;


namespace exercise.wwwapi.Repository {

    public interface IStudentRepository {

        public List<Student> GetAllStudents();
        public Student AddStudent(string FirstName, string LastName);

        public Student? GetStudent(string id);

        public Task DeleteStudent(string id);
        public Task<Student>? UpdateStudent(string FirstName, StudentUpdatePayload updatePayload);
    }
}