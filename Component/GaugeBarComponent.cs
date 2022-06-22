namespace ULD.Component;

public class GaugeBarComponent : ComponentBase {
    protected override int DataCount => 6;
    
    public override long Size => base.Size + 8;
    
    public ushort VerticalMargin;
    public ushort HorizontalMargin;
    public bool IsVertical;
    public byte[] Padding = new byte[3];
    
    protected override void DecodeData(ULD baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        VerticalMargin = br.ReadUInt16();
        HorizontalMargin = br.ReadUInt16();
        IsVertical = br.ReadBoolean();
        Padding = br.ReadBytes(3);
    }

    protected override void EncodeData(BufferWriter writer) {
        base.EncodeData(writer);
        writer.Write(VerticalMargin);
        writer.Write(HorizontalMargin);
        writer.Write(IsVertical);
        writer.Write(Padding);
    }
    
}