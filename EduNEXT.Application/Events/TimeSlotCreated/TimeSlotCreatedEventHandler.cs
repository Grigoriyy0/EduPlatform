using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using MediatR;

namespace EduNEXT.Application.Events.TimeSlotCreated;

public class TimeSlotCreatedEventHandler(ILessonsRepository lessonsRepository)
    : INotificationHandler<TimeSlotCreatedEvent>
{
    public async Task Handle(TimeSlotCreatedEvent notification, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        
        var endOfMonth = new DateOnly(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));

        var date = today;

        while (date <= endOfMonth)
        {
            if ((int)date.DayOfWeek == notification.Day)
            {
                var start = notification.StartTime;
                
                var end = notification.EndTime;
                
                var lessonResult = Lesson.Create(date, start, end, notification.StudentId);

                if (lessonResult.IsSuccess)
                {
                    await lessonsRepository.CreateLessonAsync(lessonResult.Value);
                }
            }
            
            date = date.AddDays(1);
        }
    }
}