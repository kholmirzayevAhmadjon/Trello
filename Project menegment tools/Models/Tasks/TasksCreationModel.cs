using Project_menegment_tools.Enums;

namespace Project_menegment_tools.Models.Tasks;

public class TasksCreationModel
{
    public string Description { get; set; }

    public string Title { get; set; }

    public DateTime DueDate { get; set; }

    public long ProjectId { get; set; }

    public Priority Priority { get; set; }

    public Status Status { get; set; }

    public long TemMemberId { get; set; }
}
