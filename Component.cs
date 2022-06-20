using ULD.Node;

namespace ULD;

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

public class Component : IEncodeable {

    public uint Id;
    public bool ShouldIgnoreInput;
    public bool DragArrow;
    public bool DropArrow;
    public ComponentType Type;
    
    public ushort Offset; // TODO: Calculate this
    public ushort NodeListOffset; // TODO: Calculate this

    public List<ResNode> Nodes = new();
    
    
    public byte[] Encode() {
        var data = new BufferWriter();
        
        data.Write(Id);
        data.Write(ShouldIgnoreInput);
        data.Write(DragArrow);
        data.Write(DropArrow);
        data.Write((byte) Type);
        data.Write((uint)Nodes.Count);
        
        data.Write(Offset); // Probably calculate these
        data.Write(NodeListOffset);

        return data.ToArray();
    }

    public void Decode(ULD baseUld, BufferReader br) {
        var pos = br.BaseStream.Position;
        Id = br.ReadUInt32();
        ShouldIgnoreInput = br.ReadBoolean();
        DragArrow = br.ReadBoolean();
        DropArrow = br.ReadBoolean();
        Type = (ComponentType)br.ReadByte();
        var nodeCount = br.ReadUInt32();

        Offset =  br.ReadUInt16();
        NodeListOffset = br.ReadUInt16();
        br.Seek(pos + NodeListOffset);
    }
}
