using ULD.Node;

namespace ULD.Component;

public class ListComponent : ComponentBase {

    public byte Wrap;
    public byte Orientation;
    public byte[] Padding = new byte[2];
    
    public ResNode?[] UnknownNodes = new ResNode?[5];
    private readonly uint[] unknownNodeIds = new uint[5];
    
    public override long GetSize(string version) => base.GetSize(version) + 4 + UnknownNodes.Length * 4;

    protected override void DecodeData(Uld baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        for (var i = 0; i < unknownNodeIds.Length; i++) unknownNodeIds[i] = br.ReadUInt32();
        Wrap = br.ReadByte();
        Orientation = br.ReadByte();
        Padding = br.ReadBytes(2);
    }

    protected override void DecodeNodeList(Uld baseUld, BufferReader reader, string version, List<ResNode> nodes) {
        for (var i = 0; i < UnknownNodes.Length; i++) {
            UnknownNodes[i] = unknownNodeIds[i] == 0 ? null : nodes.Find(n => n.Id == unknownNodeIds[i]);
        }
    }    
    
    protected override void EncodeData(BufferWriter writer) {
        base.EncodeData(writer);
        foreach (var n in UnknownNodes) writer.Write(n?.Id ?? 0);
        writer.Write(Wrap);
        writer.Write(Orientation);
        writer.Write(Padding);
    }
    
    
    
}