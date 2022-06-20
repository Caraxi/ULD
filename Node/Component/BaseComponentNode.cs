namespace ULD.Node.Component; 

public class BaseComponentNode : ResNode {
    
    public byte Index;
    public byte Up;
    public byte Down;
    public byte Left;
    public byte Right;
    public byte Cursor;
    
    public bool RepeatUp;
    public bool RepeatDown;
    public bool RepeatLeft;
    public bool RepeatRight;
    
    public byte Unk3;

    public byte Unk4;
    public short OffsetX;
    public short OffsetY;

    protected override ushort Size => (ushort) (base.Size + 12);

    public override void Decode(ULD baseUld, BufferReader reader) {
        base.Decode(baseUld, reader);
        Index = reader.ReadByte();
        Up = reader.ReadByte();
        Down = reader.ReadByte();
        Left = reader.ReadByte();
        Right = reader.ReadByte();
        Cursor = reader.ReadByte();

        var f = reader.ReadByte();
        RepeatUp = (f & 0x80) == 0x80;
        RepeatDown = (f & 0x40) == 0x40;
        RepeatLeft = (f & 0x20) == 0x20;
        RepeatRight = (f & 0x10) == 0x10;
        Unk3 = (byte)(f & 0x0F);
        
        Unk4 = reader.ReadByte();
        OffsetX = reader.ReadInt16();
        OffsetY = reader.ReadInt16();
    }

    public override byte[] Encode() {
        var b = new BufferWriter();
        b.Write(base.Encode());
        b.Write(Index);
        b.Write(Up);
        b.Write(Down);
        b.Write(Left);
        b.Write(Right);
        b.Write(Cursor);
        byte f = 0;
        if (RepeatUp) f |= 0x80;
        if (RepeatDown) f |= 0x40;
        if (RepeatLeft) f |= 0x20;
        if (RepeatRight) f |= 0x10;
        f |= (byte)(0x0F & Unk3);
        b.Write(f);
        b.Write(Unk4);
        b.Write(OffsetX);
        b.Write(OffsetY);
        return b;
    }
    
    public static BaseComponentNode Create(ULD baseUld, uint componentId) {
        var component = baseUld.GetComponent(componentId);
        if (component == null) throw new Exception($"Component#{componentId} is not defined in ULD, but was referenced in a component node.");
        return component.Type switch {
            ComponentType.Base => new BaseComponentNode(),
            ComponentType.Button => new ButtonComponentNode(),
            ComponentType.Window => new WindowComponentNode(),
            ComponentType.CheckBox => new CheckBoxComponentNode(),
            ComponentType.RadioButton => new RadioButtonComponentNode(),
            ComponentType.GaugeBar => new GaugeComponentNode(),
            ComponentType.Slider => new SliderComponentNode(),
            ComponentType.TextInput => new TextInputComponentNode(),
            ComponentType.NumericInput => new NumericInputComponentNode(),
            ComponentType.List => new ListComponentNode(),
            ComponentType.DropDownList => new DropDownListComponentNode(),
            ComponentType.Tab => new TabComponentNode(),
            ComponentType.TreeList => new TreeListComponentNode(),
            ComponentType.ScrollBar => new ScrollBarComponentNode(),
            ComponentType.ListItemRenderer => new ListItemComponentNode(),
            ComponentType.Icon => new IconComponentNode(),
            ComponentType.IconText => new IconTextComponentNode(),
            ComponentType.DragDrop => new DragDropComponentNode(),
            ComponentType.GuildLeveCard => new GuildLeveCardComponentNode(),
            ComponentType.TextNineGrid => new TextNineGridComponentNode(),
            ComponentType.JournalCanvas => new JournalCanvasComponentNode(),
            ComponentType.Multipurpose => new MultipurposeComponentNode(),
            ComponentType.Map => new MapComponentNode(),
            ComponentType.Preview => new PreviewComponentNode(),
            
            _ => throw new Exception($"Component Type {component.Type} is not supported.")
        };
    }
    
}

public class PreviewComponentNode : BaseComponentNode { }

public class MapComponentNode : BaseComponentNode { }

public class MultipurposeComponentNode : BaseComponentNode { }

public class JournalCanvasComponentNode : BaseComponentNode { }

public class GuildLeveCardComponentNode : BaseComponentNode { }

public class DragDropComponentNode : BaseComponentNode { }

public class IconTextComponentNode : BaseComponentNode { }

public class IconComponentNode : BaseComponentNode { }

public class TreeListComponentNode : BaseComponentNode { }

public class DropDownListComponentNode : BaseComponentNode { }

public class ScrollBarComponentNode : BaseComponentNode { }

public class TextNineGridComponentNode : BaseComponentNode { }

public class ListItemComponentNode : BaseComponentNode { }

public class TabComponentNode : BaseComponentNode { }

public class ListComponentNode : BaseComponentNode { }

public class NumericInputComponentNode : BaseComponentNode { }

public class TextInputComponentNode : BaseComponentNode { }

public class SliderComponentNode : BaseComponentNode { }

public class GaugeComponentNode : BaseComponentNode { }

public class RadioButtonComponentNode : BaseComponentNode { }

public class CheckBoxComponentNode : BaseComponentNode { }

public class ButtonComponentNode : BaseComponentNode { }
