using ULD.Node;

namespace ULD.Component;

public class GaugeBarComponent : ComponentBase {
    public ResNode?[] UnknownNodes = new ResNode?[6];
    private readonly uint[] unknownNodeIds = new uint[6];
    public override long GetSize(string version) => base.GetSize(version) + 8 + UnknownNodes.Length*4;
    
    public ushort VerticalMargin;
    public ushort HorizontalMargin;
    public bool IsVertical;
    public byte[] Padding = new byte[3];
    
    protected override void DecodeData(ULD baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        
        for (var i = 0; i < unknownNodeIds.Length; i++) {
            unknownNodeIds[i] = br.ReadUInt32();
        }
        
        VerticalMargin = br.ReadUInt16();
        HorizontalMargin = br.ReadUInt16();
        IsVertical = br.ReadBoolean();
        Padding = br.ReadBytes(3);
    }

    protected override void DecodeNodeList(ULD baseUld, BufferReader reader, string version, List<ResNode> nodes) {
        for (var i = 0; i < UnknownNodes.Length; i++) {
            UnknownNodes[i] = unknownNodeIds[i] == 0 ? null : nodes.Find(n => n.Id == unknownNodeIds[i]);
        }
    }    
    
    protected override void EncodeData(BufferWriter writer) {
        base.EncodeData(writer);
        foreach (var n in UnknownNodes) writer.Write(n?.Id ?? 0);
        writer.Write(VerticalMargin);
        writer.Write(HorizontalMargin);
        writer.Write(IsVertical);
        writer.Write(Padding);
    }
    
}