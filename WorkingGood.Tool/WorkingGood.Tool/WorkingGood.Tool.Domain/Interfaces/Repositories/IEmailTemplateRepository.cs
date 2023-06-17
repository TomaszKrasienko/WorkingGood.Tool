using WorkingGood.Tool.Domain.Enums;
using WorkingGood.Tool.Domain.Models.DataModels;

namespace WorkingGood.Tool.Domain.Interfaces.Repositories;

public interface IEmailTemplateRepository : IRepository<EmailTemplate>
{
    Task<EmailTemplate> GetByDestinationAsync(EmailTemplateDestination emailTemplateDestination);
    Task<bool> IsExistsByDestinationAsync(EmailTemplateDestination emailTemplateDestination);
}