namespace ULD.Timelines;

public class Timeline : ListElement {
    public List<Frame> FrameSet0 = new();
    public List<Frame> FrameSet1 = new();

    public override long GetSize(string version) => 12 + FrameSet0.Sum(fs => fs.Size) + FrameSet1.Sum(fs => fs.Size);
    
    
    public override byte[] Encode(string version) {
        var data = new BufferWriter();
        data.Write(Id);
        var vSize = GetSize(version);
        if (vSize > uint.MaxValue) throw new Exception("Timeline is too large to encode");
        data.Write((uint) vSize);
        data.Write((ushort) FrameSet0.Count);
        data.Write((ushort) FrameSet1.Count);
        foreach (var f in FrameSet0) data.Write(f.Encode());
        foreach(var f in FrameSet1) data.Write(f.Encode());
        return data.ToArray();
    }

    public override void Decode(ULD baseUld, BufferReader reader, string version) {
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

        var vSize = GetSize(version);
        if (size != vSize) throw new Exception("Timeline size value mismatch. Expected: " + vSize + " Actual: " + size);
        
        Logging.Unindent();
    }
}
