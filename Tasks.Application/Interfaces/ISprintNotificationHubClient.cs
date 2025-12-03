using Tasks.Application.Dto;

namespace Tasks.Application.Interfaces
{
    public interface ISprintNotificationHubClient
    {
        Task SprintStatusUpdated(SprintDto sprintDto);
    }
}
