namespace StudentApi.Dtos;

public class CreateStudentDto
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Email { get; set; } = string.Empty;
}
