using System.Text;

namespace ULD; 

public class Header {
    protected string Type;
    protected string Version { get; set; } = "0100";
    
    protected ULD? BaseULD;
    protected long BaseOffset;

    public virtual bool ShouldEncode() => true;
    protected virtual string[] AcceptedVersions => new string[] { "0100" };

    protected Header(string type) {
        Type = type;
    }
    
    protected Header(BufferReader? r, string? expectedType = null) : this(null, r, expectedType) { }

    protected Header(ULD? baseUld, BufferReader? r, string? expectedType = null) {
        if (this is ULD uld && baseUld == null) baseUld = uld;
        BaseULD = baseUld ?? throw new Exception("No base ULD provided");
        if (r != null) BaseOffset = r.BaseStream.Position;
        Type = Encoding.UTF8.GetString(r.ReadBytes(4));
        var version = Encoding.UTF8.GetString(r.ReadBytes(4));
        if (!AcceptedVersions.Contains(version)) throw new Exception($"Unsupported {Type} Header Version {version}");
        if (expectedType != null && Type != expectedType) {
            throw new Exception($"Unexpected Header Type '{Type}'. Was expecting '{expectedType}'");
        }

        Version = version;
    }
}
