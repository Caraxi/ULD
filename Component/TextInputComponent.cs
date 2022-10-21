using ULD.Node;

namespace ULD.Component;

public class TextInputComponent : ComponentBase {

    
    public override long GetSize(string version) => base.GetSize(version) + 12 + UnknownNodes.Length*4;

    private readonly uint[] unknownNodeIds = new uint[16];
    public ResNode?[] UnknownNodes = new ResNode?[16];
    public uint Color;
    public uint IMEColor;
    public uint Unknown;
    
    protected override void DecodeData(Uld baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        
        for (var i = 0; i < unknownNodeIds.Length; i++) {
            unknownNodeIds[i] = br.ReadUInt32();
        }
        
        Color = br.ReadUInt32();
        IMEColor = br.ReadUInt32();
        Unknown = br.ReadUInt32();
    }

    protected override void DecodeNodeList(Uld baseUld, BufferReader reader, string version, List<ResNode> nodes) {
        for (var i = 0; i < UnknownNodes.Length; i++) {
            UnknownNodes[i] = unknownNodeIds[i] == 0 ? null : nodes.Find(n => n.Id == unknownNodeIds[i]);
        }
    }    
    
    protected override void EncodeData(BufferWriter writer) {
        base.EncodeData(writer);
        foreach (var n in UnknownNodes) writer.Write(n?.Id ?? 0);
        writer.Write(Color);
        writer.Write(IMEColor);
        writer.Write(Unknown);
    }
    
    
    
}