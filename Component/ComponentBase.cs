using ULD.Node;

namespace ULD.Component;

public enum ComponentType : byte
{
    Base = 0,
    Button = 1,
    Window = 2,
    CheckBox = 3,
    RadioButton = 4,
    GaugeBar = 5,
    Slider = 6,
    TextInput = 7,
    NumericInput = 8,
    List = 9,
    DropDownList = 10,
    Tab = 11,
    TreeList = 12,
    ScrollBar = 13,
    ListItemRenderer = 14,
    Icon = 15,
    IconText = 16,
    DragDrop = 17,
    GuildLeveCard = 18,
    TextNineGrid = 19,
    JournalCanvas = 20,
    Multipurpose = 21,
    Map = 22,
    Preview = 23,
    HoldButton = 24
}

public class ComponentBase : IEncodeable {

    public uint Id;
    public bool ShouldIgnoreInput;
    public bool DragArrow;
    public bool DropArrow;
    public ComponentType Type;

    public List<ResNode> Nodes = new();

    protected virtual long Size => 16;
    public long TotalSize => Size + Nodes.Sum(n => n.Size);
    
    protected virtual void EncodeData(BufferWriter writer) {}
    
    public byte[] Encode() {
        var data = new BufferWriter();
        
        data.Write(Id);
        data.Write(ShouldIgnoreInput);
        data.Write(DragArrow);
        data.Write(DropArrow);
        data.Write((byte) Type);
        data.Write((uint)Nodes.Count);
        
        if (Size > ushort.MaxValue) throw new Exception("Component is too large");
        if (TotalSize > ushort.MaxValue) throw new Exception("Component's Node List is too large");
        
        data.Write((ushort)TotalSize);
        data.Write((ushort)Size);
        
        EncodeData(data);
        
        foreach(var node in Nodes) {
            data.Write(node.Encode());
        }

        if (data.Length != TotalSize) {
            throw new Exception("Total Size does not match expected value. Expected: " + TotalSize + ", Actual: " + data.Length);
        }
        
        return data.ToArray();
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
            ComponentType.Button => new ComponentButton(),
            ComponentType.Window => new ComponentWindow(),
            // ComponentType.CheckBox => expr,
            // ComponentType.RadioButton => expr,
            // ComponentType.GaugeBar => expr,
            // ComponentType.Slider => expr,
            // ComponentType.TextInput => expr,
            // ComponentType.NumericInput => expr,
            // ComponentType.List => expr,
            // ComponentType.DropDownList => expr,
            // ComponentType.Tab => expr,
            // ComponentType.TreeList => expr,
            ComponentType.ScrollBar => new ComponentScrollBar(),
            // ComponentType.ListItemRenderer => expr,
            // ComponentType.Icon => expr,
            // ComponentType.IconText => expr,
            // ComponentType.DragDrop => expr,
            // ComponentType.GuildLeveCard => expr,
            // ComponentType.TextNineGrid => expr,
            // ComponentType.JournalCanvas => expr,
            // ComponentType.Multipurpose => expr,
            // ComponentType.Map => expr,
            // ComponentType.Preview => expr,
            // ComponentType.HoldButton => expr,
            _ => throw new Exception($"Component Type {type} is not supported.")
        };
    }
}