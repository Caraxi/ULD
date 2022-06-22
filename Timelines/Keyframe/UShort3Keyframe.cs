namespace ULD.Timelines.Keyframe;

public class UShort3Keyframe : KeyframeBase {
    public ushort[] Value = new ushort[3];
    
    public override long Size => BaseSize + 6;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value[0]);
        b.Write(Value[1]);
        b.Write(Value[2]);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value[0] = reader.ReadUInt16();
        Value[1] = reader.ReadUInt16();
        Value[2] = reader.ReadUInt16();
    }
}