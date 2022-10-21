namespace ULD;


public abstract class ListElement : IEncodable, IHasID {
    public abstract byte[] Encode(string version);
    public byte[] Encode() => Encode(DefaultVersion);
    public abstract void Decode(Uld baseUld, BufferReader reader, string version);
    public void Decode(Uld baseUld, BufferReader reader) => Decode(baseUld, reader, DefaultVersion);

    public abstract long GetSize(string version);
    public long Size => GetSize(DefaultVersion);
    protected virtual string DefaultVersion => "0100";

    public uint Id { get; set; }
}

public abstract class ListHeader<T> : Header, IEncodable where T : ListElement, new() {
    
    
    public int Unknown;

    
    public List<T> Elements { get; set; } = new();

    public Type ElementType => typeof(T);

    public uint ElementCount => (uint)Elements.Count;

    public override bool ShouldEncode() => Elements.Count > 0;

    protected abstract string HeaderType { get; }

    protected virtual long NextOffset(T element) {
        return element.GetSize(Version);
    }
    
    protected ListHeader(string name) : base(name) { }

    protected ListHeader(Uld baseUld, BufferReader r, string headerType) : base(baseUld, r, headerType) {
        
    }

    protected virtual T CreateElementObject(Uld baseUld, BufferReader r) {
        return new T();
    }
    
    public void Decode(Uld baseUld, BufferReader r) {
        
        Logging.IndentLog($"Decoding {GetType().Name} @ {BaseOffset}");
        
        var elementCount = r.ReadUInt32();
        Logging.Log($" - Elements: {elementCount}");
        Unknown = r.ReadInt32();

        for (var i = 0; i < elementCount; i++) {
            var pos = r.BaseStream.Position;
            var t = CreateElementObject(baseUld, r);
            Elements.Add(t);
            t.Decode(baseUld, r, Version);
            var offset = NextOffset(t);
            if (offset > 0) {
                r.Seek(pos + offset);
            }
        }
        
        AfterDecode(baseUld, r);
        Logging.Unindent();
    }

    public virtual long Size => Elements.Sum(e => e.GetSize(Version));

    protected virtual void AfterDecode(Uld baseUld, BufferReader reader) {}
    

    public byte[] Encode() {
        var bytes = new BufferWriter();
        bytes.Write(HeaderType);
        bytes.Write(Version);
        bytes.Write((uint) Elements.Count);
        bytes.Write((uint) Unknown);

        foreach (var e in Elements) {
            bytes.Write(e.Encode(Version));
        }
        
        return bytes.ToArray();
    }

}
