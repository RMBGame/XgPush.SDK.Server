using XgPush.SDK.Server.BaseTypes;
using XgPush.SDK.Server.Internal;

namespace XgPush.SDK.Server
{
    /// <summary>
    ///
    /// </summary>
    public class Style : IsValid
    {
        /// <summary>
        /// 本地通知样式标识。
        /// </summary>
        public int BuilderId { get; set; }

        /// <summary>
        /// 是否有铃声。
        /// <para>0, 没有铃声</para>
        /// <para>1, 有铃声</para>
        /// </summary>
        public DigitBoolean Ring { get; set; }

        /// <summary>
        /// 是否使用震动。
        /// <para>0, 没有震动</para>
        /// <para>1, 有震动</para>
        /// </summary>
        public DigitBoolean Vibrate { get; set; }

        /// <summary>
        /// 通知栏是否可清除。
        /// </summary>
        public DigitBoolean Clearable { get; set; }

        /// <summary>
        /// 通知消息对象的唯一标识。
        /// <para>1.大于0，会覆盖先前相同id的消息；</para>
        /// <para>2.等于0，展示本条通知且不影响其他消息；</para>
        /// <para>3.等于-1，将清除先前所有消息，仅展示本条消息</para>
        /// </summary>
        public int NId { get; set; }

        /// <summary>
        /// 是否使用呼吸灯。
        /// <para>0, 使用呼吸灯</para>
        /// <para>1, 不使用呼吸灯</para>
        /// </summary>
        public DigitBoolean Lights { get; set; }

        /// <summary>
        /// 通知栏图标是应用内图标还是上传图标。
        /// <para>0，应用内图标</para>
        /// <para>1，上传图标</para>
        /// </summary>
        public DigitBoolean IconType { get; set; }

        /// <summary>
        /// 设置是否覆盖指定编号的通知样式。
        /// </summary>
        public DigitBoolean StyleId { get; set; }

        /// <summary>
        /// 指定Android工程里raw目录中的铃声文件名，不需要后缀名。
        /// </summary>
        public string RingRaw { get; set; }

        /// <summary>
        /// 应用内图标文件名或者下载图标的url地址。
        /// </summary>
        public string IconRes { get; set; }

        /// <summary>
        /// 消息在状态栏显示的图标，若不设置，则显示应用图标。
        /// </summary>
        public string SmallIcon { get; set; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return true;
        }

        /// <summary>
        ///
        /// </summary>
        public Style() { }

        /// <summary>
        ///
        /// </summary>
        /// <param name="builderId"></param>
        public Style(int builderId)
        {
            BuilderId = builderId;
            Ring = 0;
            Vibrate = 0;
            Clearable = 1;
            NId = 0;
            Lights = 1;
            IconType = 0;
            StyleId = 1;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="builderId"></param>
        /// <param name="ring"></param>
        /// <param name="vibrate"></param>
        /// <param name="clearable"></param>
        /// <param name="nId"></param>
        public Style(int builderId, DigitBoolean ring, DigitBoolean vibrate,
            DigitBoolean clearable, int nId)
        {
            BuilderId = builderId;
            Ring = ring;
            Vibrate = vibrate;
            Clearable = clearable;
            NId = nId;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="builderId"></param>
        /// <param name="ring"></param>
        /// <param name="vibrate"></param>
        /// <param name="clearable"></param>
        /// <param name="nId"></param>
        /// <param name="lights"></param>
        /// <param name="iconType"></param>
        /// <param name="styleId"></param>
        public Style(int builderId, DigitBoolean ring, DigitBoolean vibrate, DigitBoolean clearable,
            int nId, DigitBoolean lights, DigitBoolean iconType, DigitBoolean styleId)
            : this(builderId, ring, vibrate, clearable, nId)
        {
            Lights = lights;
            IconType = iconType;
            StyleId = styleId;
        }
    }
}

namespace XgPush.SDK.Server.Compat
{
#pragma warning disable IDE1006 // 命名样式

    public static partial class XgPush_Server_SDK_Compat_Extensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public static int getBuilderId(this Style style)
        {
            return style.BuilderId;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public static int getRing(this Style style)
        {
            return style.Ring;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public static int getVibrate(this Style style)
        {
            return style.Vibrate;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public static int getClearable(this Style style)
        {
            return style.Clearable;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public static int getNId(this Style style)
        {
            return style.NId;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public static int getLights(this Style style)
        {
            return style.Lights;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public static int getIconType(this Style style)
        {
            return style.IconType;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public static int getStyleId(this Style style)
        {
            return style.StyleId;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public static string getRingRaw(this Style style)
        {
            return style.RingRaw;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public static string getIconRes(this Style style)
        {
            return style.IconRes;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="style"></param>
        /// <returns></returns>
        public static string getSmallIcon(this Style style)
        {
            return style.SmallIcon;
        }
    }

#pragma warning restore IDE1006 // 命名样式
}