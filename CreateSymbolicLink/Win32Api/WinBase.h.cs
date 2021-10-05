using System.Runtime.InteropServices;

namespace Win32Api
{
    public static partial class Kernel32
    {
        [DllImport(AssemblyName, SetLastError = true, CharSet = CharSet.Unicode)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, uint dwFlags);
    }
}
