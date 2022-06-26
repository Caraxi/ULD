using ULD.Node;

namespace ULD.Component;

public class PreviewComponent : ComponentBase {
    public ResNode?[] UnknownNodes = new ResNode?[2];
    private readonly uint[] unknownNodeIds = new uint[2];
    
    public override long GetSize(string version) => base.GetSize(version) + UnknownNodes.Length * 4;
    
    protected override void DecodeData(ULD baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);

        for (var i = 0; i < unknownNodeIds.Length; i++) {
            unknownNodeIds[i] = br.ReadUInt32();
        }
    }
    
    protected override void DecodeNodeList(ULD baseUld, BufferReader reader, string version, List<ResNode> nodes) {
        for (var i = 0; i < UnknownNodes.Length; i++) {
            UnknownNodes[i] = unknownNodeIds[i] == 0 ? null : nodes.Find(n => n.Id == unknownNodeIds[i]);
        }
    }    
    
    protected override void EncodeData(BufferWriter writer) {
        base.EncodeData(writer);
        foreach (var n in UnknownNodes) writer.Write(n?.Id ?? 0);
    }
}