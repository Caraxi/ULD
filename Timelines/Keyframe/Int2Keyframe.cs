namespace ULD.Timelines.Keyframe;

public class Int2Keyframe : KeyframeBase {
    public int[] Value = new int[2];
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value[0]);
        b.Write(Value[1]);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value[0] = reader.ReadInt32();
        Value[1] = reader.ReadInt32();
    }
}