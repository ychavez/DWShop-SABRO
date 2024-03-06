using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace OnlySpans
{
    [MemoryDiagnoser(false)]
    [SimpleJob(RuntimeMoniker.Net70)]
    [SimpleJob(RuntimeMoniker.Net80)]
    public class Benchi
    {

        private IEnumerable<int> _items;
        [GlobalSetup]
        public void Setup()
        {
            _items = Enumerable.Range(1, 2000).ToArray();
        }

        [Benchmark]
        public double avg() => _items.Average();

        [Benchmark]
        public int Sum() => _items.Sum();

        [Benchmark]
        public int Min() => _items.Min();

        [Benchmark]
        public int Max() => _items.Max();

        [Benchmark]
        public int MaxClassic()
        {
            int biggest = int.MinValue;

            foreach (var item in _items)
            {
                if (item > biggest)
                    biggest = item;
            }
            return biggest;
        }


        //public static readonly string _dateAsText = "26 02 2023";
        //[Benchmark]
        //public (int day, int month, int year) DateWithString()
        //{
        //    var dayAsText = _dateAsText.Substring(0, 2);
        //    var monthAsText = _dateAsText.Substring(3, 2);
        //    var yearAsText = _dateAsText.Substring(6);

        //    var day = int.Parse(dayAsText);
        //    var month = int.Parse(monthAsText);
        //    var year = int.Parse(yearAsText);

        //    return (day, month, year);
        //}

        //[Benchmark]
        //public (int day, int month, int year) DateWithStringSpan()
        //{
        //    ReadOnlySpan<char> dateSpan = _dateAsText;
        //    var dayAsText = dateSpan.Slice(0, 2);
        //    var monthAsText = dateSpan.Slice(3, 2);
        //    var yearAsText = dateSpan.Slice(6);

        //    var day = int.Parse(dayAsText);
        //    var month = int.Parse(monthAsText);
        //    var year = int.Parse(yearAsText);

        //    return (day, month, year);
        //}


    }
}
