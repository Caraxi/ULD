namespace ULD.Timelines.Keyframe;

public abstract class KeyframeBase : IEncodeable {
    public uint Time;
    public ushort Offset;
    public byte Interpolation;
    public byte Unk1;
    public float Acceleration;
    public float Deceleration;

    public abstract long Size { get; }// => 16;
    protected long BaseSize { get; } = 16;
    
    public byte[] Encode() {
        var b = new BufferWriter();
        b.Write(Time);
        if (Size > ushort.MaxValue) throw new Exception("Keyframe is too large to encode");
        b.Write((ushort) Size);
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
        var size = reader.ReadUInt16();
        Interpolation = reader.ReadByte();
        Unk1 = reader.ReadByte();
        Acceleration = reader.ReadSingle();
        Deceleration = reader.ReadSingle();
        DecodeKeyframeData(reader);
        if (Size != size) throw new Exception($"{GetType().Name} size mismatch. Expected " + Size + " but got " + size);
    }
    
    protected abstract void DecodeKeyframeData(BufferReader reader); 
}
