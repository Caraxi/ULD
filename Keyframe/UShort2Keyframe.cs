namespace ULD.Keyframe;

public class UShort2Keyframe : KeyframeBase {
    public ushort[] Value = new ushort[2];
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value[0]);
        b.Write(Value[1]);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value[0] = reader.ReadUInt16();
        Value[1] = reader.ReadUInt16();
    }
}