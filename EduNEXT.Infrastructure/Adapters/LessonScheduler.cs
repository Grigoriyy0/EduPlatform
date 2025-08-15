using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using Hangfire;
using Microsoft.Extensions.Logging;

namespace EduNEXT.Infrastructure.Adapters;

public class LessonScheduler
{
    private readonly IStudentRepository _studentRepository;
    private readonly IPublisher _publisher;
    private readonly ILessonsRepository _lessonsRepository;
    private readonly ILogger<LessonScheduler> _logger;
    
    public LessonScheduler(
        IStudentRepository studentRepository, 
        ILessonsRepository lessonsRepository, 
        IPublisher publisher, 
        ILogger<LessonScheduler> logger)
    {
        _studentRepository = studentRepository;
        _lessonsRepository = lessonsRepository;
        _publisher = publisher;
        _logger = logger;
    }

    public async Task PlanLessonsForNextMonthAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Planing lessons for next month");
        var students = await _studentRepository.GetAllStudentsAsync();

        var firstDay = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddMonths(1);
        var lastDay = new DateTime(firstDay.Year, firstDay.Month, DateTime.DaysInMonth(firstDay.Year, firstDay.Month));
        
        foreach (var student in students)
        {
            var timeSlots = student.TimeSlots;

            for (var date = firstDay; date <= lastDay; date = date.AddDays(1))
            {
                var slots = timeSlots.Where(slot => slot.Day == (int)date.DayOfWeek).ToList();

                foreach (var slot in slots)
                {
                    var lessonDate = DateOnly.FromDateTime(date);
                    
                    var lessonResult = Lesson.Create(lessonDate, slot.StartTime, slot.EndTime, student.StudentId);
                    
                    
                    if (lessonResult.IsSuccess)
                    {
                        await _lessonsRepository.CreateLessonAsync(lessonResult.Value);
                        BackgroundJob.Schedule(() =>
                                _publisher.SendToQueueAsync(
                                    $"У вас был урок с {student.FirstName} {student.LastName}"
                                ),
                            new DateTimeOffset(date.Year, date.Month, date.Day,
                                slot.EndTime.Hours, slot.EndTime.Minutes, 0,
                                TimeSpan.Zero));

                    }
                }
            }
        }
    }
}