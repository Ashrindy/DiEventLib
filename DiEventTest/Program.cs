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
                    DvElementCompositeAnimation setting = new();
                    Animation animation = new();
                    Animation[] animations = new Animation[16];

                    animation.FileName = "test";
                    animation.Type = AnimationType.SkeletalAnimation;

                    setting.Field_60 = 0;
                    setting.StateName = "Test";
                    setting.Field_6c = 0;
                    int foreachIndex = 0;
                    foreach (var i in animations)
                    {
                        setting.Animations[foreachIndex].FileName = "";
                        setting.Animations[foreachIndex].Type = AnimationType.SkeletalAnimation;
                        foreachIndex++;
                    }
                    setting.Animations[0] = animation;
                    setting.Animations[1] = animation;

                    element.ElementID = DvElementID.CompositeAnimation;
                    element.Start = 0f;
                    element.End = diEvent.Common.End;
                    element.Version = 0;
                    element.Flags = 0;
                    element.PlayType = ElementPlayType.Normal;
                    element.UpdateTiming = ElementUpdateTiming.OnUpdatePos;
                    element.Element = setting;

                    mainNode.Guid = new Guid();
                    mainNode.Category = DvNodeCategory.Element;
                    mainNode.Flags = 0;
                    mainNode.Priority = 0;
                    mainNode.Name = "CompositeAnimation";
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
                            if(((DvNodeElement)i.NodeObject).ElementID == DvElementID.Caption || ((DvNodeElement)i.NodeObject).ElementID == DvElementID.LetterBox || ((DvNodeElement)i.NodeObject).ElementID == DvElementID.Fade || ((DvNodeElement)i.NodeObject).ElementID == DvElementID.OpeningLogo)
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