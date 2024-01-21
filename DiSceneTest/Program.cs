using DiEventLib;
using System.Text;

namespace DiEventTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string filepath;

            if (args.Length == 0 )
            {
                Console.WriteLine("What's the .dvscene?");
                filepath = Console.ReadLine();
            } else
            {
                filepath = args[0];
            }

            Reader reader = new Reader();

            DiEvent diEvent = reader.ReadDvScene(filepath);

            Console.WriteLine("");
            Console.WriteLine("Common:");

            Console.WriteLine("Frame Start: " + diEvent.common.frameStart);
            Console.WriteLine("Frame End: " + diEvent.common.frameEnd);
            Console.WriteLine(diEvent.common.drawNodeNum);
            Console.WriteLine(diEvent.common.chainCameraIn);
            Console.WriteLine(diEvent.common.chainCameraOut);
            Console.WriteLine(diEvent.common.type);
            Console.WriteLine(diEvent.common.skipPointTick);
            Console.WriteLine(diEvent.resources.resourceObject.count);

            Console.WriteLine("");
            Console.WriteLine("Nodes:");

            foreach (var i in diEvent.common.nodes)
            {
                Console.WriteLine("GUID: " + i.guid);
                if (i.category == nodeCategory.element)
                {
                    Console.WriteLine("Type: " + i.category + " - " + ((elementProperties)i.info).elementID);
                }
                else
                {
                    Console.WriteLine("Type: " + i.category);
                }
                Console.WriteLine("Name: " + i.name);
                Console.WriteLine("");
            }

            Console.WriteLine("Resources:");

            foreach (var i in diEvent.resources.resources)
            {
                Console.WriteLine("GUID: " + i.guid);
                Console.WriteLine("Type: " + i.resourceType);
                Console.WriteLine("Filename: " + i.filename);
                Console.WriteLine("");
            }
        }
    }
}