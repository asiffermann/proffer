namespace Proffer.Templating.Handlerbars.Tests.Stubs
{
    using System;

    public class FormatContext
    {
        public DateTime Date { get; set; }

        public int Integer { get; set; }

        public decimal Decimal { get; set; }

        public float Float { get; set; }

        public double Double { get; set; }

        public DateTime? NullableDate { get; set; }

        public int? NullableInteger { get; set; }

        public decimal? NullableDecimal { get; set; }

        public float? NullableFloat { get; set; }

        public double? NullableDouble { get; set; }

        public Unformattable Others { get; set; } = new();

        public class Unformattable
        {
            public override string ToString() => "Unformattable test value";
        }
    }
}
