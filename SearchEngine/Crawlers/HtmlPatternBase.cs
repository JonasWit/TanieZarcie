using System;
using System.Collections.Generic;
using System.Text;

namespace SearchEngine.Crawlers
{
    public class HtmlPatternBase
    {
        public string PatternName { get; set; }

        public NodeDetails TopNode { get; set; }

        public struct NodeDetails
        {
            public string Descendant { get; set; }

            public string AttributeValue { get; set; }

            public string AttributeName { get; set; }

            public List<string> CombinedAttributeName { get; set; }
        }
    }
}
