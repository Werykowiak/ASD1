namespace ASD1
{
    internal class Program
    {
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

            foreach(Range range in ranges)
            {
                Console.WriteLine(range.count);
            }
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
            foreach(int fortune in fortunes)
            {
                foreach(Range range in ranges)
                {
                    if(fortune>=range.start&&fortune<=range.end) range.count++;
                }
            }
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