//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace ASP.NET_FinalTermExam.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Scores
    {
        public string TestID { get; set; }
        public string StudentID { get; set; }
        public byte Score { get; set; }
    
        public virtual Tests Tests { get; set; }
    }
}