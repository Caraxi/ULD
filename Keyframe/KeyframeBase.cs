namespace ULD.Keyframe;

public abstract class KeyframeBase : IEncodeable {
    public uint Time;
    public ushort Offset;
    public byte Interpolation;
    public byte Unk1;
    public float Acceleration;
    public float Deceleration;
    
    public byte[] Encode() {
        var b = new BufferWriter();
        b.Write(Time);
        b.Write(Offset);
        b.Write(Interpolation);
        b.Write(Unk1);
        b.Write(Acceleration);
        b.Write(Deceleration);
        EncodeKeyframeData(b);
        return b.ToArray();
    }

    protected abstract void EncodeKeyframeData(BufferWriter b);
    
    public void Decode(ULD baseUld, BufferReader reader) {
        Time = reader.ReadUInt32();
        Offset = reader.ReadUInt16();
        Interpolation = reader.ReadByte();
        Unk1 = reader.ReadByte();
        Acceleration = reader.ReadSingle();
        Deceleration = reader.ReadSingle();
        DecodeKeyframeData(reader);
    }
    
    protected abstract void DecodeKeyframeData(BufferReader reader); 
}
