namespace Orneholm.PEAccountingNet.Filters
{
    public enum ClientInvoiceStatus
    {
        /// <summary>
        /// Get all client invoices, standard if not otherwise stated.
        /// </summary>
        All,
        /// <summary>
        /// Get certified client invoices only.
        /// </summary>
        Certified,
        /// <summary>
        /// Only get client invoices ready to be certified.
        /// </summary>
        Certifiable,
        /// <summary>
        /// Get client invoices that are ready to send only.
        /// </summary>
        Sendable
    }
}