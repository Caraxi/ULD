namespace ULD.Timelines.Keyframe;

public class Float1Keyframe : KeyframeBase {
    public float Value;
    
    public override long Size => BaseSize + 4;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value = reader.ReadSingle();
    }
}