using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_menegment_tools.Models.Commons;

public abstract class Auditable
{
    //
    public long Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; }

    public DateTime DeletedAt { get; set; }

    public bool IsDeleted { get; set; }
}
