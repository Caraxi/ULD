namespace ULD.Node.Component;

public class ListComponentNode : BaseComponentNode {
    public ushort RowNum;
    public ushort ColumnNum;

    public override long Size => base.Size + 4;

    public override void Decode(Uld baseUld, BufferReader reader) {
        base.Decode(baseUld, reader);
        RowNum = reader.ReadUInt16();
        ColumnNum = reader.ReadUInt16();
    }

    public override byte[] Encode() {
        var w = new BufferWriter();
        w.Write(base.Encode());
        w.Write(RowNum);
        w.Write(ColumnNum);
        return w;
    }
}
