namespace ULD.Timelines.Keyframe;

public class Short1Keyframe : KeyframeBase {
    public short Value;
    
    public override long Size => BaseSize + 2;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value = reader.ReadInt16();
    }
}