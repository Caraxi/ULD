namespace ULD;

public class WidgetList : ListHeader<Widget> {
    public WidgetList() : base("wdhd") { }
    public WidgetList(ULD baseUld, BufferReader r) : base(baseUld, r, "wdhd") { }
    protected override string HeaderType => "wdhd";
}
