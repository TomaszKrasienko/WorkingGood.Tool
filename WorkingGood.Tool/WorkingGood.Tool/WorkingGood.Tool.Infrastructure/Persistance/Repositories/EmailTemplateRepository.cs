using MongoDB.Driver;
using WorkingGood.Tool.Domain.Enums;
using WorkingGood.Tool.Domain.Interfaces.Repositories;
using WorkingGood.Tool.Domain.Models.DataModels;
using WorkingGood.Tool.Infrastructure.Persistance;

namespace WorkingGood.Tool.Infrastructure.Repositories;

public class EmailTemplateRepository : Repository<EmailTemplate>, IEmailTemplateRepository
{
    public EmailTemplateRepository(
        IMongoDbContext mongoDbContext) : base(mongoDbContext, "email-templates")
    {
    }

    public async Task<EmailTemplate> GetByDestinationAsync(EmailTemplateDestination emailTemplateDestination)
    {
        var collection = GetCollection();
        var result = await collection.FindAsync<EmailTemplate>(x => x.Destination == emailTemplateDestination);
        return await result.FirstOrDefaultAsync();
    }
}