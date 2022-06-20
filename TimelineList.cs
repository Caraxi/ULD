namespace ULD;

public class TimelineList : ListHeader<Timeline> {
    public TimelineList(ULD baseUld, BufferReader r) : base(baseUld, r, "tlhd") { }

    protected override string HeaderType => "tlhd";

    protected override long NextOffset(Timeline element) {
        return element.Offset;
    }
}