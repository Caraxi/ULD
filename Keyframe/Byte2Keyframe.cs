namespace ULD.Keyframe;

public class Byte2Keyframe : KeyframeBase {
    public byte[] Value = new byte[2];
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value[0]);
        b.Write(Value[1]);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value[0] = reader.ReadByte();
        Value[1] = reader.ReadByte();
    }
}