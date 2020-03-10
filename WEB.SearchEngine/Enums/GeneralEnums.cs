﻿namespace WEB.SearchEngine.Enums
{
    public enum Shops
    { 
        Biedronka = 0,
        Lidl = 1,
        Kaufland = 3
    }

    public enum MatchDirection
    {
        InputContainsOutput = 0,
        OutputContainsInput = 1,
        Equals = 3
    }

    public enum HtmlTags
    {
        div = 0,
        p = 1,
        span = 2,
        h5 = 3,
        h4 = 4,
        h3 = 5,
        a = 6,
    }

    public enum HtmlAttributes
    {
        ClassAttribute = 0,
    }
}