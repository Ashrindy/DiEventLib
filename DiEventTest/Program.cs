using DiEventLib;
using System.ComponentModel;
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

            DvNode mainNode = new();
            DvNodeElement element = new();
            DvElementNearFarSetting setting = new();

            setting.Field_00 = 0;
            setting.Near = 0.01f;
            setting.Far = 100000f;
            setting.Field_10 = new uint[5];
            foreach (var item in setting.Field_10)
            {
                setting.Field_10[item] = 0;
            }

            element.ElementID = DvElementID.NearFarSetting;
            element.Start = 0f;
            element.End = diEvent.Common.End;
            element.Version = 0;
            element.Flags = 0;
            element.PlayType = ElementPlayType.Normal;
            element.UpdateTiming = ElementUpdateTiming.OnUpdatePos;
            element.Element = setting;

            mainNode.Guid = Guid.Parse("{2E6836D6-E7D8-4131-88E6-299B40781D9A}");
            mainNode.Category = DvNodeCategory.Element;
            mainNode.NodeSize = 292;
            mainNode.ChildCount = 0;
            mainNode.Flags = 0;
            mainNode.Priority = 0;
            mainNode.Name = "NearFarSettingTest";
            mainNode.NodeObject = element;

            diEvent.Common.Node.ChildNodes.Insert(diEvent.Common.Node.ChildNodes.Count - 1, mainNode);

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