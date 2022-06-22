namespace ULD.Timelines.Keyframe;

public class UInt1Keyframe : KeyframeBase {
    public uint Value;
    
    public override long Size => BaseSize + 4;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value = reader.ReadUInt32();
    }
}