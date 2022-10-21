using ULD.Timelines.Keyframe;

namespace ULD.Timelines;

public class KeyGroup : IEncodable {
    
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
    public List<KeyframeBase> Keyframes = new();

    public long Size => 8 + Keyframes.Sum(kf => kf.Size);
    
    
    protected Uld BaseULD;

    public byte[] Encode() {
        var b = new BufferWriter();
        b.Write((ushort) Usage);
        b.Write((ushort) Type);
        if (Size > ushort.MaxValue) throw new Exception("KeyGroup is too large to encode");
        b.Write((ushort) Size);
        b.Write((ushort) Keyframes.Count);
        foreach(var kf in Keyframes) b.Write(kf.Encode());
        return b.ToArray();
    }

    private List<KeyframeBase> DecodeKeyframeData<T>(BufferReader r, ushort count) where T : KeyframeBase, new() {
        Logging.IndentLog($"Decoding {count}x {typeof(T).Name}");
        var keyframes = new List<KeyframeBase>();
        for (var i = 0; i < count; i++) {
            var p = r.BaseStream.Position;
            var kf = new T();
            kf.Decode(BaseULD, r);
            keyframes.Add(kf);
            r.Seek(p + kf.Size);
        }

        Logging.Unindent();
        return keyframes;
    }
    
    public void Decode(Uld baseUld, BufferReader reader) {
        Logging.IndentLog("Decoding KeyGroup");
        BaseULD = baseUld;
        Usage = (KeyUsage)reader.ReadUInt16();
        Type = (KeyGroupType)reader.ReadUInt16();
        var size = reader.ReadUInt16();
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
        
        if (size != Size) throw new Exception("KeyGroup size value mismatch. Expected: " + Size + ", Actual: " + size);
        
        Logging.Unindent();
        
    }
}
