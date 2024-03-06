using Project_menegment_tools.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_menegment_tools.Models.Tasks;

public class TasksViewModel
{
    public long Id { get; set; }    

    public string Description { get; set; }

    public string Title { get; set; }

    public DateTime DueDate { get; set; }

    public long ProjectId { get; set; }

    public Priority Priority { get; set; }

    public Status Status { get; set; }

    public long TemMemberId { get; set; }
}
