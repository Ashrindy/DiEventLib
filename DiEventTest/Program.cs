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

           // Reader reader = new Reader();
            //Writer writer = new Writer();

            DvScene diEvent = new(filepath);
            //writer.WriteDvScene(filepath, diEvent);


            Console.WriteLine("");
            Console.WriteLine("Common:");


            
        }
    }
}