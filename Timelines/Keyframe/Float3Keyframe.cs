namespace ULD.Timelines.Keyframe;

public class Float3Keyframe : KeyframeBase {
    public float[] Value = new float[3];
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value[0]);
        b.Write(Value[1]);
        b.Write(Value[2]);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value[0] = reader.ReadSingle();
        Value[1] = reader.ReadSingle();
        Value[2] = reader.ReadSingle();
    }
}