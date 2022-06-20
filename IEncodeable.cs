namespace ULD; 

public interface IEncodeable {
    public byte[] Encode();
    public void Decode(ULD baseUld, BufferReader reader);
}
