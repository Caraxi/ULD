namespace ULD.Timelines.Keyframe;

public class SByte1Keyframe : KeyframeBase {
    public sbyte Value;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value = reader.ReadSByte();
    }
}