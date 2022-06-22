namespace ULD; 

public class BufferReader : BinaryReader {
    public static implicit operator BufferReader(byte[] data) => new(data);


    private readonly Stack<long> positions = new();

    public void Push() => positions.Push(BaseStream.Position);

    public void Pop() {
        if (positions.TryPop(out var p)) {
            BaseStream.Position = p;
            return;
        }
        throw new Exception("No position to pop");
    }


    public ushort PeekUInt16(long relativeOffset) {
        Push();
        this.BaseStream.Position += relativeOffset;
        var v = ReadUInt16();
        Pop();
        return v;
    }
    
    public int PeekInt32(long relativeOffset) {
        Push();
        this.BaseStream.Position += relativeOffset;
        var v = ReadInt32();
        Pop();
        return v;
    }

    public uint PeekUInt32(long relativeOffset) {
        Push();
        this.BaseStream.Position += relativeOffset;
        var v = ReadUInt32();
        Pop();
        return v;
    }

    public byte PeekByte(long relativeOffset) {
        Push();
        this.BaseStream.Position += relativeOffset;
        var v = ReadByte();
        Pop();
        return v;
    }


    public uint[] ReadUInt32(int count) {
        var ret = new uint[count];
        for(var i = 0; i < count; i++) {
            ret[i] = ReadUInt32();
        }
        return ret;
    }
    
    
    

    public void Seek(long offset) => this.BaseStream.Position = offset;
    
    public BufferReader(byte[] file) : base(new MemoryStream(file)) { }
}
