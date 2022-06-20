namespace ULD; 

public class ULD : Header {
    
    public ATK ATK;
    public ATK ATK2;
    
    public ULD(BufferReader r) : base(r, "uldh") {
        
        var atkOffset = r.ReadUInt32();
        var atk2Offset = r.ReadUInt32();

        if (atkOffset != 0) {
            r.Push();
            r.Seek(atkOffset);
            ATK = new ATK(this, r);
            r.Pop();
        }
        
        if (atk2Offset != 0) {
            r.Push();
            r.Seek(atk2Offset);
            ATK2 = new ATK(this, r);
            r.Pop();
        }
    }

    public byte[] Encode() {
        var atk = ATK.Encode();
        var atk2 = ATK2.Encode();
        var bytes = new BufferWriter();
        bytes.Write("uldh");
        bytes.Write("0100");
        bytes.Write(16U);
        bytes.Write((uint)(16 + atk.Length));
        bytes.Write(atk);
        bytes.Write(atk2);
        return bytes.ToArray();
    }
    
    public BufferWriter DebugExport() {
        var atk = ATK.Encode();
        var atk2 = ATK2.Encode();
        var bytes = new BufferWriter();
        bytes.Write("uldh");
        bytes.Write("0100");
        bytes.Write(16U);
        bytes.Write((uint)(16 + atk.Length));
        bytes.Write(atk);
        bytes.Write(atk2);
        return bytes;
    }

    public Component? GetComponent(uint id) {
        Component? component = null;
        if (ATK.Components != null) component = ATK.Components?.Elements.Find(c => c.Id == id);
        if (component == null && ATK2.Components != null) component = ATK.Components?.Elements.Find(c => c.Id == id);
        return component;
    }
}
