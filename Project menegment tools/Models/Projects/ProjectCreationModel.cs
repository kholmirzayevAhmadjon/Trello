using Project_menegment_tools.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_menegment_tools.Models.Projects;

public class ProjectCreationModel
{
    public string Name { get; set; }

    public string Description { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public Status Status { get; set; }
    public long ProjectMenegerId { get; set; }
}
