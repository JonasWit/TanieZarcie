using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngine.Crawlers
{
    public class HtmlPatternBiedronka : HtmlPatternBase
    {
        public NodeDetails ZlNode { get; set; }

        public NodeDetails GrNode { get; set; }

        public NodeDetails Description { get; set; }

        public NodeDetails Name { get; set; }
    }
}
