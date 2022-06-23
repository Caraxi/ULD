namespace ULD.Component;

public class NumericInputComponent : ComponentBase {
    protected override int DataCount => 5;
    
   
    public override long GetSize(string version) => base.GetSize(version) + 4;

    public uint Color;


    protected override void DecodeData(ULD baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        Color = br.ReadUInt32();
    }

    protected override void EncodeData(BufferWriter writer) {
        base.EncodeData(writer);
        writer.Write(Color);
    }

    
}