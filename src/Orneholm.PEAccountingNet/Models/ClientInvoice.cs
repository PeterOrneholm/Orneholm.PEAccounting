using System;
using System.Collections.Generic;
using System.Linq;
using Orneholm.PEAccountingNet.Models.Native;

namespace Orneholm.PEAccountingNet.Models
{
    public class ClientInvoice
    {
        /// <summary>
        /// The invoice's internal identifier in PE. Unique within a given company.
        /// Non-writable.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// The invoice's number, set when invoice is certified. Otherwise 0.
        /// Non-writable.
        /// </summary>
        public int InvoiceNr { get; set; }

        /// <summary>
        /// Used when creating a credit invoice.
        /// Specify invoice number of debit invoice in order to create proper association.
        /// Required for all credit invoices.
        /// </summary>
        public int? DebitInvoiceNr { get; set; }
        /// <summary>
        /// List of invoices that was used to credit this invoice.
        /// Non-writable.
        /// </summary>
        public List<int> CreditInvoicesIds { get; set; }

        public string ForeignId { get; set; }
        public string PoNr { get; set; } // null = ""

        public int ClientId { get; set; }
        /// <summary>
        /// Optional.
        /// If not set, a template will be automatically assigned.
        /// </summary>
        public int? ClientInvoiceTemplateId { get; set; }

        public string YourReference { get; set; }
        public int? OurReferenceUserId { get; set; } // null = 0
        public int? ApproverUserId { get; set; } // null = 0

        public Address InvoiceAddress { get; set; }
        public string InvoiceEmail { get; set; }

        public string DeliveryName { get; set; }
        public Address DeliveryAddress { get; set; }
        public string DeliveryEmail { get; set; }

        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? DeliveryDate { get; set; } //null == ""

        public ClientInvoicePeriod Period { get; set; } // can be null

        public string Currency { get; set; }
        /// <summary>
        /// If not specified, the latest available currency rate will be used.
        /// </summary>
        public decimal? CurrencyRate { get; set; }

        /// <summary>
        /// Whether this invoice has been certified or not.
        /// Read-only.
        /// </summary>
        public bool IsCertified { get; set; }
        /// <summary>
        /// Whether this invoice has been sent at least once or not.
        /// Read-only.
        /// </summary>
        public bool IsSent { get; set; }

        public string Notes { get; set; }
        /// <summary>
        /// When creating, leave empty to use default delivery.
        /// When saving, leave empty to leave original value unchanged.
        /// </summary>
        public string DeliveryType { get; set; }

        public string Gln { get; set; }
        /// <summary>
        /// When creating, leave empty to use client vat-nr.
        /// When saving leave empty to leave original value unchanged.
        /// </summary>
        public string VatNr { get; set; }
        /// <summary>
        /// The ISO 3166-1 alpha-2 code for sales turnover for the client invoice.
        /// When creating, leave empty to use client country code.
        /// When saving leave empty to leave original value unchanged.
        /// </summary>
        public string CountryCode { get; set; }

        /// <summary>
        /// Set when reading invoices created from client agreements.
        /// Ignored when creating/updating.
        /// </summary>
        public int? ClientAgreementId { get; set; }

        /// <summary>
        /// In cent, will be 0 for non certified invoices.
        /// Read-only.
        /// </summary>
        public long Remaining { get; set; }
        /// <summary>
        /// The last date, if any, that this invoice had a payment registered.
        /// </summary>
        public DateTime? LastPaymentDate { get; set; }

        /// <summary>
        /// A disabled invoice means it has been deleted by a user.
        /// </summary>
        public bool IsDisabled { get; set; }

        public bool? IsAutomaticActionsDisabled { get; set; }
        public string AutomaticActionsMessage { get; set; }

        public List<Field> Fields { get; set; }

        public ClientInvoiceApproved Approved { get; set; }  //null

        public List<ClientInvoiceFile> Files { get; set; }

        public List<ClientInvoiceRow> Rows { get; set; }

        public static ClientInvoice FromNative(clientinvoice native)
        {
            return new ClientInvoice
            {
                Id = native.id,
                InvoiceNr = native.invoicenr,

                DebitInvoiceNr = native.debitinvoicenrSpecified ? native.debitinvoicenr : (int?)null,
                CreditInvoicesIds = native.creditinvoices?.Select(x => x.id).ToList() ?? new List<int>(),

                ForeignId = native.foreignid,
                PoNr = native.ponr != string.Empty ? native.ponr : null,

                ClientId = native.clientref.id,
                ClientInvoiceTemplateId = native.clientinvoicetemplateref?.id,

                YourReference = native.yourreference,
                OurReferenceUserId = native.ourreference?.id > 0 ? native.ourreference?.id : null,
                ApproverUserId = native.approver?.id > 0 ? native.approver?.id : null,
                
                InvoiceAddress = Address.FromNative(native.invoiceaddress),
                InvoiceEmail = native.invoiceemail,
                
                DeliveryName = native.deliveryname,
                DeliveryType = native.deliverytype,
                DeliveryAddress = Address.FromNative(native.deliveryaddress),
                DeliveryEmail = native.deliveryemail,

                InvoiceDate = native.invoicedate,
                DueDate = native.duedate,
                DeliveryDate = native.deliverydate != DateTime.MinValue ? native.deliverydate : (DateTime?)null,
                LastPaymentDate = native.lastpaymentdate,

                Period = ClientInvoicePeriod.FromNative(native.period),

                Currency = native.currency,
                CurrencyRate = native.currencyrateSpecified ? native.currencyrate : (decimal?)null,

                IsCertified = native.certified,
                IsSent = native.sent,
                
                Notes = native.notes,
                Gln = native.gln,
                VatNr = native.vatnr,
                CountryCode = native.countrycode,
                
                ClientAgreementId = native.clientagreementref?.id,
                
                Remaining = native.remaining,

                IsDisabled = native.disabled,
                IsAutomaticActionsDisabled = native.automaticactionsdisabledSpecified ? native.automaticactionsdisabled : (bool?)null,
                AutomaticActionsMessage = native.automaticactionsmessage,

                Fields = native.fields?.Select(Field.FromNative).ToList() ?? new List<Field>(),
                Files = native.files?.Select(ClientInvoiceFile.FromNative).ToList() ?? new List<ClientInvoiceFile>(),
                Rows = native.rows?.Select(ClientInvoiceRow.FromNative).ToList() ?? new List<ClientInvoiceRow>(),

                Approved = ClientInvoiceApproved.FromNative(native.approved)
            };
        }

        public clientinvoice ToNative()
        {
            return new clientinvoice
            {

            };
        }
    }
}