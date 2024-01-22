using DiEventLib;
using DiEventLib.Nodes;
using DiEventLib.Nodes.Elements;
using DiEventLib.Nodes.NodeTypes;
using System.Text;

namespace DiEventTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            string filepath;

            Console.WriteLine("What's the .dvscene?");
            filepath = Console.ReadLine();

            Reader reader = new Reader();
            Writer writer = new Writer();

            DiEvent diEvent = reader.ReadDvScene(filepath);
            writer.WriteDvScene(filepath, diEvent);


            Console.WriteLine("");
            Console.WriteLine("Common:");


            foreach (var i in diEvent.common.nodes)
            {
               Console.WriteLine("GUID: " + i.guid);
               if (i.category == nodeCategory.element)
               {
                    if(i.GetInfoByType<DvElement>().elementInfo.elementID == elementID.sun)
                    {
                        Console.WriteLine("Type: " + i.category + " - " + i.GetInfoByType<DvElement>().GetElementInfoByType<DvElementSun>().sun.field_00);
                    } else
                    {
                        Console.WriteLine("Type: " + i.category + " - " + i.GetInfoByType<DvElement>().elementInfo.elementID);
                    }
               }
               else
               {
                  Console.WriteLine("Type: " + i.category);
               }
               Console.WriteLine("Name: " + i.name);
               Console.WriteLine("");
            }
        }
    }
}