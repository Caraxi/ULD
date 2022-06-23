using ULD.Node;

namespace ULD;

public class Widget : IVersionedEncodable {

    public uint Id;
    public AlignmentType AlignmentType;
    public short X;
    public short Y;
    public ushort NodeCount;
    public long GetSize(string version) => 16 + Nodes.Sum(n => n.Size);
    public List<ResNode> Nodes = new();


    public byte[] Encode(string version) {
        var b = new BufferWriter();
        b.Write(Id);
        b.Write((int)AlignmentType);
        b.Write(X);
        b.Write(Y);
        b.Write((ushort) Nodes.Count);

        var nodeData = new BufferWriter();
        foreach (var node in Nodes) {
            nodeData.Write(node.Encode());
        }
        
        if (nodeData.Length + 16 > ushort.MaxValue) throw new Exception("Widget is too big");
        
        b.Write((ushort)GetSize(version));
        b.Write(nodeData);
        
        return b.ToArray();
    }

    public void Decode(ULD baseUld, BufferReader reader, string version) {
        Id = reader.ReadUInt32();
        AlignmentType = (AlignmentType)reader.ReadInt32();
        X = reader.ReadInt16();
        Y = reader.ReadInt16();
        var nodeCount = reader.ReadUInt16();
        var size = reader.ReadUInt16();
        Nodes = new List<ResNode>();

        for (var i = 0; i < nodeCount; i++) {
            var node = ResNode.ReadNode(baseUld, reader);
            Nodes.Add(node);
        }

        var vSize = GetSize(version);
        if (vSize != size) throw new Exception("Widget size mismatch. Expected " + size + " but got " + vSize);
    }
}
