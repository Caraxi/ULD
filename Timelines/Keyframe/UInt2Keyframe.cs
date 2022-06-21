namespace ULD.Timelines.Keyframe;

public class UInt2Keyframe : KeyframeBase {
    public uint[] Value = new uint[2];
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value[0]);
        b.Write(Value[1]);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value[0] = reader.ReadUInt32();
        Value[1] = reader.ReadUInt32();
    }
}