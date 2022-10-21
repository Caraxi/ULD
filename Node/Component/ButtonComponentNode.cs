namespace ULD.Node.Component;

public class ButtonComponentNode : BaseComponentNode {
    public uint TextId;

    public override long Size => base.Size + 4;

    public override void Decode(Uld baseUld, BufferReader reader) {
        base.Decode(baseUld, reader);
        TextId = reader.ReadUInt32();
    }
    
    public override byte[] Encode() {
        var b = new BufferWriter();
        b.Write(base.Encode());
        b.Write(TextId);
        return b;
    }
    
}

public class CheckBoxComponentNode : ButtonComponentNode { }
public class HoldButtonComponentNode : ButtonComponentNode { }
