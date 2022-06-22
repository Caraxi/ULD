namespace ULD.Node.Component;

public class TextInputComponentNode : BaseComponentNode {
    
    public uint MaxWidth;
    public uint MaxLine;
    public uint MaxSByte;
    public uint MaxChar;
    
    public bool Capitalize;
    public bool Mask;
    public bool AutoTranslateEnabled;
    public bool HistoryEnabled;
    public bool IMEEnabled;
    public bool EscapeClears;
    public bool CapsAllowed;
    public bool LowerAllowed;
    
    public bool NumbersAllowed;
    public bool SymbolsAllowed;
    public bool WordWrap;
    public bool Multiline;
    public bool AutoMaxWidth;
    public byte Unk1;
    
    public ushort Charset;
    public char[] CharsetExtras;

    public override long Size => base.Size + 36;

    public override void Decode(ULD baseUld, BufferReader reader) {
        base.Decode(baseUld, reader);

        MaxWidth = reader.ReadUInt32();
        MaxLine = reader.ReadUInt32();
        MaxSByte = reader.ReadUInt32();
        MaxChar = reader.ReadUInt32();

        var field = reader.ReadByte();
        Capitalize = (field & 0x80) == 0x80;
        Mask = (field & 0x40) == 0x40;
        AutoTranslateEnabled = (field & 0x20) == 0x20;
        HistoryEnabled = (field & 0x10) == 0x10;
        IMEEnabled = (field & 0x08) == 0x08;
        EscapeClears = (field & 0x04) == 0x04;
        CapsAllowed = (field & 0x02) == 0x02;
        LowerAllowed = (field & 0x01) == 0x01;
        
        field = reader.ReadByte();
        NumbersAllowed = (field & 0x80) == 0x80;
        SymbolsAllowed = (field & 0x40) == 0x40;
        WordWrap = (field & 0x20) == 0x20;
        Multiline = (field & 0x10) == 0x10;
        AutoMaxWidth = (field & 0x08) == 0x08;
        Unk1 = (byte)(field & 0x07);
        
        Charset = reader.ReadUInt16();
        CharsetExtras = reader.ReadChars(16);
    }

    public override byte[] Encode() {
        var w = new BufferWriter();
        w.Write(base.Encode());
        w.Write(MaxWidth);
        w.Write(MaxLine);
        w.Write(MaxSByte);
        w.Write(MaxChar);
        
        var field = 0x00;
        if (Capitalize) field |= 0x80;
        if (Mask) field |= 0x40;
        if (AutoTranslateEnabled) field |= 0x20;
        if (HistoryEnabled) field |= 0x10;
        if (IMEEnabled) field |= 0x08;
        if (EscapeClears) field |= 0x04;
        if (CapsAllowed) field |= 0x02;
        if (LowerAllowed) field |= 0x01;
        w.Write((byte)field);
        
        field = Unk1 & 0x07;
        if (NumbersAllowed) field |= 0x80;
        if (SymbolsAllowed) field |= 0x40;
        if (WordWrap) field |= 0x20;
        if (Multiline) field |= 0x10;
        if (AutoMaxWidth) field |= 0x08;
        w.Write((byte)field);
        
        w.Write(Charset);
        w.Write(CharsetExtras);
        
        
        return w;
    }
    
    
    
}
