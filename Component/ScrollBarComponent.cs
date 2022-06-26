using ULD.Node;

namespace ULD.Component; 

public class ScrollBarComponent : ComponentBase {
    public ResNode?[] UnknownNodes = new ResNode?[4];
    private readonly uint[] unknownNodeIds = new uint[4];
    
    public override long GetSize(string version) => base.GetSize(version) + 4 + UnknownNodes.Length * 4;
    
    public ushort Margin;
    public bool IsVertical;
    public sbyte Padding;
    
    protected override void DecodeData(ULD baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        for (var i = 0; i < unknownNodeIds.Length; i++) unknownNodeIds[i] = br.ReadUInt32();
        Margin = br.ReadUInt16();
        IsVertical = br.ReadBoolean();
        Padding = br.ReadSByte();
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
        writer.Write(IsVertical);
        writer.Write(Padding);
    }
}
