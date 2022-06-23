namespace ULD;

public class PartList : ListHeader<Parts> {
    public PartList(ULD baseUld, BufferReader r) : base(baseUld, r, "tphd") {
        
    }
    
    protected override string HeaderType => "tphd";
}
