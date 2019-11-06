using Framework.Shared;
using System.Collections.Generic;

namespace Framework.Core
{
    /// <summary>
    /// BaseResponse. Response object for application methods.
    /// </summary>
    public class BaseResponse<T> where T : DataTransferObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EquipoServiceResponse" /> class.
        /// </summary>
        /// <param name="status">The status.</param>
        /// <param name="errorMessage">The error message.</param>
        public BaseResponse(StatusEnum status, string errorMessage)
        {
            this.ErrorMessage = errorMessage;
            this.Status = status;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseResponse"/> class.
        /// </summary>
        /// <param name="status">The status.</param>
        public BaseResponse(StatusEnum status)
        {
            this.ErrorMessage = string.Empty;
            this.Status = status;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseResponse"/> class.
        /// </summary>
        public BaseResponse()
        {
            this.ErrorMessage = string.Empty;
            this.Status = StatusEnum.NotProcessedOperation;
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public StatusEnum Status { get; private set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// Gets or sets the error detail.
        /// </summary>
        /// <value>
        /// The error detail.
        /// </value>
        public IEnumerable<string> ErrorDetail { get; set; }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public IEnumerable<T> Data;

    }
}
