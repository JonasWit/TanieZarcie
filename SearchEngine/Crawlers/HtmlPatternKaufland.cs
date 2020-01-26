using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngine.Crawlers
{
    public class HtmlPatternKaufland : HtmlPatternBase
    {
        public NodeDetails PriceNode { get; set; }

        public NodeDetails Description { get; set; }

        public NodeDetails Name { get; set; }

        public NodeDetails SubName { get; set; }
    }
}
