namespace ULD.Keyframe;

public class Bool1Keyframe : KeyframeBase {

    public bool Value;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value = reader.ReadBoolean();
    }
}