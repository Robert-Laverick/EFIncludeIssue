using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFIncludeIssue.Model
{
    public class MetaInfo
    {
        public Guid Id { get; set; }
        public Guid DocumentId { get; set; }
        public virtual Document Document { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
