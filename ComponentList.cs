using ULD.Component;

namespace ULD;

public class ComponentList : ListHeader<ComponentBase> {
    
    public ComponentList() : base("cohd") { }
    public ComponentList(Uld baseUld, BufferReader r) : base(baseUld, r, "cohd") {
        
    }
    
    protected override string HeaderType => "cohd";

    protected override long NextOffset(ComponentBase element) {
        return element.GetTotalSize(Version);
    }
    
    

    protected override ComponentBase CreateElementObject(Uld baseUld, BufferReader r) {
        var type = (ComponentType) r.PeekByte(7);
        return ComponentBase.Create(type);
    }

    protected override void AfterDecode(Uld baseUld, BufferReader r) {
        // Allow loading full component list before loading their respective node lists.
        r.Push();
        foreach (var c in Elements) {
            c.DecodeNodeList(baseUld, r, Version);
        }
        r.Pop();
        
    }
}