namespace ULD.Node.Component;

public class GaugeComponentNode : BaseComponentNode {
    public int Indicator;
    public int Min;
    public int Max;
    public int Value;

    public override long Size => base.Size + 16;

    public override void Decode(ULD baseUld, BufferReader reader) {
        base.Decode(baseUld, reader);
        Indicator = reader.ReadInt32();
        Min = reader.ReadInt32();
        Max = reader.ReadInt32();
        Value = reader.ReadInt32();
    }
    
    public override byte[] Encode() {
        var b = new BufferWriter();
        b.Write(base.Encode());
        b.Write(Indicator);
        b.Write(Min);
        b.Write(Max);
        b.Write(Value);
        return b;
    }
}
