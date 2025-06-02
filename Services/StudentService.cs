using MongoDB.Driver;
using StudentApi.Models;
using StudentApi.Dtos;

namespace StudentApi.Services;

public class StudentService
{
    private readonly IMongoCollection<Student> _students;

    public StudentService(IConfiguration config)
    {
        var client = new MongoClient(config["StudentDatabaseSettings:ConnectionString"]);
        var database = client.GetDatabase(config["StudentDatabaseSettings:DatabaseName"]);
        _students = database.GetCollection<Student>(config["StudentDatabaseSettings:CollectionName"]);
    }

    public async Task<List<Student>> GetAllAsync() => await _students.Find(_ => true).ToListAsync();
    public async Task<Student?> GetByIdAsync(string id) => await _students.Find(s => s.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(CreateStudentDto dto)
    {
        var student = new Student { Name = dto.Name, Age = dto.Age, Email = dto.Email };
        await _students.InsertOneAsync(student);
    }

    public async Task UpdateAsync(string id, UpdateStudentDto dto)
    {
        var updated = new Student { Id = id, Name = dto.Name, Age = dto.Age, Email = dto.Email };
        await _students.ReplaceOneAsync(s => s.Id == id, updated);
    }

    public async Task DeleteAsync(string id) => await _students.DeleteOneAsync(s => s.Id == id);
}
