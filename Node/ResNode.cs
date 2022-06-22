using ULD.Node.Component;

namespace ULD.Node; 

public enum NodeType
{
    Res = 1,
    Image = 2,
    Text = 3,
    NineGrid = 4,
    Counter = 5,

    Collision = 8
    // Component: >=1000
}

public class ResNode : IEncodable {
    public uint NodeId;
    public int ParentId;
    public int NextSiblingId;
    public int PrevSiblingId;
    public int ChildNodeId;

    public NodeType Type;
    public short TabIndex;
    public int[] Unk1 = new int[4];
    public short X;
    public short Y;
    public ushort W;
    public ushort H;
    public float Rotation;
    public float ScaleX;
    public float ScaleY;
    public short OriginX;
    public short OriginY;
    public ushort Priority;
    
    public bool Visible;
    public bool Enabled;
    public bool Clip;
    public bool Fill;
    public bool AnchorTop;
    public bool AnchorBottom;
    public bool AnchorLeft;
    public bool AnchorRight;

    public byte Unk2;

    public short MultiplyRed;
    public short MultiplyGreen;
    public short MultiplyBlue;
    public short AddRed;
    public short AddGreen;
    public short AddBlue;
    public byte Alpha;
    public byte ClipCount;

    public ushort TimelineId;

    public virtual long Size => 88;
    
    public virtual byte[] Encode() {
        var b = new BufferWriter();
        b.Write(NodeId);
        b.Write(ParentId);
        b.Write(NextSiblingId);
        b.Write(PrevSiblingId);
        b.Write(ChildNodeId);
        b.Write((int)Type);
        if (Size > ushort.MaxValue) throw new Exception("Node is too large to encode");
        b.Write((ushort)Size);
        b.Write(TabIndex);
        b.Write(Unk1);
        b.Write(X);
        b.Write(Y);
        b.Write(W);
        b.Write(H);
        b.Write(Rotation);
        b.Write(ScaleX);
        b.Write(ScaleY);
        b.Write(OriginX);
        b.Write(OriginY);
        b.Write(Priority);

        var field = (byte)0;
        if (Visible) field |= 0x80;
        if (Enabled) field |= 0x40;
        if (Clip) field |= 0x20;
        if (Fill) field |= 0x10;
        if (AnchorTop) field |= 0x08;
        if (AnchorBottom) field |= 0x04;
        if (AnchorLeft) field |= 0x02;
        if (AnchorRight) field |= 0x01;
        b.Write(field);
        
        b.Write(Unk2);
        b.Write(MultiplyRed);
        b.Write(MultiplyGreen);
        b.Write(MultiplyBlue);
        b.Write(AddRed);
        b.Write(AddGreen);
        b.Write(AddBlue);
        b.Write(Alpha);
        b.Write(ClipCount);
        b.Write(TimelineId);
        return b;
    }

    public virtual void Decode(ULD baseUld, BufferReader reader) {
        Logging.IndentLog($"Decoding {GetType()}");
        NodeId = reader.ReadUInt32();
        ParentId = reader.ReadInt32();
        NextSiblingId = reader.ReadInt32();
        PrevSiblingId = reader.ReadInt32();
        ChildNodeId = reader.ReadInt32();
        Type = (NodeType) reader.ReadInt32();
        reader.ReadUInt16(); // Size
        TabIndex = reader.ReadInt16();
        Unk1 = new[] {
            reader.ReadInt32(), 
            reader.ReadInt32(), 
            reader.ReadInt32(), 
            reader.ReadInt32()
        };
        X = reader.ReadInt16();
        Y = reader.ReadInt16();
        W = reader.ReadUInt16();
        H = reader.ReadUInt16();
        Rotation = reader.ReadSingle();
        ScaleX = reader.ReadSingle();
        ScaleY = reader.ReadSingle();
        OriginX = reader.ReadInt16();
        OriginY = reader.ReadInt16();
        Priority = reader.ReadUInt16();
        var field1 = reader.ReadByte();
        Visible = ( field1 & 0x80 ) == 0x80;
        Enabled = ( field1 & 0x40 ) == 0x40;
        Clip = ( field1 & 0x20 ) == 0x20;
        Fill = ( field1 & 0x10 ) == 0x10;
        AnchorTop = ( field1 & 0x08 ) == 0x08;
        AnchorBottom = ( field1 & 0x04 ) == 0x04;
        AnchorLeft = ( field1 & 0x02 ) == 0x02;
        AnchorRight = ( field1 & 0x01 ) == 0x01;
        Unk2 = reader.ReadByte();
        MultiplyRed = reader.ReadInt16();
        MultiplyGreen = reader.ReadInt16();
        MultiplyBlue = reader.ReadInt16();
        AddRed = reader.ReadInt16();
        AddGreen = reader.ReadInt16();
        AddBlue = reader.ReadInt16();
        Alpha = reader.ReadByte();
        ClipCount = reader.ReadByte();
        TimelineId = reader.ReadUInt16();
        Logging.Unindent();
    }

    public static ResNode ReadNode(ULD baseUld, BufferReader reader) {
        
        var pos = reader.BaseStream.Position;
        
        // peek type
        var nodeType = (NodeType) reader.PeekInt32(20);
        var size = reader.PeekUInt16(24);
        
        Logging.IndentLog($"Reading {nodeType} Node @ {reader.BaseStream.Position}");
        
        var node = nodeType switch {
            NodeType.Res => new ResNode(),
            NodeType.Text => new TextNode(),
            NodeType.Image => new ImageNode(),
            NodeType.Collision => new CollisionNode(),
            NodeType.NineGrid => new NineGridNode(),
            NodeType.Counter => new CounterNode(),
            > (NodeType)1000 => BaseComponentNode.Create(baseUld, (uint) nodeType),
            _ => new ResNode()
        };
        
        node.Decode(baseUld, reader);

        if (size != node.Size) {
            throw new Exception($"{node.GetType().Name} does not match the expected size for a {node.Type}. Expected {node.Size} but got {size}");
        }

        if (size > 0) {
            reader.Seek(pos + size);
        }
        Logging.Unindent();
        
        return node;
    }
    
    
}