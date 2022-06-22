namespace ULD.Component;

public class JournalCanvasComponent : ComponentBase {
    protected override int DataCount => 32;
    
      
    public override long Size => base.Size + 8;

    public ushort Margin;
    public ushort Unk1;
    public ushort Unk2;
    public ushort Padding;

    protected override void DecodeData(ULD baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        Margin = br.ReadUInt16();
        Unk1 = br.ReadUInt16();
        Unk2 = br.ReadUInt16();
        Padding = br.ReadUInt16();
    }

    protected override void EncodeData(BufferWriter writer) {
        base.EncodeData(writer);
        writer.Write(Margin);
        writer.Write(Unk1);
        writer.Write(Unk2);
        writer.Write(Padding);
    }
    
    
}