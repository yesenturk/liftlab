using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace liftmarket.Models.Abstracts
{
    public interface IBaseModel
    {
        int Id { get; set; }
        DateTime CreateDate { get; set; }

        DateTime? UpdateDate { get; set; }

        DateTime? DeleteDate { get; set; }
        bool IsDeleted { get; set; }
    }
}