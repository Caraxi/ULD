namespace ULD; 

public interface IEncodable {
    public byte[] Encode();
    public void Decode(ULD baseUld, BufferReader reader);
    
    public long Size { get; }
    
}
