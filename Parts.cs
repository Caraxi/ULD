namespace ULD;

public class Parts : IEncodeable {
    public uint Id;
    public uint Offset;
    public List<Part> SubParts = new();

    public byte[] Encode() {
        var data = new BufferWriter();
        data.Write(Id);
        data.Write((uint)SubParts.Count);    
        data.Write(Offset);
        foreach(var part in SubParts) data.Write(part.Encode());
        return data.ToArray();
    }

    public void Decode(ULD baseUld, BufferReader reader) {
        Id = reader.ReadUInt32();
        var partCount = reader.ReadUInt32();
        Offset = reader.ReadUInt32();
        for(var i = 0; i < partCount; i++) {
            var part = new Part();
            part.Decode(baseUld, reader);
            SubParts.Add(part);
        }
    }
}
