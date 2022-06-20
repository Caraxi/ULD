using ULD.Keyframe;

namespace ULD;

public class KeyGroup : IEncodeable {
    
    public enum KeyGroupType : ushort
    {
        Float1 = 0x0,
        Float2 = 0x1,
        Float3 = 0x2,
        SByte1 = 0x3,
        SByte2 = 0x4,
        SByte3 = 0x5,
        Byte1 = 0x6,
        Byte2 = 0x7,
        Byte3 = 0x8,
        Short1 = 0x9,
        Short2 = 0xA,
        Short3 = 0xB,
        UShort1 = 0xC,
        UShort2 = 0xD,
        UShort3 = 0xE,
        Int1 = 0xF,
        Int2 = 0x10,
        Int3 = 0x11,
        UInt1 = 0x12,
        UInt2 = 0x13,
        UInt3 = 0x14,
        Bool1 = 0x15,
        Bool2 = 0x16,
        Bool3 = 0x17,
        Color = 0x18,
        Label = 0x19,
        Number = 0x1A,
    }

    public enum KeyUsage : ushort
    {
        Position = 0x0,
        Rotation = 0x1,
        Scale = 0x2,
        Alpha = 0x3,
        NodeColor = 0x4,
        TextColor = 0x5,
        EdgeColor = 0x6,
        Number = 0x7,
    }

    public KeyUsage Usage;
    public KeyGroupType Type;
    public ushort Offset;
    public List<KeyframeBase> Keyframes;


    protected ULD BaseULD;

    public byte[] Encode() {
        var b = new BufferWriter();
        b.Write((ushort) Usage);
        b.Write((ushort) Type);
        b.Write(Offset);
        b.Write((ushort) Keyframes.Count);
        foreach(var kf in Keyframes) b.Write(kf.Encode());
        return b.ToArray();
    }

    private List<KeyframeBase> DecodeKeyframeData<T>(BufferReader r, ushort count) where T : KeyframeBase, new() {
        var keyframes = new List<KeyframeBase>();
        for (var i = 0; i < count; i++) {
            var kf = new T();
            kf.Decode(BaseULD, r);
            keyframes.Add(kf);
        }

        return keyframes;
    }
    
    public void Decode(ULD baseUld, BufferReader reader) {
        BaseULD = baseUld;
        Usage = (KeyUsage)reader.ReadUInt16();
        Type = (KeyGroupType)reader.ReadUInt16();
        Offset = reader.ReadUInt16();
        var keyframeCount = reader.ReadUInt16();
        Keyframes = Type switch {
            KeyGroupType.Float1 => DecodeKeyframeData<Float1Keyframe>(reader, keyframeCount),
            KeyGroupType.Float2 => DecodeKeyframeData<Float2Keyframe>(reader, keyframeCount),
            KeyGroupType.Float3 => DecodeKeyframeData<Float3Keyframe>(reader, keyframeCount),
            KeyGroupType.SByte1 => DecodeKeyframeData<SByte1Keyframe>(reader, keyframeCount),
            KeyGroupType.SByte2 => DecodeKeyframeData<SByte2Keyframe>(reader, keyframeCount),
            KeyGroupType.SByte3 => DecodeKeyframeData<SByte3Keyframe>(reader, keyframeCount),
            KeyGroupType.Byte1 => DecodeKeyframeData<Byte1Keyframe>(reader, keyframeCount),
            KeyGroupType.Byte2 => DecodeKeyframeData<Byte2Keyframe>(reader, keyframeCount),
            KeyGroupType.Byte3 => DecodeKeyframeData<Byte3Keyframe>(reader, keyframeCount),
            KeyGroupType.Short1 => DecodeKeyframeData<Short1Keyframe>(reader, keyframeCount),
            KeyGroupType.Short2 => DecodeKeyframeData<Short2Keyframe>(reader, keyframeCount),
            KeyGroupType.Short3 => DecodeKeyframeData<Short3Keyframe>(reader, keyframeCount),
            KeyGroupType.UShort1 => DecodeKeyframeData<UShort1Keyframe>(reader, keyframeCount),
            KeyGroupType.UShort2 => DecodeKeyframeData<UShort2Keyframe>(reader, keyframeCount),
            KeyGroupType.UShort3 => DecodeKeyframeData<UShort3Keyframe>(reader, keyframeCount),
            KeyGroupType.Int1 => DecodeKeyframeData<Int1Keyframe>(reader, keyframeCount),
            KeyGroupType.Int2 => DecodeKeyframeData<Int2Keyframe>(reader, keyframeCount),
            KeyGroupType.Int3 => DecodeKeyframeData<Int3Keyframe>(reader, keyframeCount),
            KeyGroupType.UInt1 => DecodeKeyframeData<UInt1Keyframe>(reader, keyframeCount),
            KeyGroupType.UInt2 => DecodeKeyframeData<UInt2Keyframe>(reader, keyframeCount),
            KeyGroupType.UInt3 => DecodeKeyframeData<UInt3Keyframe>(reader, keyframeCount),
            KeyGroupType.Bool1 => DecodeKeyframeData<Bool1Keyframe>(reader, keyframeCount),
            KeyGroupType.Bool2 => DecodeKeyframeData<Bool2Keyframe>(reader, keyframeCount),
            KeyGroupType.Bool3 => DecodeKeyframeData<Bool3Keyframe>(reader, keyframeCount),
            KeyGroupType.Color => DecodeKeyframeData<ColorKeyframe>(reader, keyframeCount),
            KeyGroupType.Label => DecodeKeyframeData<LabelKeyframe>(reader, keyframeCount),
            _ => throw new Exception($"Unhandled Key Group Type: {Type}")
        };
    }
}

