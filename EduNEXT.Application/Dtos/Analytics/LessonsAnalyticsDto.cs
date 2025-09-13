namespace EduNEXT.Application.Dtos.Analytics;

public class LessonsAnalyticsDto
{
    public int LessonsCount { get; set; }
    
    public int LessonsTodayCount { get; set; }

    public List<LessonDto> LessonsPending { get; set; } = [];

    public List<int> LessonsDayCount { get; set; } = [];
}