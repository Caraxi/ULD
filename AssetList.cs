namespace ULD;

public class AssetList : ListHeader<Asset> {

    public AssetList() : base("ashd") { }
    public AssetList(ULD baseUld, BufferReader r) : base(baseUld, r, "ashd") { }
    
    protected override string[] AcceptedVersions => new[] { "0100", "0101" };

    protected override string HeaderType => "ashd";
}