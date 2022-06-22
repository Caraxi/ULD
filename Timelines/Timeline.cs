namespace ULD.Timelines;

public class Timeline : IEncodable {

    public uint Id;

    public List<Frame> FrameSet0 = new();
    public List<Frame> FrameSet1 = new();

    public long Size => 12 + FrameSet0.Sum(fs => fs.Size) + FrameSet1.Sum(fs => fs.Size);
    
    
    public byte[] Encode() {
        var data = new BufferWriter();
        data.Write(Id);
        if (Size > uint.MaxValue) throw new Exception("Timeline is too large to encode");
        data.Write((uint) Size);
        data.Write((ushort) FrameSet0.Count);
        data.Write((ushort) FrameSet1.Count);
        foreach (var f in FrameSet0) data.Write(f.Encode());
        foreach(var f in FrameSet1) data.Write(f.Encode());
        return data.ToArray();
    }

    public void Decode(ULD baseUld, BufferReader reader) {
        Logging.IndentLog($"Decoding Timeline @ {reader.BaseStream.Position}");
        Id = reader.ReadUInt32();
        Logging.Log(" - ID: " + Id);
        var size = reader.ReadUInt16();
        reader.ReadUInt16();
        var fc0 = reader.ReadUInt16();
        var fc1 = reader.ReadUInt16();

        FrameSet0 = new List<Frame>();
        FrameSet1 = new List<Frame>();
        for (var i = 0; i < fc0; i++) {
            var frame = new Frame();
            frame.Decode(baseUld, reader);
            FrameSet0.Add(frame);
        }
        for (var i = 0; i < fc1; i++) {
            var frame = new Frame();
            frame.Decode(baseUld, reader);
            FrameSet1.Add(frame);
        }
        
        if (size != Size) throw new Exception("Timeline size value mismatch. Expected: " + Size + " Actual: " + size);
        
        Logging.Unindent();
    }
}
