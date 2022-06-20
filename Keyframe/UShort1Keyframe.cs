namespace ULD.Keyframe;

public class UShort1Keyframe : KeyframeBase {
    public ushort Value;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value = reader.ReadUInt16();
    }
}