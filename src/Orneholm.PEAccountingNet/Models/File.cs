using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class File
    {
        public string Filename { get; set; }
        public byte[] Data { get; set; }

        internal static File FromNative(file native)
        {
            return new File
            {
                Filename = native.filename,
                Data = native.data
            };
        }

        internal file ToNative()
        {
            return new file
            {
                filename = Filename,
                data = Data
            };
        }
    }
}
