namespace ULD.Timelines.Keyframe;

public class UShort1Keyframe : KeyframeBase {
    public ushort Value;
    
    public override long Size => BaseSize + 4;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value);
        b.Write((ushort)0); // Padding
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value = reader.ReadUInt16();
        reader.ReadUInt16(); // Padding
    }
}