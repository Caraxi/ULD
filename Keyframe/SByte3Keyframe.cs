namespace ULD.Keyframe;

public class SByte3Keyframe : KeyframeBase {
    public sbyte[] Value = new sbyte[3];
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value[0]);
        b.Write(Value[1]);
        b.Write(Value[2]);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value[0] = reader.ReadSByte();
        Value[1] = reader.ReadSByte();
        Value[2] = reader.ReadSByte();
    }
}