namespace ULD.Node.Component;

public class TextNineGridComponentNode : BaseComponentNode {
    public uint TextId;

    public override long Size => base.Size + 4;

    public override void Decode(ULD baseUld, BufferReader reader) {
        base.Decode(baseUld, reader);
        TextId = reader.ReadUInt32();
    }
    
    public override byte[] Encode() {
        var b = new BufferWriter();
        b.Write(base.Encode());
        b.Write(TextId);
        return b;
    }
}
