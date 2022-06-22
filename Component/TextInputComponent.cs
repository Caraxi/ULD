namespace ULD.Component;

public class TextInputComponent : ComponentBase {
    protected override int DataCount => 16;
    
    public override long Size => base.Size + 12;

    public uint Color;
    public uint IMEColor;
    public uint Unknown;
    
    
    protected override void DecodeData(ULD baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        Color = br.ReadUInt32();
        IMEColor = br.ReadUInt32();
        Unknown = br.ReadUInt32();
    }

    protected override void EncodeData(BufferWriter writer) {
        base.EncodeData(writer);
        writer.Write(Color);
        writer.Write(IMEColor);
        writer.Write(Unknown);
    }
    
    
    
}