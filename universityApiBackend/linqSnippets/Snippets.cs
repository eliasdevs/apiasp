namespace universityApiBackend.linqSnippets
{
    public class Snippets
    {
        //paging with skip & take
        static public IEnumerable<T> GetPage<T>(IEnumerable<T> collection , int pageNumber, int resultsPerPage)
        {
            int startIndex = (pageNumber - 1) * resultsPerPage;
            return collection.Skip(startIndex).Take(resultsPerPage);    
        }
        static public void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var aboveAverage = from number in numbers
                               let average = numbers.Average()
                               let nScuared = Math.Pow(number, 2)
                               where nScuared > average
                               select number;
            Console.WriteLine("Average: {0}", numbers.Average());
            foreach (int number in aboveAverage)
            {
                Console.WriteLine(number);              

            }
        }
    }
}
