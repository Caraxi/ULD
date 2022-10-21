namespace ULD.Node.Component;

public class NumericInputComponentNode : BaseComponentNode {

    public int Value;
    public int Max;
    public int Min;
    public int Add;
    public uint Unk1;
    public bool Comma;
    public byte[] Unk2;

    public override long Size => base.Size + 24;

    public override void Decode(Uld baseUld, BufferReader reader) {
        base.Decode(baseUld, reader);
        Value = reader.ReadInt32();
        Max = reader.ReadInt32();
        Min = reader.ReadInt32();
        Add = reader.ReadInt32();
        Unk1 = reader.ReadUInt32();
        Comma = reader.ReadBoolean();
        Unk2 = reader.ReadBytes(3); // Padding ?
    }

    public override byte[] Encode() {
        var w = new BufferWriter();
        w.Write(base.Encode());
        w.Write(Value);
        w.Write(Max);
        w.Write(Min);
        w.Write(Add);
        w.Write(Unk1);
        w.Write(Comma);
        w.Write(new byte[3]); // Padding
        return w;
    }
    
    
}
