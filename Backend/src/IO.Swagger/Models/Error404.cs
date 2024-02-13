/*
 * Smart Lab RA API
 *
 * Documento OAS para la API Smart Lab RA
 *
 * OpenAPI spec version: 1
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */
using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace IO.Swagger.Models
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public partial class Error404 : IEquatable<Error404>
    { 
        /// <summary>
        /// Descripción del error 404
        /// </summary>
        /// <value>Descripción del error 404</value>

        [DataMember(Name="mensaje")]
        public string Mensaje { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class Error404 {\n");
            sb.Append("  Mensaje: ").Append(Mensaje).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="obj">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((Error404)obj);
        }

        /// <summary>
        /// Returns true if Error404 instances are equal
        /// </summary>
        /// <param name="other">Instance of Error404 to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(Error404 other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            return 
                (
                    Mensaje == other.Mensaje ||
                    Mensaje != null &&
                    Mensaje.Equals(other.Mensaje)
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                var hashCode = 41;
                // Suitable nullity checks etc, of course :)
                    if (Mensaje != null)
                    hashCode = hashCode * 59 + Mensaje.GetHashCode();
                return hashCode;
            }
        }

        #region Operators
        #pragma warning disable 1591

        public static bool operator ==(Error404 left, Error404 right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Error404 left, Error404 right)
        {
            return !Equals(left, right);
        }

        #pragma warning restore 1591
        #endregion Operators
    }
}
