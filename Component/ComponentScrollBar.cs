namespace ULD.Component; 

public class ComponentScrollBar : ComponentBase {
    protected override long Size => base.Size + 20;

    public uint[] Data = new uint[4];

    public ushort Margin;
    public bool IsVertical;
    public sbyte Padding;
    
    protected override void DecodeData(ULD baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        for (var i = 0; i < Data.Length; i++) {
            Data[i] = br.ReadUInt32();
        }

        Margin = br.ReadUInt16();
        IsVertical = br.ReadBoolean();
        Padding = br.ReadSByte();
    }

    protected override void EncodeData(BufferWriter writer) {
        base.EncodeData(writer);
        foreach (var d in Data) { writer.Write(d); }
        writer.Write(Margin);
        writer.Write(IsVertical);
        writer.Write(Padding);
    }
}
