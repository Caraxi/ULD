using System.Text;
using Newtonsoft.Json;

namespace ULD;

[JsonObject(MemberSerialization.OptIn)]
public class Asset : IVersionedEncodable {
    
    [JsonProperty]
    public uint Id;
    
    [JsonProperty]
    public string Path = string.Empty;
    
    [JsonProperty]
    public uint Unk1;
    
    [JsonProperty]
    public uint Unk2;

    public long GetSize(string version) {
        return version switch {
            "0100" => 52,
            "0101" => 56,
            _ => throw new Exception($"Unsupported Asset Version: {version}")
        };
    }

    public byte[] Encode(string version) {

        var data = new BufferWriter();
        data.Write(Id);
        data.Write(Path);
        while(data.Length < 48) data.Write((byte)0);
        data.Write(Unk1);
        if (version == "0101") {
            
            data.Write(Unk2);
        }
        
        return data.ToArray();
    }

    public void Decode(ULD baseUld, BufferReader reader, string version) {
        Id = reader.ReadUInt32();
        Path = Encoding.UTF8.GetString(reader.ReadBytes(44)).Split('\0')[0];
        Unk1 = reader.ReadUInt32();
        if (version == "0101") {
            
            Unk2 = reader.ReadUInt32();
        }
    }
}
