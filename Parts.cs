namespace ULD;

public class Parts : IVersionedEncodable{
    public uint Id;
    public List<Part> SubParts = new();

    public long GetSize(string version) => 12 + SubParts.Sum(sp => sp.Size);

    public byte[] Encode(string version) {
        var data = new BufferWriter();
        data.Write(Id);
        data.Write((uint)SubParts.Count);
        var vSize = GetSize(version);
        if (vSize > uint.MaxValue) throw new Exception("Too many parts");
        data.Write((uint)vSize);
        foreach(var part in SubParts) data.Write(part.Encode());
        return data.ToArray();
    }

    public void Decode(ULD baseUld, BufferReader reader, string version) {
        Id = reader.ReadUInt32();
        var partCount = reader.ReadUInt32();
        var size = reader.ReadUInt32();
        for(var i = 0; i < partCount; i++) {
            var part = new Part();
            part.Decode(baseUld, reader);
            SubParts.Add(part);
        }
        var vSize = GetSize(version);
        if (vSize != size) throw new Exception("Part size mismatch. Expected " + size + " but got " + vSize);
    }
}
