using System.Text;

namespace ULD; 

public class BufferWriter {
    private List<byte> data = new();
    public static implicit operator byte[](BufferWriter b) => b.data.ToArray();
    public byte[] ToArray() => data.ToArray();
    public uint Length => (uint)data.Count;
    
    public void Write(sbyte value) => data.Add((byte)value);
    public void Write(byte value) => data.Add(value);
    
    public void Write(short value) => data.AddRange(BitConverter.GetBytes(value));
    public void Write(ushort value) => data.AddRange(BitConverter.GetBytes(value));
    
    public void Write(int value) => data.AddRange(BitConverter.GetBytes(value));
    public void Write(uint value) => data.AddRange(BitConverter.GetBytes(value));
    
    public void Write(long value) => data.AddRange(BitConverter.GetBytes(value));
    public void Write(ulong value) => data.AddRange(BitConverter.GetBytes(value));
    
    public void Write(float value) => data.AddRange(BitConverter.GetBytes(value));
    public void Write(double value) => data.AddRange(BitConverter.GetBytes(value));
    
    
    
    public void Write(string value) => data.AddRange(Encoding.UTF8.GetBytes(value));
    public void Write(bool value) => data.Add((byte)(value ? 0 : 1));
    
    public void Write(BufferWriter o) => data.AddRange(o.data);
    public void Write(IEnumerable<uint> value) {
        foreach (var v in value) Write(v);
    }
    public void Write(IEnumerable<int> value) {
        foreach (var v in value) {
            Write(v);
        }
    }
    public void Write(IEnumerable<byte> value) {
        foreach (var v in value) {
            Write(v);
        }
    }
}