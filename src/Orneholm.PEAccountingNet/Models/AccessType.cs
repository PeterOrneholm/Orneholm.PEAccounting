namespace Orneholm.PEAccountingNet.Models
{
    public enum AccessType
    {
        ClientAgreementOwn,
        ClientInvoice,
        ClientInvoiceCertifyOwn,
        ClientInvoiceOwn,
        ClientProjectOwn,

        Dimensions,

        Expenses,
        ExpensesOwn,
        ExpensesPay,

        ExternalAccountant,

        Payroll,
        PayrollOwn,
        PayrollPay,

        Responsible,
        ResponsibleRead,

        ResultreportOwn,

        SupplierInvoice,
        SupplierInvoiceCreate,
        SupplierInvoicePay,

        Timereport,
        TimereportOwn
    }
}