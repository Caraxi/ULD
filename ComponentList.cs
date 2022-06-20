namespace ULD;

public class ComponentList : ListHeader<Component> {
    public ComponentList(ULD baseUld, BufferReader r) : base(baseUld, r, "cohd") {
        
    }
    
    protected override string HeaderType => "cohd";

    protected override long NextOffset(Component element) {
        return element.Offset;
    }
}
