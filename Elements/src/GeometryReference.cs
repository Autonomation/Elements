using System.Collections.Generic;

namespace Elements
{
    /// <summary>A reference to a model, hosted at a URL.</summary>
    [Newtonsoft.Json.JsonConverter(typeof(Elements.Serialization.JSON.JsonInheritanceConverter), "discriminator")]
    public partial class GeometryReference
    {
        /// <summary>The URL where the referenced geometry is hosted.</summary>
        [Newtonsoft.Json.JsonProperty("GeometryUrl", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public string GeometryUrl { get; set; }

        /// <summary>Any geometric data directly contained in this reference.</summary>
        [Newtonsoft.Json.JsonProperty("InternalGeometry", Required = Newtonsoft.Json.Required.DisallowNull, NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
        public IList<object> InternalGeometry { get; set; }

        /// <summary>
        /// Construct a geometry reference.
        /// </summary>
        /// <param name="geometryUrl">The url of the referenced geometry.</param>
        /// <param name="internalGeometry">Geometry containe in this reference.</param>
        [Newtonsoft.Json.JsonConstructor]
        public GeometryReference(string @geometryUrl, IList<object> @internalGeometry)
        {
            this.GeometryUrl = @geometryUrl;
            this.InternalGeometry = @internalGeometry;
        }
    }
}