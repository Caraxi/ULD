namespace ULD.Keyframe;

public class Short2Keyframe : KeyframeBase {
    public short[] Value = new short[2];
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value[0]);
        b.Write(Value[1]);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value[0] = reader.ReadInt16();
        Value[1] = reader.ReadInt16();
    }
}