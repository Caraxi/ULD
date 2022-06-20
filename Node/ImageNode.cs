namespace ULD.Node; 

public class ImageNode : ResNode {

    public uint PartListId;
    public uint PartId;
    public bool FlipH;
    public bool FlipV;
    public byte Wrap;
    public byte Unk3;

    protected override ushort Size => (ushort)(base.Size + 12);

    public override byte[] Encode() {
        var b = new BufferWriter();
        b.Write(base.Encode());
        b.Write(PartListId);
        b.Write(PartId);
        b.Write(FlipH);
        b.Write(FlipV);
        b.Write(Wrap);
        b.Write(Unk3);
        return b;
    }

    public override void Decode(ULD baseUld, BufferReader reader) {
        base.Decode(baseUld, reader);
        PartListId = reader.ReadUInt32();
        PartId = reader.ReadUInt32();
        FlipH = reader.ReadBoolean();
        FlipV = reader.ReadBoolean();
        Wrap = reader.ReadByte();
        Unk3 = reader.ReadByte();
    }
}
