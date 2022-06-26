using ULD.Node;

namespace ULD;

public class Widget : ListElement {
    public AlignmentType AlignmentType;
    public short X;
    public short Y;
    public override long GetSize(string version) => 16 + ResNode.Collapse(RootNode).Sum(n => n.Size);

    public ResNode? RootNode;

    public override byte[] Encode(string version) {
        var b = new BufferWriter();
        b.Write(Id);
        b.Write((int)AlignmentType);
        b.Write(X);
        b.Write(Y);

        var nodeList = ResNode.Collapse(RootNode);
        
        if (nodeList.Count > ushort.MaxValue) throw new Exception("Too many nodes");
        
        b.Write((ushort) nodeList.Count);

        var nodeData = new BufferWriter();
        foreach (var node in nodeList) {
            nodeData.Write(node.Encode());
        }
        
        if (nodeData.Length + 16 > ushort.MaxValue) throw new Exception("Widget is too big");
        
        b.Write((ushort)GetSize(version));
        b.Write(nodeData);
        
        return b.ToArray();
    }

    public override void Decode(ULD baseUld, BufferReader reader, string version) {
        Id = reader.ReadUInt32();
        AlignmentType = (AlignmentType)reader.ReadInt32();
        X = reader.ReadInt16();
        Y = reader.ReadInt16();
        var nodeCount = reader.ReadUInt16();
        var size = reader.ReadUInt16();
        var nodeList = new List<ResNode>();
        for (var i = 0; i < nodeCount; i++) {
            var node = ResNode.ReadNode(baseUld, reader);
            nodeList.Add(node);
        }
        
        if (nodeList.Count(n => n.IsRootNode) != 1) throw new Exception("Widget must have exactly one root node");
        

        // Nodes = nodeList;
        RootNode = ResNode.Expand(nodeList);
        
        var vSize = GetSize(version);
        if (vSize != size) throw new Exception("Widget size mismatch. Expected " + size + " but got " + vSize);
    }
}
