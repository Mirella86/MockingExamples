using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeModels.Implementations
{
    public class FakeClientSet : FakeDbSet<Client>
    {
        public override Client Find(params object[] keyValues)
        {
            return this.SingleOrDefault(e => e.Id == (int)keyValues.Single());
        }
    }
}
