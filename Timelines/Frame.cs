namespace ULD.Timelines;

public class Frame : IEncodable {

    public uint StartFrame;
    public uint EndFrame;
    public List<KeyGroup> KeyGroups = new();

    public long Size => 16 + KeyGroups.Sum(k => k.Size);
    
    public byte[] Encode() {
        var b = new BufferWriter();
        b.Write(StartFrame);
        b.Write(EndFrame);
        if (Size > uint.MaxValue) throw new Exception("Frame is too large to encode");
        b.Write((uint)Size);
        b.Write((uint) KeyGroups.Count);
        foreach (var k in KeyGroups) {
            b.Write(k.Encode());
        }
        return b.ToArray();
    }

    public void Decode(Uld baseUld, BufferReader reader) {
        Logging.IndentLog("Decoding Frame");
        StartFrame = reader.ReadUInt32();
        EndFrame = reader.ReadUInt32();
        var size = reader.ReadUInt32();
        
        var count = reader.ReadUInt32();
        KeyGroups = new List<KeyGroup>();
        for (var i = 0; i < count; i++) {
            var k = new KeyGroup();
            k.Decode(baseUld, reader);
            KeyGroups.Add(k);
        }
        if (size != Size) throw new Exception("Frame size value mismatch. Expected: " + Size + " Actual: " + size);
        Logging.Unindent();
    }
}
