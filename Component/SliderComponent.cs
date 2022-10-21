using ULD.Node;

namespace ULD.Component;

public class SliderComponent : ComponentBase {
    public ResNode?[] UnknownNodes = new ResNode?[4];
    private readonly uint[] unknownNodeIds = new uint[4];
    
    public override long GetSize(string version) => base.GetSize(version) + 4 + UnknownNodes.Length * 4;

    public bool IsVertical;
    public byte LeftOffset;
    public byte RightOffset;
    public sbyte Padding;
    
    protected override void DecodeData(Uld baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        for (var i = 0; i < unknownNodeIds.Length; i++) unknownNodeIds[i] = br.ReadUInt32();
        IsVertical = br.ReadBoolean();
        LeftOffset = br.ReadByte();
        RightOffset = br.ReadByte();
        Padding = br.ReadSByte();
    }
    
    protected override void DecodeNodeList(Uld baseUld, BufferReader reader, string version, List<ResNode> nodes) {
        for (var i = 0; i < UnknownNodes.Length; i++) {
            UnknownNodes[i] = unknownNodeIds[i] == 0 ? null : nodes.Find(n => n.Id == unknownNodeIds[i]);
        }
    }    

    protected override void EncodeData(BufferWriter writer) {
        base.EncodeData(writer);
        foreach (var n in UnknownNodes) writer.Write(n?.Id ?? 0);
        writer.Write(IsVertical);
        writer.Write(LeftOffset);  
        writer.Write(RightOffset);
        writer.Write(Padding);
    }
    
}