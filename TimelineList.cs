using ULD.Timelines;

namespace ULD;

public class TimelineList : ListHeader<Timeline> {
    public TimelineList() : base("tlhd") { }
    public TimelineList(Uld baseUld, BufferReader r) : base(baseUld, r, "tlhd") { }

    protected override string HeaderType => "tlhd";
    
}
