using System.Text.Json.Serialization;

namespace VariacaoAtivos.Domain.Response
{
    /// <summary>
    /// The API response.
    /// </summary>
    public class ApiResponse<T> where T : class
    {
        public ApiResponse() // TODO remove in future impl.
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="id">The Id.</param>
        /// <param name="success">If true, success.</param>
        /// <param name="exceptions">The exceptions.</param>
        public ApiResponse(T data, string id, bool success, params string[] exceptions)
        {
            Data = data;
            Id = id;
            Success = success;
            Exceptions = exceptions;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        [JsonPropertyName("retorno")]
        public T Data { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonPropertyName("identificador")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether success.
        /// </summary>
        [JsonPropertyName("sucesso")]
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the exceptions.
        /// </summary>
        public IEnumerable<string> Exceptions { get; set; }
    }

    /// <summary>
    /// The API response.
    /// </summary>
    public class ApiResponse
    {
        public ApiResponse() // TODO remove in future impl.
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApiResponse"/> class.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="id">The Id.</param>
        /// <param name="success">If true, success.</param>
        /// <param name="exceptions">The exceptions.</param>
        public ApiResponse(object data, string id, bool success, params string[] exceptions)
        {
            Data = data;
            Id = id;
            Success = success;
            Exceptions = exceptions;
        }

        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        [JsonPropertyName("retorno")]
        public object Data { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonPropertyName("identificador")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether success.
        /// </summary>
        [JsonPropertyName("sucesso")]
        public bool Success { get; set; }

        /// <summary>
        /// Gets or sets the exceptions.
        /// </summary>
        public IEnumerable<string> Exceptions { get; set; }
    }
}