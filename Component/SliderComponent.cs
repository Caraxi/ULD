namespace ULD.Component;

public class SliderComponent : ComponentBase {
    protected override int DataCount => 4;
    
    public override long GetSize(string version) => base.GetSize(version) + 4;

    public bool IsVertical;
    public byte LeftOffset;
    public byte RightOffset;
    public sbyte Padding;
    
    protected override void DecodeData(ULD baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        IsVertical = br.ReadBoolean();
        LeftOffset = br.ReadByte();
        RightOffset = br.ReadByte();
        Padding = br.ReadSByte();
    }

    protected override void EncodeData(BufferWriter writer) {
        base.EncodeData(writer);
        writer.Write(IsVertical);
        writer.Write(LeftOffset);  
        writer.Write(RightOffset);
        writer.Write(Padding);
    }
    
}