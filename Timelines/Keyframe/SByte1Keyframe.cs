namespace ULD.Timelines.Keyframe;

public class SByte1Keyframe : KeyframeBase {
    public sbyte Value;
    
    public override long Size => BaseSize + 1;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value = reader.ReadSByte();
    }
}