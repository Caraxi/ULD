namespace ULD.Timelines.Keyframe;

public class Byte3Keyframe : KeyframeBase {
    public byte[] Value = new byte[3];
    
    public override long Size => BaseSize + 4;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value[0]);
        b.Write(Value[1]);
        b.Write(Value[2]);
        b.Write((byte)0); // Padding
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value[0] = reader.ReadByte();
        Value[1] = reader.ReadByte();
        Value[2] = reader.ReadByte();
        reader.ReadByte(); // Padding
    }
}