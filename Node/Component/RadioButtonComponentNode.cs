namespace ULD.Node.Component;

public class RadioButtonComponentNode : ButtonComponentNode {
    public uint GroupId;

    public override long Size => base.Size + 4;

    public override void Decode(Uld baseUld, BufferReader reader) {
        base.Decode(baseUld, reader);
        GroupId = reader.ReadUInt32();
    }
    
    public override byte[] Encode() {
        var b = new BufferWriter();
        b.Write(base.Encode());
        b.Write(GroupId);
        return b;
    }
}
