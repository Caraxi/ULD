namespace ULD.Timelines.Keyframe;

public class Short3Keyframe : KeyframeBase {
    public short[] Value = new short[3];
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value[0]);
        b.Write(Value[1]);
        b.Write(Value[2]);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value[0] = reader.ReadInt16();
        Value[1] = reader.ReadInt16();
        Value[2] = reader.ReadInt16();
    }
}