using Project_menegment_tools.Enums;
using Project_menegment_tools.Models.Commons;

namespace Project_menegment_tools.Models.Tasks;

public class Tasks : Auditable
{ 
    public string Description { get; set; }

    public string Title { get; set; }

    public DateTime DueDate { get; set; }

    public Priority Priority { get; set; }

    public Status Status { get; set; }

    public long ProjectId { get; set; }

    public long TemMemberId {  get; set; } 
}