public class Frame : IEncodeable {

    public uint StartFrame;
    public uint EndFrame;
    public uint Offset;
    public List<KeyGroup> KeyGroups = new();

    public byte[] Encode() {
        var b = new BufferWriter();
        b.Write(StartFrame);
        b.Write(EndFrame);
        b.Write(Offset);
        b.Write((uint) KeyGroups.Count);
        foreach (var k in KeyGroups) {
            b.Write(k.Encode());
        }
        return b.ToArray();
    }

    public void Decode(ULD baseUld, BufferReader reader) {
        StartFrame = reader.ReadUInt32();
        EndFrame = reader.ReadUInt32();
        Offset = reader.ReadUInt32();
        var count = reader.ReadUInt32();
        KeyGroups = new List<KeyGroup>();
        for (var i = 0; i < count; i++) {
            var k = new KeyGroup();
            k.Decode(baseUld, reader);
            KeyGroups.Add(k);
        }
    }
}

public class Timeline : IEncodeable {

    public uint Id;
    public ushort Offset;
    
    public List<Frame> FrameSet0 = new();
    public List<Frame> FrameSet1 = new();
    
    public byte[] Encode() {
        var data = new BufferWriter();
        data.Write(Id);
        data.Write(Offset);
        data.Write((ushort) FrameSet0.Count);
        data.Write((ushort) FrameSet1.Count);
        foreach (var f in FrameSet0) data.Write(f.Encode());
        foreach(var f in FrameSet1) data.Write(f.Encode());
        return data.ToArray();
    }

    public void Decode(ULD baseUld, BufferReader reader) {
        Id = reader.ReadUInt32();
        
        Offset = reader.ReadUInt16();
        reader.ReadUInt16();

        var fc0 = reader.ReadUInt16();
        var fc1 = reader.ReadUInt16();

        FrameSet0 = new List<Frame>();
        FrameSet1 = new List<Frame>();
        for (var i = 0; i < fc0; i++) {
            var frame = new Frame();
            //frame.Decode(reader);
            FrameSet0.Add(frame);
        }
        for (var i = 0; i < fc1; i++) {
            var frame = new Frame();
            //frame.Decode(reader);
            FrameSet1.Add(frame);
        }
    }
}
