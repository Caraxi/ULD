namespace ULD.Timelines.Keyframe;

public class Bool3Keyframe : KeyframeBase {

    public bool[] Value = new bool[3];

    public override long Size => BaseSize + 3;

    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value[0]);
        b.Write(Value[1]);
        b.Write(Value[2]);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value[0] = reader.ReadBoolean();
        Value[1] = reader.ReadBoolean();
        Value[2] = reader.ReadBoolean();
    }
}