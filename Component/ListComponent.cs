namespace ULD.Component;

public class ListComponent : ComponentBase {

    public byte Wrap;
    public byte Orientation;
    public byte[] Padding = new byte[2];
    
    protected override int DataCount => 5;
    public override long Size => base.Size + 4;

    protected override void DecodeData(ULD baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        Wrap = br.ReadByte();
        Orientation = br.ReadByte();
        Padding = br.ReadBytes(2);
    }

    protected override void EncodeData(BufferWriter writer) {
        base.EncodeData(writer);
        writer.Write(Wrap);
        writer.Write(Orientation);
        writer.Write(Padding);
    }
    
    
    
}