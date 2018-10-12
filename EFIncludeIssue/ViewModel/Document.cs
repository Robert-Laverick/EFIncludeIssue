using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFIncludeIssue.ViewModel
{
    public class DocumentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Body { get; set; }
        public ICollection<MetaInfoDto> MetaInfo { get; set; }
    }
}
