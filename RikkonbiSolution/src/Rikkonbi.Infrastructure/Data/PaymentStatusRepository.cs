using Rikkonbi.Core.Entities;
using Rikkonbi.Core.Interfaces;

namespace Rikkonbi.Infrastructure.Data
{
    public class PaymentStatusRepository : EfRepository<PaymentStatus>, IPaymentStatusRepository
    {
        public PaymentStatusRepository(RikkonbiDbContext context) : base(context) { }
    }
}