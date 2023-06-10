using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace EfQuerysOptimized;
public class DataQuerys
{
    readonly DataContext _context;
    public DataQuerys(DataContext context)
    {
        _context = context;
        _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    
    
    public async Task<List<Transaction>> GetTransactionsByAccountId(int accountId,
                                                                    Expression<Func<Transaction, bool>>? predicate = null,
                                                                    CancellationToken cancellationToken = default)
    {
        var query = _context.Transactions.Where(x => x.AccountId == accountId);
        if (predicate != null)
            query = query.Where(predicate);
        return await query.ToListAsync(cancellationToken);
    }
    
    //find account by id with find
    public async Task<Account?> FindAccountById(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Accounts.FindAsync(new object[] { id }, cancellationToken);
    }
    //get account by id with first or default
    public async Task<Account?> GetAccountById(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Accounts.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
    //get account by id with single or default
    public async Task<Account?> GetAccountByIdSingle(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Accounts.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

        
}
