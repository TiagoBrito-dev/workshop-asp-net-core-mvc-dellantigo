using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Models.ViewModels
{
    public class SalesRecordFormViewModel
    {
        public SalesRecord SalesRecord { get; set; }
        public ICollection<Seller> Sellers { get; set; }

        public SalesRecordFormViewModel()
        {
        }

        public SalesRecordFormViewModel(SalesRecord salesRecord, ICollection<Seller> sellers)
        {
            SalesRecord = salesRecord;
            Sellers = sellers;
        }
    }
}
