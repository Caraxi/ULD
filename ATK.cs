namespace ULD; 

public class ATK : Header {
    public AssetList? Assets;

    public PartList? Parts;
    public ComponentList? Components;
    public TimelineList? Timelines;

    public WidgetList? Widget;
    
    public uint WidgetOffset;
    public uint RewriteDataOffset;
    public uint TimelineListSize;

    public ATK() : base("atkh") {
        
    }

    public ATK(ULD uld, BufferReader r) : base(uld, r, "atkh") {
        
    }

    public void Decode(ULD uld, BufferReader r) {
        Logging.IndentLog($"Decoding ATK @ {r.BaseStream.Position}");
        var assetListOffset = r.ReadUInt32();
        if (assetListOffset != 0) {
            r.Push();
            r.Seek(BaseOffset + assetListOffset);
            Assets = new AssetList(uld, r);
            Assets.Decode(uld, r);
            r.Pop();
        }

        var partListOffset = r.ReadUInt32();
        if (partListOffset != 0) {
            r.Push();
            r.Seek(BaseOffset + partListOffset);
            Parts = new PartList(uld, r);
            Parts.Decode(uld, r);
            r.Pop();
        }
        
        var componentListOffset = r.ReadUInt32();
        if (componentListOffset != 0) {
            r.Push();
            r.Seek(BaseOffset + componentListOffset);
            Components = new ComponentList(uld, r);
            Components.Decode(uld, r);
            r.Pop();
        }

        
        var timelineListOffset = r.ReadUInt32();
        if (timelineListOffset != 0) {
            r.Push();
            r.Seek(BaseOffset + timelineListOffset);
            Timelines = new TimelineList(uld, r);
            Timelines.Decode(uld, r);
            r.Pop();
        }
        

        WidgetOffset = r.ReadUInt32();
        if (WidgetOffset != 0) {
            r.Push();
            r.Seek(BaseOffset + WidgetOffset);
            Widget = new WidgetList(uld, r);
            Widget.Decode(uld, r);
            r.Pop();
        }
        
        RewriteDataOffset = r.ReadUInt32();
        TimelineListSize = r.ReadUInt32();
        // TODO: Validate Timeline List Size?
    }
    
    
    public byte[] Encode() {
        var bytes = new BufferWriter();
        Logging.IndentLog("Encoding ATK");
        
        bytes.Write("atkh");
        bytes.Write("0100");
        
        var data = new BufferWriter();
        
        if (Assets != null && Assets.ShouldEncode()) {
            bytes.Write(36U + data.Length);
            data.Write(Assets.Encode());
        } else {
            bytes.Write(0U);
        }
        
        if (Parts != null && Parts.ShouldEncode()) {
            bytes.Write(36U + data.Length);
            data.Write(Parts.Encode());
        } else {
            bytes.Write(0U);
        }
        
        if (Components != null && Components.ShouldEncode()) {
            bytes.Write(36U + data.Length);
            data.Write(Components.Encode());
        } else {
            bytes.Write(0U);
        }
        
        if (Timelines != null && Timelines.ShouldEncode()) {
            bytes.Write(36U + data.Length);
            data.Write(Timelines.Encode());
        } else {
            bytes.Write(0U);
        }

        if (Widget != null && Widget.ShouldEncode()) {
            bytes.Write(36U + data.Length);
            data.Write(Widget.Encode());
        } else {
            bytes.Write(0U);
        }
        
        bytes.Write(RewriteDataOffset); // TODO: work out wtf this is
        bytes.Write(TimelineListSize); // TODO: work out wtf this really is. 
        
        
        bytes.Write(data);
        
        Logging.Unindent();
        
        return bytes.ToArray();
    }
    
}
