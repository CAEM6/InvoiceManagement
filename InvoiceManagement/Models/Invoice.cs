//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InvoiceManagement.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice()
        {
            this.InvoicePayments = new HashSet<InvoicePayment>();
        }
    
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public int AccountId { get; set; }
        public string Folio { get; set; }
        public int NumTrans { get; set; }
        public string OrdenCompra { get; set; }
        public string Moneda { get; set; }
        public string TipoPago { get; set; }
        public string MetodoPago { get; set; }
        public System.DateTime Fecha { get; set; }
        public decimal Iva { get; set; }
        public decimal IvaPorcentaje { get; set; }
        public decimal IvaRetenido { get; set; }
        public decimal IvaTrasladado { get; set; }
        public decimal Total { get; set; }
        public decimal IsrRetenido { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Pagado { get; set; }
        public Nullable<System.DateTime> UltimoPago { get; set; }
        public decimal Balance { get; set; }
        public string Notas { get; set; }
    
        public virtual Account Account { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InvoicePayment> InvoicePayments { get; set; }
        public virtual Supplier Supplier { get; set; }
    }
}
