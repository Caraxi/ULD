
using ULD.Component;
using ULD.Node;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ULD; 

public class Uld : Header {

    private bool singleAtk = false;


    public ATK?[] ATK = new ATK?[2] { new ATK(), new ATK() };


    public AssetList? Assets => ATK[0]?.Assets ?? ATK[1]?.Assets ?? null;
    public PartList? Parts => ATK[0]?.Parts ?? ATK[1]?.Parts ?? null;
    public ComponentList? Components => ATK[0]?.Components ?? ATK[1]?.Components ?? null;
    public TimelineList? Timelines => ATK[0]?.Timelines ?? ATK[1]?.Timelines ?? null;
    public WidgetList? Widgets => ATK[1]?.Widgets ?? ATK[0]?.Widgets ?? null;
    
    public Uld(BufferReader r) : base(r, "uldh") {
        Decode(r);
    }

    public Uld() : base("uldh") {
        
    }
    
    public void Decode(BufferReader r) {
        Logging.ZeroIndent();
        Logging.IndentLog($"Decoding ULD @ {r.BaseStream.Position}");
        var atkOffset = r.ReadUInt32();
        var atk2Offset = r.ReadUInt32();

        if (atkOffset != 0) {
            r.Push();
            r.Seek(atkOffset);
            ATK[0] = new ATK(this, r);
            ATK[0]?.Decode(this, r);
            r.Pop();
        }
        
        if (atk2Offset != 0 && atk2Offset != atkOffset) {
            r.Push();
            r.Seek(atk2Offset);
            ATK[1] = new ATK(this, r);
            ATK[1]?.Decode(this, r);
            r.Pop();
        } else {
            singleAtk = true;
        }
        
        
        Logging.Unindent();
    }
    
    
    public BufferWriter Encode() {
        Logging.ZeroIndent();
        Logging.IndentLog("Encoding ULD");
        var atk = ATK[0]?.Encode(this, 0) ?? Array.Empty<byte>();
        var atk2 = ATK[1]?.Encode(this, 1) ?? Array.Empty<byte>();
        var bytes = new BufferWriter();
        bytes.Write("uldh");
        bytes.Write("0100");
        bytes.Write(ATK[0] == null ? 0U : 16U);
        if (singleAtk) {
            bytes.Write(ATK[0] == null ? 0U : 16U);
        } else {
            bytes.Write(ATK[1] == null ? 0U : (uint)(16 + atk.Length));
        }
        
        bytes.Write(atk);
        bytes.Write(atk2);
        Logging.Unindent();
        return bytes;
    }

    public ComponentBase? GetComponent(uint id) {
        foreach (var atk in ATK) {
            var c = atk?.Components?.Elements.Find(c => c.Id == id);
            if (c != null) return c;
        }
        return null;
    }
    
    public ComponentBase? GetComponent(NodeType nodeType) => GetComponent((uint) nodeType);
    

    public Asset? GetAsset(uint id) {
        foreach (var atk in ATK) {
            var c = atk?.Assets?.Elements.Find(c => c.Id == id);
            if (c != null) return c;
        }
        return null;
    }
    
}
