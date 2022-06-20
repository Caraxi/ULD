namespace ULD.Keyframe;

public class LabelKeyframe : KeyframeBase {
    public ushort LabelId;
    public byte LabelCommand;
    public byte JumpId;
    
    protected override void EncodeKeyframeData(BufferWriter b) {
        b.Write(LabelId);
        b.Write(LabelCommand);
        b.Write(JumpId);
    }

    protected override void DecodeKeyframeData(BufferReader reader) {
        LabelId = reader.ReadUInt16();
        LabelCommand = reader.ReadByte();
        JumpId = reader.ReadByte();
    }
}