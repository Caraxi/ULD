using System.Text.Json.Serialization;

namespace ULD;

public abstract class ListHeader<T> : Header, IEncodable where T : IEncodable, new() {

    public int Unknown;

    public List<T> Elements = new();
    
    [JsonIgnore]
    public uint ElementCount => (uint)Elements.Count;

    public override bool ShouldEncode() => Elements.Count > 0;

    protected abstract string HeaderType { get; }
    protected override string Version => "0100";

    protected virtual long NextOffset(T element) { return 0; }
    
    protected ListHeader(string name) : base(name) { }

    protected ListHeader(ULD baseUld, BufferReader r, string headerType) : base(baseUld, r, headerType) {
        
    }

    protected virtual T CreateElementObject(ULD baseUld, BufferReader r) {
        return new T();
    }
    
    public void Decode(ULD baseUld, BufferReader r) {
        
        Logging.IndentLog($"Decoding {GetType().Name} @ {BaseOffset}");
        
        var elementCount = r.ReadUInt32();
        Logging.Log($" - Elements: {elementCount}");
        Unknown = r.ReadInt32();

        for (var i = 0; i < elementCount; i++) {
            var pos = r.BaseStream.Position;
            var t = CreateElementObject(baseUld, r);
            Elements.Add(t);
            t.Decode(baseUld, r);
            var offset = NextOffset(t);
            if (offset > 0) {
                r.Seek(pos + offset);
            }
        }
        
        AfterDecode(baseUld, r);
        Logging.Unindent();
    }

    public virtual long Size => Elements.Sum(e => e.Size);

    protected virtual void AfterDecode(ULD baseUld, BufferReader reader) {}
    

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
