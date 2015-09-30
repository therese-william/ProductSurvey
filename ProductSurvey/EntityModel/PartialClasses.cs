using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProductSurvey.EntityModel
{
    public class PartialClasses
    {
        [MetadataType(typeof(SurveyQuestionMetaData))]
        public partial class SurveyQuestion{}
    }
}