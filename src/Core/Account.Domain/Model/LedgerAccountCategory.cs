using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Account.Domain.Model
{
    public class LedgerAccountCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StatusId { get; set; }

        public ICollection<LedgerAccount> LedgerAccounts { get; set; }
        public Status Status { get; set; }
    }
}
