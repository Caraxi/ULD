namespace ULD.Component;

public class ComponentButton : ComponentBase {
    protected override long Size => base.Size + 8;

    public uint[] Data = new uint[2];
    
    protected override void DecodeData(ULD baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        for (var i = 0; i < Data.Length; i++) {
            Data[i] = br.ReadUInt32();
        }
    }

    protected override void EncodeData(BufferWriter writer) {
        base.EncodeData(writer);
        foreach(var d in Data) writer.Write(d);
    }
}