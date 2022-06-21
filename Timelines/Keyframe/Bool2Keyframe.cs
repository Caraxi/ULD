namespace ULD.Timelines.Keyframe;

public class Bool2Keyframe : KeyframeBase {

    public bool[] Value = new bool[2];
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(Value[0]);
        b.Write(Value[1]);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        Value[0] = reader.ReadBoolean();
        Value[1] = reader.ReadBoolean();
    }
}