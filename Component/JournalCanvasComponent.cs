using ULD.Node;

namespace ULD.Component;

public class JournalCanvasComponent : ComponentBase {
    public ResNode?[] UnknownNodes = new ResNode?[32];
    private readonly uint[] unknownNodeIds = new uint[32];
    
    public override long GetSize(string version) => base.GetSize(version) + 8 + UnknownNodes.Length * 4;

    public ushort Margin;
    public ushort Unk1;
    public ushort Unk2;
    public ushort Padding;

    protected override void DecodeData(ULD baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        for (var i = 0; i < unknownNodeIds.Length; i++) unknownNodeIds[i] = br.ReadUInt32();
        Margin = br.ReadUInt16();
        Unk1 = br.ReadUInt16();
        Unk2 = br.ReadUInt16();
        Padding = br.ReadUInt16();
    }

    protected override void DecodeNodeList(ULD baseUld, BufferReader reader, string version, List<ResNode> nodes) {
        for (var i = 0; i < UnknownNodes.Length; i++) {
            UnknownNodes[i] = unknownNodeIds[i] == 0 ? null : nodes.Find(n => n.Id == unknownNodeIds[i]);
        }
    }
    
    protected override void EncodeData(BufferWriter writer) {
        base.EncodeData(writer);
        foreach (var n in UnknownNodes) writer.Write(n?.Id ?? 0);
        writer.Write(Margin);
        writer.Write(Unk1);
        writer.Write(Unk2);
        writer.Write(Padding);
    }
    
    
}