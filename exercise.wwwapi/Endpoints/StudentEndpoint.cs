
using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;
using exercise.wwwapi.Models.Payload;

namespace exercise.Endpoints {
    public static class StudentEndpoint {

        public static void ConfigureStudentEndpoint(this WebApplication app) {
            var students = app.MapGroup("students");
            students.MapGet("/", GetAllStudents);
            students.MapPost("/", CreateStudent);
            students.MapGet("/{id}", GetStudent);
            students.MapPut("/{FirstName}", UpdateStudent);
            students.MapDelete("/{id}", DeleteStudent);
        }

    public static IResult CreateStudent(IStudentRepository sr, StudentPostPayload payload)
    {
        if (payload == null)
        {
            return Results.BadRequest("Payload cannot be null");
        }

        Student student = sr.AddStudent(payload.FirstName, payload.LastName);
        return Results.Created($"/tasks/{student.FirstName}", student);
    }

    public static IResult DeleteStudent(IStudentRepository sr, string id)
    {
        int i = 0;
        bool isInt = int.TryParse(id, out i);
        if (!isInt)
        {
            return Results.BadRequest("Id Must be a numeric value");
        }

        var deletedStudent = sr.DeleteStudent(id);

        if (deletedStudent == null)
        {
            return Results.NotFound($"Student with Id {id} not found.");
        }

        return Results.Ok(deletedStudent);
    }

    public static IResult GetStudent(IStudentRepository sr, string id)
    {

        int i = 0;
        bool isInt = int.TryParse(id, out i);
        if (!isInt)
        {
            return Results.NotFound($"Must be a numeric value");
        }
        var student = sr.GetStudent(id);
        if(student == null) {
            return Results.NotFound($"Student with Id {id} not found.");
        }
        return Results.Ok(student);
    }

    public static IResult GetAllStudents(IStudentRepository sr)
    {
        return Results.Ok(sr.GetAllStudents());
    }

    public static IResult UpdateStudent(IStudentRepository students, string FirstName, StudentUpdatePayload payload)
    {
        try
        {
            if (payload == null)
            {
                return Results.BadRequest("Payload cannot be null");
            }

            Task<Student> student = students.UpdateStudent(FirstName, payload);

            if (student == null)
            {
                return Results.NotFound($"Student with FirstName {FirstName} not found.");
            }

            return Results.Ok(student);
        }
        catch (Exception e)
        {
            return Results.BadRequest(e.Message);
        }
    }
    }
}