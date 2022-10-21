namespace ULD.Node;

public class CounterNode : ResNode {
    public uint PartListId;
    public byte PartId;
    public byte NumberWidth;
    public byte CommaWidth;
    public byte SpaceWidth;
    public ushort Alignment;
    public ushort Unk1;
    
    public override long Size => base.Size + 12;

    public override NodeType Type => NodeType.Counter;

    public override byte[] Encode() {
        var b = new BufferWriter();
        b.Write(base.Encode());
        b.Write(PartListId);
        b.Write(PartId);
        b.Write(NumberWidth);
        b.Write(CommaWidth);
        b.Write(SpaceWidth);
        b.Write(Alignment);
        b.Write(Unk1);
        return b;
    }

    public override void Decode(Uld baseUld, BufferReader reader) {
        base.Decode(baseUld, reader);
        PartListId = reader.ReadUInt32();
        PartId = reader.ReadByte();
        NumberWidth = reader.ReadByte();
        CommaWidth = reader.ReadByte();
        SpaceWidth = reader.ReadByte();
        Alignment = reader.ReadUInt16();
        Unk1 = reader.ReadUInt16();
    }
    
    
    
}
