//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProductSurvey.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class SurveyOption
    {
        public int OptionId { get; set; }
        public string OptionValue { get; set; }
        public Nullable<int> SurveyId { get; set; }
        public Nullable<int> OptionRate { get; set; }
    }
}
