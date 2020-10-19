using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AasxPackageExplorer.Models
{

    public record AasView
    {
        public string TypeName { get; set; }
        public string IdShort { get; set; }
        public int SubmodelCount { get; set; }
    }

    public record TecalogExportXml
    {
        public string Url { get; set; }
    }
}
