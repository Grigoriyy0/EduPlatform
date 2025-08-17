using EduNEXT.Application.Ports;
using EduNEXT.Core.Domain.Entities;
using Hangfire;
using MediatR;
using IPublisher = EduNEXT.Application.Ports.IPublisher;

namespace EduNEXT.Application.Events.TimeSlotCreated;

public class TimeSlotCreatedEventHandler(
    ILessonsRepository lessonsRepository, 
    IPublisher publisher)
    : INotificationHandler<TimeSlotCreatedEvent>
{
    public async Task Handle(TimeSlotCreatedEvent notification, CancellationToken cancellationToken)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);
        
        var endOfMonth = new DateOnly(today.Year, today.Month, DateTime.DaysInMonth(today.Year, today.Month));

        var date = today;
        
        while (date <= endOfMonth)
        {
            var dayOfWeek = date.DayOfWeek;
            
            if ((int)dayOfWeek == notification.Day)
            {
                var start = notification.StartTime;
                
                var end = notification.EndTime;
                
                var lessonResult = Lesson.Create(date, start, end, notification.StudentId);

                if (lessonResult.IsSuccess)
                {
                    await lessonsRepository.CreateLessonAsync(lessonResult.Value);

                    var scheduleDate = date;
                    
                    BackgroundJob.Schedule(() =>
                            publisher.SendToQueueAsync(
                                $"У вас был урок с {notification.StudentName} {scheduleDate} в {start}?"
                            ),
                        new DateTimeOffset(date.Year, date.Month, date.Day,
                            end.Hours, end.Minutes, 0,
                            TimeSpan.Zero));
                }
            }
            
            date = date.AddDays(1);
        }
    }
}