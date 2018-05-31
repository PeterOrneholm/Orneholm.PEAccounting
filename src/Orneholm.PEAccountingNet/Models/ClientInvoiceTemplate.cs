using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class ClientInvoiceTemplate
    {
        public int Id { get; set; }
        public string ForeignId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        internal static ClientInvoiceTemplate FromInternal(clientinvoicetemplate native)
        {
            return new ClientInvoiceTemplate
            {
                Id = native.id,
                Name = native.name,
                ForeignId = native.foreignid,
                Description = native.description
            };
        }
    }
}