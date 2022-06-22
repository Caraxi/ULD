using ULD.Node;

namespace ULD.Component;

public class ComponentBase : IEncodable {

    private bool nodeListDecoded = true;
    private uint encodedNodeListCount = 0;
    private ushort encodedTotalSize = 0;
    private long encodedNodeListSize = 0;
    private long encodedNodeListPosition = 0;
    
    
    protected virtual int DataCount => 0;
    
    public uint Id;
    public bool ShouldIgnoreInput;
    public bool DragArrow;
    public bool DropArrow;
    public ComponentType Type;

    public List<ResNode> Nodes = new();

    public virtual long Size => 16 + DataCount * 4;
    public long TotalSize => Size + (nodeListDecoded ? Nodes.Sum(n => n.Size) : encodedNodeListSize);
    

    private uint[]? data;
    public uint[] Data {
        get => data ??= new uint[DataCount];
        set {
            if (value.Length != DataCount) throw new Exception($"Incorrect array size. Expected {DataCount}");
            data = value;
        }
    }
    
    
    protected virtual void EncodeData(BufferWriter writer) {}
    
    public byte[] Encode() {
        var writer = new BufferWriter();
        
        writer.Write(Id);
        writer.Write(ShouldIgnoreInput);
        writer.Write(DragArrow);
        writer.Write(DropArrow);
        writer.Write((byte) Type);
        writer.Write((uint)Nodes.Count);
        
        if (Size > ushort.MaxValue) throw new Exception("Component is too large");
        if (TotalSize > ushort.MaxValue) throw new Exception("Component's Node List is too large");
        
        writer.Write((ushort)TotalSize);
        writer.Write((ushort)Size);
        foreach(var d in Data) writer.Write(d);
        EncodeData(writer);
        
        foreach(var node in Nodes) {
            var encodedNode = node.Encode();
            if (encodedNode.Length != node.Size) throw new Exception($"{node.GetType().Name} does not match expected size of {node.Size}. Actual size: {encodedNode.Length}");
            writer.Write(node.Encode());
        }

        if (writer.Length != TotalSize) {
            throw new Exception($"{GetType().Name} does not match expected size. Expected: " + TotalSize + ", Actual: " + writer.Length);
        }
        
        return writer.ToArray();
    }

    protected virtual void DecodeData(ULD baseUld, BufferReader br) {
        
    }
    
    public void Decode(ULD baseUld, BufferReader br) {
        nodeListDecoded = false;
        Logging.IndentLog($"Decoding {GetType().Name} @ {br.BaseStream.Position}");
        var pos = br.BaseStream.Position;
        Id = br.ReadUInt32();
        Logging.Log($" - ID: {Id}");
        ShouldIgnoreInput = br.ReadBoolean();
        DragArrow = br.ReadBoolean();
        DropArrow = br.ReadBoolean();
        Type = (ComponentType)br.ReadByte();
        Logging.Log($" - Type: {Type}");
        encodedNodeListCount = br.ReadUInt32();
        Logging.Log($" - Node Count: {encodedNodeListCount}");

        encodedTotalSize = br.ReadUInt16();
        Logging.Log($" - Total Size: {encodedTotalSize}");
        var dataSize = br.ReadUInt16();
        Logging.Log($" - Data Size: {dataSize}");
        encodedNodeListSize = encodedTotalSize - dataSize;

        if (dataSize != Size) throw new Exception($"{GetType().Name} Size does not match the expected value. Expected: {Size}, Actual: {dataSize}");
        
        Data = new uint[DataCount];
        for (var i = 0; i < DataCount; i++) {
            Data[i] = br.ReadUInt32();
        }
        
        DecodeData(baseUld, br);

        encodedNodeListPosition = br.BaseStream.Position;
        
        Logging.Unindent();
    }

    public void DecodeNodeList(ULD baseUld, BufferReader reader) {
        if (nodeListDecoded) return;
        
        Logging.IndentLog($"Decoding Node List for {GetType().Name}#{Id} - {encodedNodeListCount} Nodes");
        
        reader.Seek(encodedNodeListPosition);
        
        for (var i = 0; i < encodedNodeListCount; i++) {
            Nodes.Add(ResNode.ReadNode(baseUld, reader));
        }
        nodeListDecoded = true;
        if (encodedTotalSize != TotalSize) throw new Exception($"{GetType().Name} Total Size does not match the expected value. Expected: {TotalSize}, Actual: {encodedTotalSize}");
        Logging.Unindent();
    }


    public static ComponentBase Create(ComponentType type) {
        return type switch {
            ComponentType.Base => new ComponentBase(),
            ComponentType.Button => new ButtonComponent(),
            ComponentType.Window => new WindowComponent(),
            ComponentType.CheckBox => new CheckBoxComponent(),
            ComponentType.RadioButton => new RadioButtonComponent(),
            ComponentType.GaugeBar => new GaugeBarComponent(),
            ComponentType.Slider => new SliderComponent(),
            ComponentType.TextInput => new TextInputComponent(),
            ComponentType.NumericInput => new NumericInputComponent(),
            ComponentType.List => new ListComponent(),
            ComponentType.DropDownList => new DropDownListComponent(),
            ComponentType.Tab => new TabComponent(),
            ComponentType.TreeList => new TreeListComponent(),
            ComponentType.ScrollBar => new ScrollBarComponent(),
            ComponentType.ListItemRenderer => new ListItemRendererComponent(),
            ComponentType.Icon => new IconComponent(),
            ComponentType.IconText => new IconTextComponent(),
            ComponentType.DragDrop => new DragDropComponent(),
            ComponentType.GuildLeveCard => new GuildLeveCardComponent(),
            ComponentType.TextNineGrid => new TextNineGridComponent(),
            ComponentType.JournalCanvas => new JournalCanvasComponent(),
            ComponentType.Multipurpose => new MultipurposeComponent(),
            ComponentType.Map => new MapComponent(),
            ComponentType.Preview => new PreviewComponent(),
            ComponentType.HoldButton => new HoldButtonComponent(),
            _ => throw new Exception($"Component Type {type} is not supported.")
        };
    }
}