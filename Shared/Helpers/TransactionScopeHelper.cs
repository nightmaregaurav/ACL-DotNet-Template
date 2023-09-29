using System.Transactions;

namespace Shared.Helpers
{
    public static class TransactionScopeHelper
    {
        public static TransactionScope GetInstance() => new(TransactionScopeAsyncFlowOption.Enabled);
    }
}
