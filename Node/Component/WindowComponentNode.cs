namespace ULD.Node.Component;

public class WindowComponentNode : BaseComponentNode {
    public uint TitleTextId;
    public uint SubtitleTextId;
    public bool CloseButton;
    public bool ConfigButton;
    public bool HelpButton;
    public bool Header;

    public override long Size => base.Size + 12;

    public override void Decode(ULD baseUld, BufferReader reader) {
        base.Decode(baseUld, reader);
        TitleTextId = reader.ReadUInt32();
        SubtitleTextId = reader.ReadUInt32();
        CloseButton = reader.ReadBoolean();
        ConfigButton = reader.ReadBoolean();
        HelpButton = reader.ReadBoolean();
        Header = reader.ReadBoolean();
    }
    
    public override byte[] Encode() {
        var b = new BufferWriter();
        b.Write(base.Encode());
        b.Write(TitleTextId);
        b.Write(SubtitleTextId);
        b.Write(CloseButton);
        b.Write(ConfigButton);
        b.Write(HelpButton);
        b.Write(Header);
        return b;
    }
    
    
}
