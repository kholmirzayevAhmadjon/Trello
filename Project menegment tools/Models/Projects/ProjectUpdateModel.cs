using Project_menegment_tools.Enums;

namespace Project_menegment_tools.Models.Projects;

public class ProjectUpdateModel
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime EndTime { get; set; }

    public Status Status { get; set; }

    public long ProjectMenegerId { get; set; }
}
