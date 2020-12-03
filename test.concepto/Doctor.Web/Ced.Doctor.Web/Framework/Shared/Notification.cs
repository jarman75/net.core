using Framework.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Framework.Shared
{
    /// <summary>
    /// Notification. Manage multiple notifications.
    /// </summary>
    public class Notification
    {
        private readonly List<NotificationError> _errors = new List<NotificationError>();

        private class NotificationError
        {
            public String Message { get; }
            public Exception Cause { get; }
            public int Id { get; }

            public NotificationError(String message, Exception cause, int id)
            {
                this.Message = message ?? string.Empty;
                this.Cause = cause ?? new Exception(string.Empty);
                this.Id = id;
            }
        }

        /// <summary>
        /// Errors the message.
        /// </summary>
        /// <returns></returns>
        public String ErrorMessage()
        {
            return HasErrors() ? string.Join("\r\n", _errors.Select(x => x.Message + "  " + x.Cause.Message)) : String.Empty;
        }

        /// <summary>
        /// Errors the message with identifier.
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> ErrorMessageWithId()
        {
            Dictionary<int, string> result = new Dictionary<int, string>();

            if (HasErrors())
            {
                foreach (var item in _errors)
                {
                    string locerror = string.Join(";", item.Message.Replace("\r\n", "") + item.Cause.Message.Replace("\r\n", ""));
                    if (result.ContainsKey(item.Id))
                    {
                        result[item.Id] = new StringBuilder(locerror).AppendLine().ToString();
                    }
                    else
                    {
                        result.Add(item.Id, locerror);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="id">The identifier.</param>
        public void AddError(String message, Exception exception = null, int id = 0) { _errors.Add(new NotificationError(message, exception, id)); }

        /// <summary>
        /// Adds the error.
        /// </summary>
        /// <param name="efStatus">The ef status.</param>
        /// <param name="id">The identifier.</param>
        public void AddError(EntityStatus efStatus, int id = 0)
        {
            if (!efStatus.IsValid)
            {
                foreach (var errorStatus in efStatus.EfErrors)
                {
                    _errors.Add(new NotificationError("Error validando entidad.", new ValidationException(errorStatus.ErrorMessage), id));
                }
            }
        }

        /// <summary>
        /// Determines whether this instance has errors.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has errors; otherwise, <c>false</c>.
        /// </returns>
        public bool HasErrors() => _errors.Count > 0;
    }
}
