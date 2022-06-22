namespace ULD.Component; 

public class ScrollBarComponent : ComponentBase {
    protected override int DataCount => 4;

    public override long Size => base.Size + 4;
    
    public ushort Margin;
    public bool IsVertical;
    public sbyte Padding;
    
    protected override void DecodeData(ULD baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        Margin = br.ReadUInt16();
        IsVertical = br.ReadBoolean();
        Padding = br.ReadSByte();
    }

    protected override void EncodeData(BufferWriter writer) {
        base.EncodeData(writer);
        writer.Write(Margin);
        writer.Write(IsVertical);
        writer.Write(Padding);
    }
}
