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
            diEvent.Write(filepath);


            Console.WriteLine("");
            Console.WriteLine("Resource:");

            foreach(var i in diEvent.Resource.Entries) 
            { 
                Console.WriteLine(i.Name); 
                Console.WriteLine(i.Guid); 
                Console.WriteLine(i.Type); 
            } 
        }
    }
}