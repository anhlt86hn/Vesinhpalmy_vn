//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VesinhPalmy_vn.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserModule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> Order { get; set; }
        public Nullable<int> AdminID { get; set; }
        public Nullable<int> AdminModuleID { get; set; }
        public Nullable<int> Expr1 { get; set; }
        public string Description { get; set; }
    
        public virtual UserModule UserModule1 { get; set; }
        public virtual UserModule UserModule2 { get; set; }
    }
}
