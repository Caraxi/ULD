namespace ULD.Node.Component;

public class ListItemComponentNode : BaseComponentNode {
    public bool Toggle;
    public byte[] Unknown = new byte[3];
    
    public override long Size => base.Size + 4;

    public override void Decode(Uld baseUld, BufferReader reader) {
        base.Decode(baseUld, reader);
        Toggle = reader.ReadBoolean();
        Unknown = reader.ReadBytes(3);
    }

    public override byte[] Encode() {
        var w = new BufferWriter();
        w.Write(base.Encode());
        w.Write(Toggle);
        w.Write(Unknown);
        return w;
    }
}
