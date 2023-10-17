using System.Diagnostics;

namespace ASD1
{
    
    internal class Program
    {
        static long code = 0;
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("duzy4.txt");
            string line = sr.ReadLine();
            List<long> fortunes = new List<long>();
            List<Range> ranges = new List<Range>();

            string[] firstLine = line.Split(" ");

            int n = int.Parse(firstLine[0]);
            int m = int.Parse(firstLine[1]);
            for (long i = 0; i < n; i++)
            {
                line = sr.ReadLine();
                fortunes.Add(long.Parse(line));
            }
            for (long i = 0; i < m; i++)
            {
                line = sr.ReadLine();
                string[] sRange = line.Split(" ");
                ranges.Add(new Range(long.Parse(sRange[0]), long.Parse(sRange[1])));
            }
            sr.Close();
            Stopwatch stopwatch= new Stopwatch();
            stopwatch.Start();
            //Lazy(ranges, fortunes);
            fortunes = Fast(ranges, fortunes);
            stopwatch.Stop();
            int k = 0;
            /*foreach (int fortune in fortunes)
            {
                Console.WriteLine(k+": "+fortune);
                k++;
            }*/
            Console.WriteLine("-----------------------");
            StreamWriter streamWriter = new StreamWriter("output.txt");
            foreach (Range range in ranges)
            {
                streamWriter.WriteLine(range.count.ToString());
            }
            Console.WriteLine($"Operacje: {code}, czas: {stopwatch.Elapsed}");
            streamWriter.Close();
        }
        static List<long> MergeSort(List<long> list)
        {
            if (list.Count > 1)
            {
                List<long> leftList;
                List<long> rightList;
                leftList = list.Take(list.Count / 2).ToList();
                rightList = list.Skip(list.Count / 2).ToList();
                leftList = MergeSort(leftList);
                rightList = MergeSort(rightList);
                list = Merge(leftList, rightList);
            }
            return list;
        }
        static List<long> Merge(List<long> leftList, List<long> rightList)
        {
            int r = 0;
            int l = 0;
            List<long> merged = new List<long>();

            while (l < leftList.Count && r < rightList.Count)
            {
                //code++;
                if (leftList[l] <= rightList[r])
                {
                    merged.Add(leftList[l]);
                    l++;
                }
                else
                {
                    merged.Add(rightList[r]);
                    r++;
                }
            }

            merged.AddRange(leftList.Skip(l));
            merged.AddRange(rightList.Skip(r));

            return merged;
        }
        static void Lazy(List<Range> ranges, List<long> fortunes)
        {
            foreach (long fortune in fortunes)
            {
                foreach (Range range in ranges)
                {
                    code++;
                    if (fortune >= range.start && fortune <= range.end) range.count++;
                }
            }
        }
        static List<long> Fast(List<Range> ranges, List<long> fortunes)
        {
            fortunes = MergeSort(fortunes);
            foreach (Range range in ranges)
            {
                range.count = Top(fortunes, range.end) - Bot(fortunes, range.start) + 1;
            }
            return fortunes;
        }
        static int Bot(List<long> fortunes, long value)
        {
            int begin = 0;
            int end = fortunes.Count();
            while (begin < end)
            {
                int mid = (begin + end) / 2;
                if (value > fortunes[mid])
                    begin = mid + 1;
                else
                    end = mid;
                code++;
            }
            code++;
            if (fortunes[begin] < value)
                return begin + 1;
            else return begin;
        }
        static int Top(List<long> fortunes, long value)
        {
            int begin = 0;
            int end = fortunes.Count();
            while (begin < end)
            {
                int mid = (begin + end) / 2;
                if (value > fortunes[mid])
                    begin = mid + 1;
                else
                    end = mid;
                code++;
            }
            code++;
            if (fortunes[begin] > value)
                return begin - 1;
            else return begin;
        }
    }

    public class Range
    {
        public long start;
        public long end;
        public long count;
        public Range(long start, long end)
        {
            this.start = start;
            this.end = end;
            count = 0;
        }
    }
}