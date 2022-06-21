using ULD.Component;

namespace ULD;

public class ComponentList : ListHeader<Component.ComponentBase> {
    public ComponentList(ULD baseUld, BufferReader r) : base(baseUld, r, "cohd") {
        
    }
    
    protected override string HeaderType => "cohd";

    protected override long NextOffset(Component.ComponentBase element) {
        return element.TotalSize;
    }

    protected override ComponentBase CreateElementObject(ULD baseUld, BufferReader r) {
        var type = (ComponentType) r.PeekByte(7);
        return ComponentBase.Create(type);
    }
}