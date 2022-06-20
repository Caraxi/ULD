namespace ULD.Keyframe;

public class Int1Keyframe : KeyframeBase {
    public int Value;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value = reader.ReadInt32();
    }
}