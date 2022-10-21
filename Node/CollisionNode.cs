namespace ULD.Node; 

public enum CollisionType : ushort
{
    Hit = 0x0,
    Focus = 0x1,
    Move = 0x2,
}

public class CollisionNode : ResNode {

    public CollisionType CollisionType;
    public ushort Unk3;
    public int CollisionX;
    public int CollisionY;
    public uint Radius;

    public override long Size => base.Size + 16;

    public override NodeType Type => NodeType.Collision;

    public override byte[] Encode() {
        var b = new BufferWriter();
        b.Write(base.Encode());
        b.Write((ushort) CollisionType);
        b.Write(Unk3);
        b.Write(CollisionX);
        b.Write(CollisionY);
        b.Write(Radius);
        return b;
    }

    public override void Decode(Uld baseUld, BufferReader reader) {
        base.Decode(baseUld, reader);
        CollisionType = (CollisionType)reader.ReadUInt16();
        Unk3 = reader.ReadUInt16();
        CollisionX = reader.ReadInt32();
        CollisionY = reader.ReadInt32();
        Radius = reader.ReadUInt32();
    }
}
