namespace ULD.Node.Component;

public class SliderComponentNode : BaseComponentNode {
    public int Min;
    public int Max;
    public int Step;

    public override long Size => base.Size + 12;

    public override void Decode(ULD baseUld, BufferReader reader) {
        base.Decode(baseUld, reader);
        Min = reader.ReadInt32();
        Max = reader.ReadInt32();
        Step = reader.ReadInt32();
    }

    public override byte[] Encode() {
        var w = new BufferWriter();
        w.Write(base.Encode());
        w.Write(Min);
        w.Write(Max);
        w.Write(Step);
        return w;
    }
}
