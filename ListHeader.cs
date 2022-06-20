namespace ULD;

public abstract class ListHeader<T> : Header where T : IEncodeable, new() {

    public int Unknown;

    public List<T> Elements = new();

    public uint ElementCount => (uint)Elements.Count;

    public override bool ShouldEncode() => Elements.Count > 0;

    protected abstract string HeaderType { get; }
    protected override string Version => "0100";

    protected virtual long NextOffset(T element) { return 0; }
    
    
    protected ListHeader(ULD baseUld, BufferReader r, string headerType) : base(baseUld, r, headerType) {
        Decode(baseUld, r);
    }

    public void Decode(ULD baseUld, BufferReader r) {
        var elementCount = r.ReadUInt32();
        Unknown = r.ReadInt32();

        for (var i = 0; i < elementCount; i++) {
            var pos = r.BaseStream.Position;
            var t = new T();
            t.Decode(baseUld, r);
            Elements.Add(t);
            var offset = NextOffset(t);
            if (offset > 0) {
                r.Seek(pos + offset);
            }
            
        }
    }
    

    public byte[] Encode() {
        var bytes = new BufferWriter();
        bytes.Write(HeaderType);
        bytes.Write(Version);
        bytes.Write((uint) Elements.Count);
        bytes.Write((uint) Unknown);

        foreach (var e in Elements) {
            bytes.Write(e.Encode());
        }
        
        return bytes.ToArray();
    }

}
