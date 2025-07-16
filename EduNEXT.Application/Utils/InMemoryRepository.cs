using EduNEXT.Core.Domain.Entities;

namespace EduNEXT.Application.Utils;

public class InMemoryRepository
{
    private readonly List<Student> _students = [];
    
    private readonly Dictionary<Guid, List<StudentTimeSlots>> _timeSlots = new();

    public void AddStudent(Student student)
    {
        _students.Add(student);
    }

    public Student? GetStudent(Guid studentId)
    {
        return _students.FirstOrDefault(x => x.Id == studentId);
    }

    public void AddTimeSlot(Guid studentId, StudentTimeSlots timeSlot)
    {
        if (_timeSlots.ContainsKey(studentId))
        {
            _timeSlots[studentId].Add(timeSlot);
        }
        
        _timeSlots.Add(studentId, [timeSlot]);
    }

    public List<StudentTimeSlots> GetTimeSlot(Guid studentId)
    {
        return _timeSlots[studentId];
    }
}