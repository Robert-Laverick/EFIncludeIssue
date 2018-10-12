using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFIncludeIssue.ViewModel
{
    class AuthoredDocumentDto
    {
        public MetaInfoDto Author { get; set; }
        public DocumentDto Document { get; set; }
    }
}
