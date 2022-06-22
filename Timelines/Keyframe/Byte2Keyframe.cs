namespace ULD.Timelines.Keyframe;

public class Byte2Keyframe : KeyframeBase {
    public byte[] Value = new byte[2];
    
    public override long Size => BaseSize + 4;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value[0]);
        b.Write(Value[1]);
        b.Write((byte)0); // Padding
        b.Write((byte)0);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value[0] = reader.ReadByte();
        Value[1] = reader.ReadByte();
        reader.ReadBytes(2); // Padding
    }
}