namespace ULD.Keyframe;

public class Short1Keyframe : KeyframeBase {
    public short Value;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value = reader.ReadInt16();
    }
}