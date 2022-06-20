namespace ULD;

public class WidgetList : ListHeader<Widget> {
    public WidgetList(ULD baseUld, BufferReader r) : base(baseUld, r, "wdhd") { }
    protected override string HeaderType => "wdhd";

    protected override long NextOffset(Widget element) => element.Offset;
}