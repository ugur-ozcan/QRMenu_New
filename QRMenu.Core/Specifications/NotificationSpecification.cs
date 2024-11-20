using QRMenu.Core.Entities;
using QRMenu.Core.Specifications;

namespace QRMenu.Core.Specifications;

public class NotificationSpecification : BaseSpecification<Notification>
{
    public NotificationSpecification(int userId, bool unreadOnly = false)
        : base()
    {
        if (unreadOnly)
        {
            AddCriteria(x => x.UserId == userId && !x.IsRead && !x.IsDeleted);
        }
        else
        {
            AddCriteria(x => x.UserId == userId && !x.IsDeleted);
        }

        AddOrderByDescending(x => x.CreatedAt);
    }
}