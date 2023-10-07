namespace ASD1
{
    
    internal class Program
    {
        static int code = 0;
        static void Main(string[] args)
        {
            StreamReader sr = new StreamReader("input.txt");
            string line = sr.ReadLine();
            List<int> fortunes = new List<int>();
            List<Range> ranges = new List<Range>();

            string[] firstLine = line.Split(" ");

            int n = int.Parse(firstLine[0]);
            int m = int.Parse(firstLine[1]);
            for (int i = 0; i < n; i++)
            {
                line = sr.ReadLine();
                fortunes.Add(int.Parse(line));
            }
            for (int i = 0; i < m; i++)
            {
                line = sr.ReadLine();
                string[] sRange = line.Split(" ");
                ranges.Add(new Range(int.Parse(sRange[0]), int.Parse(sRange[1])));
            }
            sr.Close();
            Lazy(ranges, fortunes);
            //fortunes = Fast(ranges, fortunes);
            int k = 0;
            /*foreach (int fortune in fortunes)
            {
                Console.WriteLine(k+": "+fortune);
                k++;
            }
            Console.WriteLine("-----------------------");
            foreach (Range range in ranges)
            {
                Console.WriteLine(range.count);
            }*/
            Console.WriteLine(code);
        }
        static List<int> MergeSort(List<int> list)
        {
            if (list.Count > 1)
            {
                List<int> leftList;
                List<int> rightList;
                leftList = list.Take(list.Count / 2).ToList();
                rightList = list.Skip(list.Count / 2).ToList();
                leftList = MergeSort(leftList);
                rightList = MergeSort(rightList);
                list = Merge(leftList, rightList);
            }
            return list;
        }
        static List<int> Merge(List<int> leftList, List<int> rightList)
        {
            int r = 0;
            int l = 0;
            List<int> merged = new List<int>();

            while (l < leftList.Count && r < rightList.Count)
            {
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
        static void Lazy(List<Range> ranges, List<int> fortunes)
        {
            foreach (int fortune in fortunes)
            {
                foreach (Range range in ranges)
                {
                    code++;
                    if (fortune >= range.start && fortune <= range.end) range.count++;
                }
            }
        }
        static List<int> Fast(List<Range> ranges, List<int> fortunes)
        {
            fortunes = MergeSort(fortunes);
            foreach (Range range in ranges)
            {
                range.count = Top(fortunes, range.end) - Bot(fortunes, range.start) + 1;
            }
            return fortunes;
        }
        static int Bot(List<int> fortunes, int value)
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
        static int Top(List<int> fortunes, int value)
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
        public int start;
        public int end;
        public int count;
        public Range(int start, int end)
        {
            this.start = start;
            this.end = end;
            count = 0;
        }
    }
}