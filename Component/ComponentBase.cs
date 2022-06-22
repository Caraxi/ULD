using ULD.Node;

namespace ULD.Component;

public class ComponentBase : IEncodeable {

    protected virtual int DataCount => 0;
    
    public uint Id;
    public bool ShouldIgnoreInput;
    public bool DragArrow;
    public bool DropArrow;
    public ComponentType Type;

    public List<ResNode> Nodes = new();

    public virtual long Size => 16 + DataCount * 4;
    public long TotalSize => Size + Nodes.Sum(n => n.Size);
    

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
            writer.Write(node.Encode());
        }

        if (writer.Length != TotalSize) {
            throw new Exception("Total Size does not match expected value. Expected: " + TotalSize + ", Actual: " + writer.Length);
        }
        
        return writer.ToArray();
    }

    protected virtual void DecodeData(ULD baseUld, BufferReader br) {
        
    }
    
    public void Decode(ULD baseUld, BufferReader br) {
        Logging.IndentLog($"Decoding {GetType().Name} @ {br.BaseStream.Position}");
        var pos = br.BaseStream.Position;
        Id = br.ReadUInt32();
        Logging.Log($" - ID: {Id}");
        ShouldIgnoreInput = br.ReadBoolean();
        DragArrow = br.ReadBoolean();
        DropArrow = br.ReadBoolean();
        Type = (ComponentType)br.ReadByte();
        Logging.Log($" - Type: {Type}");
        var nodeCount = br.ReadUInt32();

        var totalSize =  br.ReadUInt16();
        var dataSize = br.ReadUInt16();

        if (dataSize != Size) throw new Exception($"{GetType().Name} Size does not match the expected value. Expected: {Size}, Actual: {dataSize}");
        
        Data = new uint[DataCount];
        for (var i = 0; i < DataCount; i++) {
            Data[i] = br.ReadUInt32();
        }
        
        DecodeData(baseUld, br);

        br.Seek(pos + dataSize); // We should already be here, but add some safety I guess
        
        
        for (var i = 0; i < nodeCount; i++) {
            Nodes.Add(ResNode.ReadNode(baseUld, br));
        }

        if (totalSize != TotalSize) throw new Exception($"{GetType().Name} Total Size does not match the expected value. Expected: {TotalSize}, Actual: {totalSize}");
        
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