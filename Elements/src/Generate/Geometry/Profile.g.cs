//----------------------
// <auto-generated>
//     Generated using the NJsonSchema v10.1.21.0 (Newtonsoft.Json v11.0.0.0) (http://NJsonSchema.org)
// </auto-generated>
//----------------------
using Elements;
using Elements.GeoJSON;
using Elements.Geometry;
using Elements.Geometry.Solids;
using Elements.Spatial;
using Elements.Validators;
using Elements.Serialization.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using Line = Elements.Geometry.Line;
using Polygon = Elements.Geometry.Polygon;

namespace Elements.Geometry
{
    #pragma warning disable // Disable all warnings

    /// <summary>A profile comprised of an external boundary and one or several holes.</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.21.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class Profile : Element
    {
        [Newtonsoft.Json.JsonConstructor]
        public Profile(Polygon @perimeter, IList<Polygon> @voids, System.Guid @id, string @name)
            : base(id, name)
        {
            var validator = Validator.Instance.GetFirstValidatorForType<Profile>();
            if(validator != null)
            {
                validator.PreConstruct(new object[]{ @perimeter, @voids, @id, @name});
            }
        
            this.Perimeter = @perimeter;
            this.Voids = @voids;
            
            if(validator != null)
            {
                validator.PostConstruct(this);
            }
        }
    
        /// <summary>The perimeter of the profile.</summary>
        [Newtonsoft.Json.JsonProperty("Perimeter", Required = Newtonsoft.Json.Required.AllowNull)]
        public Polygon Perimeter { get; set; }
    
        /// <summary>A collection of Polygons representing voids in the profile.</summary>
        [Newtonsoft.Json.JsonProperty("Voids", Required = Newtonsoft.Json.Required.AllowNull)]
        public IList<Polygon> Voids { get; set; }
    
    
    }
}