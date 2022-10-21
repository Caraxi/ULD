
using ULD.Node;
using ULD.Node.Component;

namespace ULD; 

public class ATK : Header {
    
    
    
    public AssetList? Assets;

    
    public PartList? Parts;
    
    
    public ComponentList? Components;
    
    
    public TimelineList? Timelines;

    
    public WidgetList? Widgets;
    
    
    public uint RewriteDataOffset;

     
    public uint NodesWithTimeline;
    
    public ATK() : base("atkh") {
        
    }

    public ATK(Uld uld, BufferReader r) : base(uld, r, "atkh") {
        
    }

    public void Decode(Uld uld, BufferReader r) {
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
        

        var widgetOffset = r.ReadUInt32();
        if (widgetOffset != 0) {
            r.Push();
            r.Seek(BaseOffset + widgetOffset);
            Widgets = new WidgetList(uld, r);
            Widgets.Decode(uld, r);
            r.Pop();
        }
        
        RewriteDataOffset = r.ReadUInt32();
        NodesWithTimeline = r.ReadUInt32();
        // TODO: Validate Timeline List Size?
    }
    
    
    public byte[] Encode(Uld uld, byte id) {
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

        if (Widgets != null && Widgets.ShouldEncode()) {
            bytes.Write(36U + data.Length);
            data.Write(Widgets.Encode());
        } else {
            bytes.Write(0U);
        }
        
        bytes.Write(RewriteDataOffset); // TODO: work out wtf this is

        /*
        if (id == 0) {
            var timelineListSize = 0U;
            foreach (var a in uld.ATK) {
                if (a?.Widgets != null) {
                    foreach (var w in a.Widgets.Elements) {
                        timelineListSize += CountNodesWithTimeline(uld, w.RootNode);
                    }
                }
                
            }
            
            bytes.Write(timelineListSize);
        } else {
            bytes.Write((uint) 0);
        }
        */
        bytes.Write(NodesWithTimeline); // TODO: fix this
        
        
        bytes.Write(data);
        
        Logging.Unindent();
        
        return bytes.ToArray();
    }

    private uint CountNodesWithTimeline(Uld uld, ResNode? rootNode) {
        if (rootNode == null) return 0U;
        var n = 0U;
        
        n += CountNodesWithTimeline(uld, rootNode.Child);
        n += CountNodesWithTimeline(uld, rootNode.NextSibling);

        
        if (rootNode is BaseComponentNode cNode) {
            var component = uld.GetComponent(cNode.Type);
            if (component != null) n += CountNodesWithTimeline(uld, component.RootNode);
        } 
        
        if (rootNode.TimelineId > 0) n++;

        return n;
    }
    
    
}
