using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class CompanyInformation
    {
        public string Name { get; set; }
        public string OrgNo { get; set; }
        public string AccountingCurrency { get; set; }
        public long? Bankgiro { get; set; }
        public string SupplierInvoiceEmail { get; set; }

        public static CompanyInformation FromNative(companyinformation native)
        {
            return new CompanyInformation
            {
                Name = native.name,
                OrgNo = native.orgno,
                AccountingCurrency = native.accountingcurrency,
                Bankgiro = native.bankgiroSpecified ? (long?)native.bankgiro : null,
                SupplierInvoiceEmail = native.supplierinvoiceemail
            };
        }
    }
}
