using DiEventLib;
using System.Text;

namespace DiEventTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Console.OutputEncoding = Encoding.GetEncoding("Shift-JIS");
            string filepath;

            Console.WriteLine("What's the .dvscene?");
            filepath = Console.ReadLine();

            DvScene diEvent = new(filepath);

            Console.WriteLine("Loaded");
        }
    }
}