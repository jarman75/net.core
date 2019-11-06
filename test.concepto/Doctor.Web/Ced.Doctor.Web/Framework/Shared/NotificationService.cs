using Framework.Core;
using System;

namespace Framework.Shared
{
    /// <summary>
    /// NotificationService. Service to manage multiple notifications.
    /// </summary>
    public class NotificationService
    {
        private Notification _notification = new Notification();

        /// <summary>
        /// Checks this instance.
        /// </summary>
        /// <returns></returns>
        public Notification Check()
        {
            return _notification ?? new Notification();
        }

        /// <summary>
        /// Clears this instance.
        /// </summary>
        public void Clear()
        {
            this._notification = new Notification();
        }

        /// <summary>
        /// Adds the notification.
        /// </summary>
        /// <param name="message">The message.</param>
        protected void AddNotification(string message)
        {
            AddNotification(message, null, 0);
        }

        /// <summary>
        /// Adds the notification.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="id">The identifier.</param>
        protected void AddNotification(string message, int id)
        {
            AddNotification(message, null, id);
        }

        /// <summary>
        /// Adds the notification.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        protected void AddNotification(string message, Exception ex)
        {
            _notification.AddError(message, ex, 0);
        }

        /// <summary>
        /// Adds the notification.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="id">The identifier.</param>
        protected void AddNotification(string message, Exception ex, int id)
        {
            _notification.AddError(message, ex, id);
        }

        /// <summary>
        /// Adds the notification.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="status">The status.</param>
        protected void AddNotification(EntityStatus status)
        {
            _notification.AddError(status);
        }

        /// <summary>
        /// Adds the notification.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="status">The status.</param>
        /// <param name="id">The identifier.</param>
        protected void AddNotification(EntityStatus status, int id)
        {
            _notification.AddError(status, id);
        }
    }
}
