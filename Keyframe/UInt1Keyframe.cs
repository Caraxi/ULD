namespace ULD.Keyframe;

public class UInt1Keyframe : KeyframeBase {
    public uint Value;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value = reader.ReadUInt32();
    }
}