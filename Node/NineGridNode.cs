namespace ULD.Node; 

public enum GridPartsType : byte
{
    Divide = 0x0,
    Compose = 0x1,
}

public enum GridRenderType : byte
{
    Scale = 0x0,
    Tile = 0x1,
}

public class NineGridNode : ResNode {

    public uint PartListId;
    public uint PartId;
    public GridPartsType GridPartsType;
    public GridRenderType GridRenderType;
    public short TopOffset;
    public short BottomOffset;
    public short LeftOffset;
    public short RightOffset;
    public byte Unk3;
    public byte Unk4;

    public override long Size => base.Size + 20;

    public override NodeType Type => NodeType.NineGrid;

    public override byte[] Encode() {
        var b = new BufferWriter();
        b.Write(base.Encode());
        b.Write(PartListId);
        b.Write(PartId);
        b.Write((byte)GridPartsType);
        b.Write((byte)GridRenderType);
        b.Write(TopOffset);
        b.Write(BottomOffset);
        b.Write(LeftOffset);
        b.Write(RightOffset);
        b.Write(Unk3);
        b.Write(Unk4);
        return b;
    }

    public override void Decode(Uld baseUld, BufferReader reader) {
        base.Decode(baseUld, reader);
        PartListId = reader.ReadUInt32();
        PartId = reader.ReadUInt32();
        GridPartsType = (GridPartsType)reader.ReadByte();
        GridRenderType = (GridRenderType)reader.ReadByte();
        TopOffset = reader.ReadInt16();
        BottomOffset = reader.ReadInt16();
        LeftOffset = reader.ReadInt16();
        RightOffset = reader.ReadInt16();
        Unk3 = reader.ReadByte();
        Unk4 = reader.ReadByte();
    }
}
