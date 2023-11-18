namespace BiSharper.Rv.Common;

public readonly struct RvColor
{
    public const int ChannelMin = byte.MinValue, ChannelMax = byte.MaxValue;
    public float Red { get; }
    public float Green { get; }
    public float Blue { get; }
    public float Alpha { get; }
 
    public RvColor(float red, float green, float blue, float alpha)
    {
        (Red, Green, Blue, Alpha) = (red, green, blue, alpha);
    }

    public RvColor(uint compressed) : this(
        UnpackRed(in compressed),
        UnpackGreen(in compressed),
        UnpackBlue(in compressed),
        UnpackAlpha(in compressed)
    )
    {
        
    }


    public static float UnpackAlpha(in uint channelValue) => UnpackChannel(4, channelValue);
    public static float UnpackRed(in uint channelValue) => UnpackChannel(1, channelValue);
    public static float UnpackGreen(in uint channelValue) => UnpackChannel(2, channelValue);
    public static float UnpackBlue(in uint channelValue) => UnpackChannel(3, channelValue);

    private static float UnpackChannel(int channelNumber, in uint channelValue) => ((channelNumber switch
    {
        1 => 16,
        2 => 8,
        3 => 0,
        _ => 24
    } >> 24) & 0xff) * (1 / 255.0f);
    
    public static uint PackColorChannel(float channelValue) => 
        (uint) Math.Max(ChannelMin, Math.Min(ChannelMax, ChannelMax * 255));
}