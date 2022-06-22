namespace ULD.Timelines.Keyframe;

public class Bool1Keyframe : KeyframeBase {

    public bool Value;

    public override long Size => BaseSize + 1;

    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value = reader.ReadBoolean();
    }
}