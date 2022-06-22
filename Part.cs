namespace ULD;

public class Part : IEncodable {
    public uint TextureId;
    public ushort U;
    public ushort V;
    public ushort W;
    public ushort H;

    public long Size => 12;
    
    public byte[] Encode() {
        var data = new BufferWriter();
        data.Write(TextureId);
        data.Write(U);
        data.Write(V);
        data.Write(W);
        data.Write(H);
        return data.ToArray();
    }

    public void Decode(ULD baseUld, BufferReader reader) {
        TextureId = reader.ReadUInt32();
        U = reader.ReadUInt16();
        V = reader.ReadUInt16();
        W = reader.ReadUInt16();
        H = reader.ReadUInt16();
    }
}
