using System.Text;

namespace ULD;

public class Asset : ListElement {

    private string path = string.Empty;

    
    public string Path {
        get => path;
        set {
            var sub = value;
            while (Encoding.UTF8.GetBytes(sub).Length > 44) sub = sub[..^1];
            path = sub;
            
        }
    }
    
    
    public uint IconId;
    
    
    [Flags]
    public enum AssetThemes : uint {
        None = 0,
        Light = 1,
        Classic = 2,
    }
    
    
    public AssetThemes Themes;

    public override long GetSize(string version) {
        return version switch {
            "0100" => 52,
            "0101" => 56,
            _ => throw new Exception($"Unsupported Asset Version: {version}")
        };
    }

    public override byte[] Encode(string version) {

        var data = new BufferWriter();
        data.Write(Id);
        data.Write(Path);
        while(data.Length < 48) data.Write((byte)0);
        data.Write(IconId);
        if (version == "0101") {
            data.Write((uint)Themes);
        }
        
        return data.ToArray();
    }

    public override void Decode(Uld baseUld, BufferReader reader, string version) {
        Id = reader.ReadUInt32();
        Path = Encoding.UTF8.GetString(reader.ReadBytes(44)).Split('\0')[0];
        IconId = reader.ReadUInt32();
        if (version == "0101") {
            Themes = (AssetThemes) reader.ReadUInt32();
        }
    }
}
