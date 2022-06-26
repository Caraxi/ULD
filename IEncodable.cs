using Newtonsoft.Json;

namespace ULD; 

public interface IEncodable {
    public byte[] Encode();
    public void Decode(ULD baseUld, BufferReader reader);
    
    [JsonIgnore]
    public long Size { get; }
    
}
