namespace ULD.Keyframe;

public class Byte1Keyframe : KeyframeBase {
    public byte Value;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value = reader.ReadByte();
    }
}