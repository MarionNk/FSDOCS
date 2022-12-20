using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FSDOCS.Shared.DataTransferObjects
{
    public class GroupeRole
    {
        public string codegroupe { get; set; }
        public List<RoleGet> roles { get; set; }
        public GroupeCreate groupe { get; set; }
    }
}
