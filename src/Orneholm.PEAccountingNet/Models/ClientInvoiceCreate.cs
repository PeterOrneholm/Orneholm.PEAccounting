using System;
using System.Collections.Generic;
using System.Linq;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class ClientInvoiceCreate
    {
        /// <summary>
        /// Used when creating a credit invoice.
        /// Specify invoice number of debit invoice in order to create proper association.
        /// Required for all credit invoices.
        /// </summary>
        public int? DebitInvoiceNr { get; set; }

        public string ForeignId { get; set; }
        public string PoNr { get; set; }

        public int ClientId { get; set; }
        /// <summary>
        /// Optional.
        /// If not set, a template will be automatically assigned.
        /// </summary>
        public int? ClientInvoiceTemplateId { get; set; }

        public string YourReference { get; set; }
        public int? OurReferenceUserId { get; set; }
        public int? ApproverUserId { get; set; }

        public DateTime InvoiceDate { get; set; }
        public Address InvoiceAddress { get; set; } = new Address();
        public string InvoiceEmail { get; set; }

        public DateTime? DeliveryDate { get; set; }
        public string DeliveryName { get; set; }
        public Address DeliveryAddress { get; set; } = new Address();
        public string DeliveryEmail { get; set; }
        /// <summary>
        /// When creating, leave empty to use default delivery.
        /// When saving, leave empty to leave original value unchanged.
        /// </summary>
        public string DeliveryType { get; set; }


        public DateTime DueDate { get; set; }

        public ClientInvoicePeriod Period { get; set; }

        public string Currency { get; set; }
        /// <summary>
        /// If not specified, the latest available currency rate will be used.
        /// </summary>
        public decimal? CurrencyRate { get; set; }

        public string Notes { get; set; }

        public string Gln { get; set; }
        /// <summary>
        /// When creating, leave empty to use client vat-nr.
        /// When saving leave empty to leave original value unchanged.
        /// </summary>
        public string VatNr { get; set; }
        /// <summary>
        /// The ISO 3166-1 alpha-2 code for sales turnover for the client invoice.
        /// 
        /// When creating, leave empty to use client country code.
        /// When saving leave empty to leave original value unchanged.
        /// </summary>
        public string CountryCode { get; set; }

        public bool? IsAutomaticActionsDisabled { get; set; }
        public string AutomaticActionsMessage { get; set; }

        public List<Field> Fields { get; set; } = new List<Field>();

        public List<ClientInvoiceFile> Files { get; set; } = new List<ClientInvoiceFile>();

        public List<ClientInvoiceRowCreate> Rows { get; set; } = new List<ClientInvoiceRowCreate>();

        
        internal clientinvoice ToNative()
        {
            return new clientinvoice
            {
                debitinvoicenr = DebitInvoiceNr.GetValueOrDefault(),
                debitinvoicenrSpecified = DebitInvoiceNr.HasValue,

                foreignid = ForeignId ?? string.Empty,
                ponr = PoNr ?? string.Empty,

                clientref = new clientreference { id = ClientId },
                clientinvoicetemplateref = ClientInvoiceTemplateId.HasValue ? new clientinvoicetemplatereference { id = ClientInvoiceTemplateId.Value } : null,

                yourreference = YourReference ?? string.Empty,
                ourreference = new userreference { id = OurReferenceUserId.GetValueOrDefault(0) }, // 0 = not set according to docs
                approver = new userreference { id = ApproverUserId.GetValueOrDefault(0) }, // 0 = not set according to docs

                invoicedate = InvoiceDate,
                invoiceaddress = InvoiceAddress != null ? InvoiceAddress.ToNative() : new Address().ToNative(),
                invoiceemail = InvoiceEmail ?? string.Empty,

                deliverydate = DeliveryDate ?? default(DateTime),
                deliveryname = DeliveryName ?? string.Empty,
                deliverytype = DeliveryType ?? string.Empty,
                deliveryaddress = DeliveryAddress != null ? DeliveryAddress.ToNative() : new Address().ToNative(),
                deliveryemail = DeliveryEmail ?? string.Empty,

                duedate = DueDate,

                period = Period?.ToNative(),
                currency = Currency ?? string.Empty,
                currencyrate = CurrencyRate.GetValueOrDefault(),
                currencyrateSpecified = CurrencyRate.HasValue,

                notes = Notes ?? string.Empty,
                gln = Gln ?? string.Empty,
                vatnr = VatNr ?? string.Empty,
                countrycode = CountryCode ?? string.Empty,

                automaticactionsdisabled = IsAutomaticActionsDisabled.GetValueOrDefault(),
                automaticactionsdisabledSpecified = IsAutomaticActionsDisabled.HasValue,
                automaticactionsmessage = AutomaticActionsMessage ?? string.Empty,

                fields = Fields?.Select(x => x.ToNative()).ToArray(),
                files = Files?.Select(x => x.ToNative()).ToArray(),
                rows = Rows?.Select(x => x.ToNative()).ToArray()
            };
        }
    }
}