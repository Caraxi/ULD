namespace ULD.Timelines.Keyframe;

public class Byte1Keyframe : KeyframeBase {
    public override long Size => base.Size + 4;

    public byte Value;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value);
        
        // Padding
        b.Write((byte) 0);
        b.Write((byte) 0);
        b.Write((byte) 0);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value = reader.ReadByte();
    }
}