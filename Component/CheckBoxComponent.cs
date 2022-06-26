using ULD.Node;

namespace ULD.Component;

public class CheckBoxComponent : ButtonComponent {

    public uint unkNodeId;
    
    public override long GetSize(string version) => base.GetSize(version) + 4;


    protected override void DecodeData(ULD baseUld, BufferReader br) {
        base.DecodeData(baseUld, br);
        unkNodeId = br.ReadUInt32();
    }

    protected override void DecodeNodeList(ULD baseUld, BufferReader reader, string version, List<ResNode> nodes) {
        base.DecodeNodeList(baseUld, reader, version, nodes);
        
    }

    protected override void EncodeData(BufferWriter writer) {
        base.EncodeData(writer);
        writer.Write(unkNodeId);
    }


}