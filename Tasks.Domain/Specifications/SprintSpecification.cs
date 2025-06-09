using Microsoft.EntityFrameworkCore;
using NSpecifications;
using Tasks.Domain.Entities;

namespace Tasks.Domain.Specifications
{
    public static class SprintSpecification
    {
        public static Spec<SprintEntity> ById(long id)
            => new(x => x.Id == id);

        public static Spec<SprintEntity> ByUserId(long userId)
            => new(x => x.UserId == userId);

        public static Spec<SprintEntity> ByStatus(string statusName)
            => new(x => x.Status.Value == statusName);

        public static Spec<SprintEntity> ByName(string name)
           => new(x => x.Name.Name.ToLower().Contains(name.ToLower()));

        public static Spec<SprintEntity> ByDescription(string description)
            => new(x => x.Description.Description.ToLower().Contains(description.ToLower()));

        public static Spec<SprintEntity> ByFieldActivities(long[] fieldActivityIds)
            => new(x => x.SprintFieldActivities.Any(sf => fieldActivityIds.Contains(sf.FieldActivityId)));

        public static Spec<SprintEntity> LessDateEnd(DateTimeOffset endDate)
            => new(x => x.EndDate < endDate);

        public static Spec<SprintEntity> ModeStartEnd(DateTimeOffset startDate)
            => new(x => x.StartDate < startDate);
    }
}
