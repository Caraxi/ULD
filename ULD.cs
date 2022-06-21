namespace ULD; 

public class ULD : Header {
    
    public ATK? ATK;
    public ATK? ATK2;
    
    public ULD(BufferReader r) : base(r, "uldh") {
        Logging.IndentLog($"Decoding ULD @ {r.BaseStream.Position}");
        var atkOffset = r.ReadUInt32();
        var atk2Offset = r.ReadUInt32();

        if (atkOffset != 0) {
            r.Push();
            r.Seek(atkOffset);
            ATK = new ATK(this, r);
            ATK.Decode(this, r);
            r.Pop();
        }
        
        if (atk2Offset != 0) {
            r.Push();
            r.Seek(atk2Offset);
            ATK2 = new ATK(this, r);
            ATK2.Decode(this, r);
            r.Pop();
        }
        
        
        Logging.Unindent();
    }
    
    public BufferWriter Encode() {
        Logging.ZeroIndent();
        Logging.IndentLog("Encoding ULD");
        var atk = ATK == null ? Array.Empty<byte>() : ATK.Encode();
        var atk2 = ATK2 == null ? Array.Empty<byte>() : ATK2.Encode();
        var bytes = new BufferWriter();
        bytes.Write("uldh");
        bytes.Write("0100");
        bytes.Write(ATK == null ? 0U : 16U);
        bytes.Write(ATK2 == null ? 0U : (uint)(16 + atk.Length));
        bytes.Write(atk);
        bytes.Write(atk2);
        Logging.Unindent();
        return bytes;
    }

    public Component.ComponentBase? GetComponent(uint id) {
        Component.ComponentBase? component = null;
        if (ATK?.Components != null) component = ATK.Components?.Elements.Find(c => c.Id == id);
        if (component == null && ATK2?.Components != null) component = ATK2.Components?.Elements.Find(c => c.Id == id);
        return component;
    }
}
