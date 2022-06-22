namespace ULD.Timelines.Keyframe;

public class ColorKeyframe : KeyframeBase {
    public override long Size => BaseSize + 12;

    public short MultiplyRed;
    public short MultiplyGreen;
    public short MultiplyBlue;
    public short AddRed;
    public short AddGreen;
    public short AddBlue;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(MultiplyRed);
        b.Write(MultiplyGreen);
        b.Write(MultiplyBlue);
        b.Write(AddRed);
        b.Write(AddGreen);
        b.Write(AddBlue);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        MultiplyRed = reader.ReadInt16();
        MultiplyGreen = reader.ReadInt16();
        MultiplyBlue = reader.ReadInt16();
        AddRed = reader.ReadInt16();
        AddGreen = reader.ReadInt16();
        AddBlue = reader.ReadInt16();
    }
}