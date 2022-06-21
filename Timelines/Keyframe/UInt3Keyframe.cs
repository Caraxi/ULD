namespace ULD.Timelines.Keyframe;

public class UInt3Keyframe : KeyframeBase {
    public uint[] Value = new uint[3];
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value[0]);
        b.Write(Value[1]);
        b.Write(Value[2]);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value[0] = reader.ReadUInt32();
        Value[1] = reader.ReadUInt32();
        Value[2] = reader.ReadUInt32();
    }
}