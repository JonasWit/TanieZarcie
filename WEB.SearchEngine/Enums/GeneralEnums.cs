namespace WEB.SearchEngine.Enums
{
    public enum Shops
    { 
        None = 0,
        Biedronka = 1,
        Lidl = 2,
        Kaufland = 3,
        Carrefour = 4,
        Auchan = 5,
        Stokrotka = 6,
        Zabka = 7,
        Castorama = 8,
        Obi = 9,
        LeroyMerlin = 10,
        Aldi = 11
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
