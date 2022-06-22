namespace ULD;

public class Parts : IEncodable {
    public uint Id;
    public List<Part> SubParts = new();

    public long Size => 12 + SubParts.Sum(sp => sp.Size);

    public byte[] Encode() {
        var data = new BufferWriter();
        data.Write(Id);
        data.Write((uint)SubParts.Count);    
        if (Size > uint.MaxValue) throw new Exception("Too many parts");
        data.Write((uint)Size);
        foreach(var part in SubParts) data.Write(part.Encode());
        return data.ToArray();
    }

    public void Decode(ULD baseUld, BufferReader reader) {
        Id = reader.ReadUInt32();
        var partCount = reader.ReadUInt32();
        var size = reader.ReadUInt32();
        for(var i = 0; i < partCount; i++) {
            var part = new Part();
            part.Decode(baseUld, reader);
            SubParts.Add(part);
        }
        if (Size != size) throw new Exception("Part size mismatch. Expected " + size + " but got " + Size);
    }
}
