namespace ULD.Keyframe;

public class Float1Keyframe : KeyframeBase {
    public float Value;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value = reader.ReadSingle();
    }
}