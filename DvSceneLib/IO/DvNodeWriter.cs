using Amicitia.IO.Binary;
using DvSceneLib.IO.Template;
using System.Reflection.PortableExecutable;
using System;

namespace DvSceneLib;

public static class DvNodeWriter
{
    public static void WriteNode(this DvNode node, BinaryObjectWriter writer)
    {
        writer.Write(node.Guid);
        writer.Write(node.Category);
        var nodeSizePos = writer.Position;
        writer.WriteNulls(4);
        writer.Write(node.ChildNodes.Count);
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
            case DvNodeCategory.Character:
                var character = node as DvNodeCharacter;
                character.Write(writer);
                break;
            case DvNodeCategory.CharacterMotion: 
                var charMot = node as DvNodeCharacterMotion;
                charMot.Write(writer);
                break;
            case DvNodeCategory.Model: 
                var model = node as DvNodeModel;
                model.Write(writer);
                break;
            case DvNodeCategory.ModelMotion: 
                var modelMot = node as DvNodeModelMotion;
                modelMot.Write(writer);
                break;
            case DvNodeCategory.ModelNode: 
                var modelNode = node as DvNodeModelNode;
                modelNode.Write(writer);
                break;
            case DvNodeCategory.Element:
                var element = node as DvNodeElement;
                element.Write(writer);
                break;
            //case DvNodeCategory.Stage: break;
            //case DvNodeCategory.FolderCondition: break;
            default:
                throw new NotSupportedException($"Not implemented category: {node.Category.ToString()}");
        }

        long postWritePos = writer.Position;

        writer.Seek(nodeSizePos, SeekOrigin.Begin);
        writer.Write((int)(postWritePos - preWritePos) / 4);
        writer.Seek(postWritePos, SeekOrigin.Begin);

        foreach (var child in node.ChildNodes)
            child.WriteNode(writer);
    }

    public static void WriteNode(this DvNodeTemplate node, BinaryObjectWriter writer, DiEventDataBase db) 
    {
        DiEventDataBase.Node dbNode = db.Nodes.Find(x => x.FullName == node.Category);
        writer.Write(node.Guid);
        writer.Write(dbNode.NodeCategory);
        var nodeSizePos = writer.Position;
        writer.WriteNulls(4);
        writer.Write(node.ChildNodes.Count);
        writer.Write(node.NodeFlags);
        writer.Write(node.Priority);
        writer.WriteNulls(12);
        writer.WriteDvString(node.NodeName, Utils.StringEncoding.ShiftJIS);

        long preWritePos = writer.Position;

        if (dbNode == null)
        {
            Console.WriteLine($"Not implemented category: {node.Category} (Name: {node.NodeName}, GUID: {node.Guid})");
        }
        else
        {
            if (dbNode.Descriptions.ContainsKey("Unknown"))
                Console.WriteLine($"Not implemented category: {node.Category} (Name: {node.NodeName}, GUID: {node.Guid})");
            else
            {
                node.Category = dbNode.FullName;
                if (dbNode.Name == "Element")
                {
                    node = (DvElementTemplate)node;
                    node.Write(writer, dbNode);
                    DiEventDataBase.Node dbElem = db.Elements.Find(x => x.NodeCategory == (int)node.Fields["Element ID"].Value);
                    if (dbElem == null)
                        Console.WriteLine($"Not implemented element: {((int)node.Fields["Element ID"].Value).ToString()} (Name: {node.NodeName}, GUID: {node.Guid}). SKIPPING");
                    else
                    {
                        ((DvElementTemplate)node).ElementName = dbElem.FullName;
                        ((DvElementTemplate)node).WriteElement(writer, dbElem);
                    }
                }
                else
                    node.Write(writer, dbNode);
            }
        }

        long postWritePos = writer.Position;

        writer.Seek(nodeSizePos, SeekOrigin.Begin);
        writer.Write((int)(postWritePos - preWritePos) / 4);
        writer.Seek(postWritePos, SeekOrigin.Begin);

        foreach (var child in node.ChildNodes)
            ((DvNodeTemplate)child).WriteNode(writer, db);
    }
}
