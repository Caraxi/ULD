using ULD.Node;

namespace ULD.Component;

public class ButtonComponent : ComponentBase {
    
    private uint buttonContentNodeId;
    private uint buttonBackgroundImageNodeId;

    public ResNode? ButtonContentNode { get; set; }
    public ResNode? ButtonBackgroundImageNode { get; set; }
    
    public override long GetSize(string version) => base.GetSize(version) + 8;


    protected override void DecodeData(Uld baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        buttonContentNodeId = br.ReadUInt32();
        buttonBackgroundImageNodeId = br.ReadUInt32();
    }

    protected override void DecodeNodeList(Uld baseUld, BufferReader reader, string version, List<ResNode> nodes) {
        ButtonContentNode = buttonContentNodeId == 0 ? null : nodes.Find(n => n.Id == buttonContentNodeId);
        ButtonBackgroundImageNode = buttonBackgroundImageNodeId == 0 ? null : nodes.Find(n => n.Id == buttonBackgroundImageNodeId);
    }

    protected override void EncodeData(BufferWriter writer) {
        base.EncodeData(writer);
        writer.Write(ButtonContentNode?.Id ?? 0U);
        writer.Write(ButtonBackgroundImageNode?.Id ?? 0U);
    }
    
    
}
