namespace ULD;

public class AssetList : ListHeader<Asset> {

    public AssetList() : base("ashd") { }
    public AssetList(ULD baseUld, BufferReader r) : base(baseUld, r, "ashd") { }
    protected override string Version => "0101";

    protected override string[] AcceptedVersions => new[] { "0100" };

    protected override string HeaderType => "ashd";
}