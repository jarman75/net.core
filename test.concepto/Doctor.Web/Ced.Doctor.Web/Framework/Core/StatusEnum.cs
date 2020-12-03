namespace Framework.Core
{
    /// <summary>
    /// StatusEnum. Type of response error in an object Response of application.
    /// </summary>
    public enum StatusEnum
    {
        /// <summary>
        /// The un known error
        /// </summary>
        UnKnownError = 0,

        /// <summary>
        /// The correct operation
        /// </summary>
        CorrectOperation = 1,

        /// <summary>
        /// The operation error
        /// </summary>
        OperationError = 2,

        /// <summary>
        /// The validation error
        /// </summary>
        ValidationError = 3,

        /// <summary>
        /// The not processed operation
        /// </summary>
        NotProcessedOperation = 4,

        /// <summary>
        /// The null excepcion
        /// </summary>
        NullExcepcion = 5,

        /// <summary>
        /// The not found error
        /// </summary>
        NotFoundError = 6,

    }
}
