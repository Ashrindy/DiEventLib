using Amicitia.IO.Binary;

namespace DiEventLib;

public static class DvNodeWriter
{
    public static void WriteNode(this DvNode node, BinaryObjectWriter writer)
    {
        writer.Write(node.Guid);
        writer.Write(node.Category);
        var nodeSizePos = writer.Position;
        writer.WriteNulls(4);
        writer.Write(node.Children.Count);
        writer.Write(node.NodeFlags);
        writer.Write(node.Priority);
        writer.WriteNulls(12);
        writer.WriteDvString(node.NodeName, Utils.StringEncoding.ShiftJIS);

        long preWritePos = writer.Position;

        //node.WriteNode()
        
        switch (node.Category)
        {
            case DvNodeCategory.DummyNode: break;
            case DvNodeCategory.Path:
                var path = node as DvNodePath;
                path.Write(writer);
                break;
            case DvNodeCategory.Camera:
                var camera = node as DvNodeCamera;
                camera.Write(writer);
                break;
            case DvNodeCategory.CameraMotion:
                var cameraMotion = node as DvNodeCameraMotion;
                cameraMotion.Write(writer);
                break;
            case DvNodeCategory.Element:
                var element = node as DvNodeElement;
                element.Write(writer);

                switch (element.ElementID)
                { 
                    case DvElementID.GameCamera:
                        var gameCamera = element as DvElementGameCamera;
                        gameCamera.Write(writer);
                        break;
                    case DvElementID.Caption:
                        var caption = element as DvElementCaption;
                        caption.Write(writer);
                        break;
                    case DvElementID.LetterBox:
                        var letterBox = element as DvElementLetterBox;
                        letterBox.Write(writer);
                        break;
                    case DvElementID.Fade:
                        var fade = element as DvElementFade;
                        fade.Write(writer);
                        break;
                }
                break;

        }
        
        long postWritePos = writer.Position;

        writer.Seek(nodeSizePos, SeekOrigin.Begin);
        writer.Write((int)(postWritePos - preWritePos) / 4);
        writer.Seek(postWritePos, SeekOrigin.Begin);

        foreach (var child in node.Children)
        {
            child.WriteNode(writer);
        }

    }
}
