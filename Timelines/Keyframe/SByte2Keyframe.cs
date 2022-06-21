namespace ULD.Timelines.Keyframe;

public class SByte2Keyframe : KeyframeBase {
    public sbyte[] Value = new sbyte[2];
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value[0]);
        b.Write(Value[1]);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value[0] = reader.ReadSByte();
        Value[1] = reader.ReadSByte();
    }
}