

namespace ULD; 

public interface IEncodable {
    public byte[] Encode();
    public void Decode(Uld baseUld, BufferReader reader);
    
    
    public long Size { get; }
    
}
