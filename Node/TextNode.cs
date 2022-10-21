namespace ULD.Node;

public enum FontType : byte {
    Axis = 0x0,
    MiedingerMed = 0x1,
    Miedinger = 0x2,
    TrumpGothic = 0x3,
    Jupiter = 0x4,
    JupiterLarge = 0x5,
}

public enum TextAlignment : ushort {
    TopLeft,    TopMiddle,    TopRight,
    MiddleLeft, Center,       MiddleRight,
    BottomLeft, BottomMiddle, BottomRight,
}

public enum SheetType : byte {
    Addon = 0x0,
    Lobby = 0x1,
}

public class TextNode : ResNode {
    public uint TextId;
    public uint Color;
    public TextAlignment Alignment;
    public FontType Font;
    public byte FontSize;
    public uint EdgeColor;
    
    public bool Bold;
    public bool Italic;
    public bool Edge;
    public bool Glare;
    public bool Multiline;
    public bool Ellipsis;
    public bool Paragraph;
    public bool Emboss;

    public SheetType SheetType;
    public byte CharSpacing;
    public byte LineSpacing;

    public uint Unk2;

    public override long Size => base.Size + 24;

    public override NodeType Type => NodeType.Text;

    public override byte[] Encode() {
        var b = new BufferWriter();
        b.Write(base.Encode());
        b.Write(TextId);
        b.Write(Color);
        b.Write((ushort)Alignment);
        b.Write((byte) Font);
        b.Write(FontSize);
        b.Write(EdgeColor);

        byte f = 0;
        if (Bold) f |= 0x80;
        if (Italic) f |= 0x40;
        if (Edge) f |= 0x20;
        if (Glare) f |= 0x10;
        if (Multiline) f |= 0x08;
        if (Ellipsis) f |= 0x04;
        if (Paragraph) f |= 0x02;
        if (Emboss) f |= 0x01;
        b.Write(f);
        
        b.Write((byte) SheetType);
        b.Write(CharSpacing);
        b.Write(LineSpacing);
        b.Write(Unk2);
        
        return b;
    }

    public override void Decode(Uld baseUld, BufferReader reader) {
        base.Decode(baseUld, reader);
        TextId = reader.ReadUInt32();
        Color = reader.ReadUInt32();
        Alignment = (TextAlignment)reader.ReadUInt16();
        Font = (FontType)reader.ReadByte();
        FontSize = reader.ReadByte();
        EdgeColor = reader.ReadUInt32();

        var f = reader.ReadByte();
        Bold = (f & 0x80) == 0x80;
        Italic = (f & 0x40) == 0x40;
        Edge = (f & 0x20) == 0x20;
        Glare = (f & 0x10) == 0x10;
        Multiline = (f & 0x08) == 0x08;
        Ellipsis = (f & 0x04) == 0x04;
        Paragraph = (f & 0x02) == 0x02;
        Emboss = (f & 0x01) == 0x01;
        
        SheetType = (SheetType)reader.ReadByte();
        CharSpacing = reader.ReadByte();
        LineSpacing = reader.ReadByte();
        Unk2 = reader.ReadUInt32();
    }
}
