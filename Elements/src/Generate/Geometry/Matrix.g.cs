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

    /// <summary>A column-ordered 4x3 matrix.</summary>
    [System.CodeDom.Compiler.GeneratedCode("NJsonSchema", "10.1.21.0 (Newtonsoft.Json v11.0.0.0)")]
    public partial class Matrix 
    {
        [Newtonsoft.Json.JsonConstructor]
        public Matrix(IList<double> @components)
        {
            var validator = Validator.Instance.GetFirstValidatorForType<Matrix>();
            if(validator != null)
            {
                validator.PreConstruct(new object[]{ @components});
            }
        
            this.Components = @components;
            
            if(validator != null)
            {
                validator.PostConstruct(this);
            }
        }
    
        /// <summary>The components of the matrix.</summary>
        [Newtonsoft.Json.JsonProperty("Components", Required = Newtonsoft.Json.Required.Always)]
        [System.ComponentModel.DataAnnotations.Required]
        [System.ComponentModel.DataAnnotations.MinLength(12)]
        [System.ComponentModel.DataAnnotations.MaxLength(12)]
        public IList<double> Components { get; set; } = new List<double>();
    
    
    }
}