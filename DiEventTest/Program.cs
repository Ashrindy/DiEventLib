using DiEventLib;
using DiEventLib.IO.Template;
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

            DiEventDataBase dievtdb = new();
            dievtdb.Open("rangers.json");
            dievtdb.SaveBinary("rangers.dievtdb");
            DvScene scene = new();
            scene.Open(filepath, dievtdb);
            scene.Save(filepath + ".dvscene", dievtdb);

            Console.WriteLine("Loaded");
        }
    }
}