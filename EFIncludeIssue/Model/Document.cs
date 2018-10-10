using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFIncludeIssue.Model
{
    public class Document
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public virtual ICollection<MetaInfo> MetaInfo { get; set; }
    }
}
