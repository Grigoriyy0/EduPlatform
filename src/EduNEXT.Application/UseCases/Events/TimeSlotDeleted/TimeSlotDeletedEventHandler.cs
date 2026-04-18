using EduNEXT.Application.Ports;
using MediatR;

namespace EduNEXT.Application.UseCases.Events.TimeSlotDeleted;

public class TimeSlotDeletedEventHandler(ILessonsRepository repository) : INotificationHandler<TimeSlotDeletedEvent>
{
    public async Task Handle(TimeSlotDeletedEvent notification, CancellationToken ct)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        
        var endOfMonth = new DateOnly(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));

        var lessons = await repository.GetStudentLessonRangeAsync(today, endOfMonth, notification.StudentId, ct);

        foreach (var lesson in lessons)
        {
            if ((int)lesson.Date.DayOfWeek == notification.Day &&
                lesson.StartTime == notification.StartTime
                && lesson.EndTime == notification.EndTime)
            {
                await repository.DeleteAsync(lesson, ct);
            }
        }
    }
}