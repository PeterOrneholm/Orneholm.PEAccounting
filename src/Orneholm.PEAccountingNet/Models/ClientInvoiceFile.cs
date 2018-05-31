using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class ClientInvoiceFile
    {
        public string Filename { get; set; }
        public byte[] Data { get; set; }

        internal static ClientInvoiceFile FromNative(clientinvoiceFile native)
        {
            return new ClientInvoiceFile
            {
                Filename = native.filename,
                Data = native.data
            };
        }

        internal clientinvoiceFile ToNative()
        {
            return new clientinvoiceFile
            {
                filename = Filename,
                data = Data
            };
        }
    }
}