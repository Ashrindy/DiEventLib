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

            Console.Clear();
            Console.WriteLine("What would you like to do with it?");
            Console.WriteLine("1. Add in a NearFarSetting");
            Console.WriteLine("2. Add in subtitles to a real-time version of a cutscene from its prerendered counterpart");

            string option = Console.ReadLine();

            switch(option)
            {
                case "1":
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

                    mainNode.Guid = new Guid();
                    mainNode.Category = DvNodeCategory.Element;
                    mainNode.NodeSize = 176;
                    mainNode.ChildCount = 0;
                    mainNode.Flags = 0;
                    mainNode.Priority = 0;
                    mainNode.Name = "NearFarSettingTest";
                    mainNode.NodeObject = element;

                    diEvent.Common.Node.ChildNodes.Insert(diEvent.Common.Node.ChildNodes.Count - 1, mainNode);

                    diEvent.Write(filepath);
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine("What's the prerendered counterpart?");

                    string prerenderedPath = Console.ReadLine();

                    DvScene prerenderedScene = new(prerenderedPath);

                    foreach(var i in prerenderedScene.Common.Node.ChildNodes)
                    {
                        if(i.Category == DvNodeCategory.Element)
                        {
                            if(((DvNodeElement)i.NodeObject).ElementID == DvElementID.Caption || ((DvNodeElement)i.NodeObject).ElementID == DvElementID.LetterBox || ((DvNodeElement)i.NodeObject).ElementID == DvElementID.Fade)
                            {
                                diEvent.Common.Node.ChildNodes.Add(i);
                            }
                        }
                    }

                    diEvent.Write(filepath);
                    break;
            }
        }
    }
}