namespace gha.mobile.mims.repositories
{
    /// <summary>
    /// Base class for an epicor response, containing success and error message
    /// </summary>
    public class EpicorResponse
    {
        /// <summary>
        /// True if successful response, false if not
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// The error message from an unsuccessful response
        /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// A template for a bad response whenever the epicor request fails
        /// </summary>
        public static EpicorResponse BadResponse => new EpicorResponse
        {
            Success = false,
            ErrorMessage = "Unknown error."
        };
    }

    /// <summary>
    /// A generic class that extends <see cref="EpicorResponse"/> to allow for retrieving response data as specified types
    /// </summary>
    /// <typeparam name="T">The type of the response data</typeparam>
	public class EpicorResponse<T> : EpicorResponse
    {
        /// <summary>
        /// The data returned from the request as an generic object
        /// </summary>
        public T ReturnObj { get; set; }
        /// <summary>
        /// A template for a bad response whenever the epicor request fails
        /// </summary>
        public new static EpicorResponse<T> BadResponse => new EpicorResponse<T>
        {
            Success = false,
            ErrorMessage = "Unknown error."
        };
    }
}
