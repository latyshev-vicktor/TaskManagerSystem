using AnalyticsService.Domain.Enums;

namespace AnalyticsService.Domain.Entities
{
    public class InsightEntity
    {
        public Guid Id { get; } = Guid.NewGuid();
        public InsightType Type { get; }
        public Severity Severity { get; }
        public double Confidence { get; }
        public long SprintId { get; }
        public long UserId { get; }
        public string Message { get; }
        public DateTimeOffset DetectAt { get; } = DateTimeOffset.UtcNow;

        public InsightEntity(
            InsightType type,
            Severity severity,
            double confidence,
            long sprintId,
            long userId,
            string message)
        {
            Type = type;
            Severity = severity;
            Confidence = confidence;
            SprintId = sprintId;
            UserId = userId;
            Message = message;
        }
    }
}
