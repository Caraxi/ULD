using ULD.Node;

namespace ULD.Component;

public class NumericInputComponent : ComponentBase {
    public ResNode?[] UnknownNodes = new ResNode?[5];
    private readonly uint[] unknownNodeIds = new uint[5];
    
    public override long GetSize(string version) => base.GetSize(version) + 4 + UnknownNodes.Length * 4;

    public uint Color;


    protected override void DecodeData(Uld baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        for (var i = 0; i < unknownNodeIds.Length; i++) unknownNodeIds[i] = br.ReadUInt32();
        Color = br.ReadUInt32();
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
    }

    
}