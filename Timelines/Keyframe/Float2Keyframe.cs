namespace ULD.Timelines.Keyframe;

public class Float2Keyframe : KeyframeBase {
    public float[] Value = new float[2];

    public override long Size => BaseSize + 8;

    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value[0]);
        b.Write(Value[1]);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value[0] = reader.ReadSingle();
        Value[1] = reader.ReadSingle();
    }
}