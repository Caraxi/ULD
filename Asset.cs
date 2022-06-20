using System.Text;

namespace ULD;

public class Asset : IEncodeable {
    
    public uint Id;
    public string Path;
    public uint Unk1;
    public uint Unk2;
    
    public byte[] Encode() {
        var data = new BufferWriter();
        data.Write(Id);
        data.Write(Path);
        while(data.Length < 48) data.Write((byte)0);
        data.Write(Unk1);
        data.Write(Unk2);
        return data.ToArray();
    }

    public void Decode(ULD baseUld, BufferReader reader) {
        Id = reader.ReadUInt32();
        Path = Encoding.UTF8.GetString(reader.ReadBytes(44)).Split('\0')[0];
        Unk1 = reader.ReadUInt32();
        Unk2 = reader.ReadUInt32();
    }
}
