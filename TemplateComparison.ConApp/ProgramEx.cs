using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateComparison.ConApp
{
    partial class Program
    {
        static partial void ClassConstructed()
        {
            SourceLabels = new string[] { StaticLiterals.CodeCopyLabel };
        }
    }
}
