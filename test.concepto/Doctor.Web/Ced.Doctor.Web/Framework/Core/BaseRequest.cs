using Framework.Core;
using Framework.Shared;

namespace Framework.Core
{
    public abstract class BaseRequest : NotificationService
    { 
        public abstract void Validate();

        public void AddEntityStatus(EntityStatus efStatus)
        {

            this.AddNotification(efStatus, efStatus.Id);
        }
    }
}
