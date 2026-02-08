using AnalyticsService.Application.Dto;
using AnalyticsService.Application.Interfaces.Detectors;
using AnalyticsService.Domain.Entities;

namespace AnalyticsService.Application.DetectorPipelines
{
    public class InsightDetectionPipeline(IEnumerable<IInsightDetector<SprintAnalyticsContext>> detectors)
    {
        public async Task<IReadOnlyList<InsightEntity>> Deletect(SprintAnalyticsContext context)
        {
            var result = new List<InsightEntity>();
            foreach(var detector in detectors)
            {
                var insight = await detector.Detect(context);
                if(insight != null)
                    result.Add(insight);
            }

            return result;
        }
    }
}
