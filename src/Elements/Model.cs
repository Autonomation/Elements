#pragma warning disable CS1591

using Elements.GeoJSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Collections;
using Newtonsoft.Json;
using Elements.Serialization.JSON;
using Elements.Geometry;

namespace Elements
{
    /// <summary>
    /// A container for elements, element types, materials, and profiles.
    /// </summary>
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public partial class Model : IDictionary<Guid, Identifiable>
    {
        private Dictionary<Guid, Identifiable> _entities = new Dictionary<Guid, Identifiable>();

        /// <summary>
        /// Items provides a serializable wrapper around the
        /// internal entities collection.
        /// </summary>
        [JsonProperty]
        public Dictionary<Guid, Identifiable> Entities => this._entities;

        public ICollection<Guid> Keys => _entities.Keys;

        public ICollection<Identifiable> Values => _entities.Values;

        public int Count => _entities.Count;

        public bool IsReadOnly => false;

        public Identifiable this[Guid key] { get => _entities[key]; set => _entities[key] = value; }
        
        /// <summary>
        /// Construct an empty model.
        /// </summary>
        public Model()
        {
            this.Origin = new Position(0, 0);
        }

        /// <summary>
        /// Add an element to the model.
        /// </summary>
        /// <param name="element">The element to add to the model.</param>
        /// <exception cref="System.ArgumentException">Thrown when an element 
        /// with the same Id already exists in the model.</exception>
        public void AddElement(Element element)
        {
            if (element == null)
            {
                return;
            }

            if (!_entities.ContainsKey(element.Id))
            {
                GetRootLevelElementData(element);
                _entities.Add(element.Id, element);
            }
            else
            {
                throw new ArgumentException("An element with the same Id already exists in the Model.");
            }
        }

        /// <summary>
        /// Update an element existing in the model.
        /// </summary>
        /// <param name="element">The element to update in the model.</param>
        /// <exception cref="System.ArgumentException">Thrown when no element 
        /// with the same Id exists in the model.</exception>
        public void UpdateElement(Element element)
        {
            if (element == null)
            {
                return;
            }

            if (_entities.ContainsKey(element.Id))
            {
                // remove the previous element
                _entities.Remove(element.Id);
                // Update the element itselft
                _entities.Add(element.Id, element);
                // Update the root elements
                GetRootLevelElementData(element);
            }
            else
            {
                throw new ArgumentException("No element with this Id exists in the Model.");
            }
        }

        /// <summary>
        /// Add a collection of elements to the model.
        /// </summary>
        /// <param name="elements">The elements to add to the model.</param>
        public void AddElements(IEnumerable<Element> elements)
        {
            foreach (var e in elements)
            {
                AddElement(e);
            }
        }

        /// <summary>
        /// Update a collection of elements in the model.
        /// </summary>
        /// <param name="elements">The elements to be updated in the model.</param>
        public void UpdateElements(IEnumerable<Element> elements)
        {
            foreach (var e in elements)
            {
                UpdateElement(e);
            }
        }

        /// <summary>
        /// Get an entity by id from the Model.
        /// </summary>
        /// <param name="id">The identifier of the element.</param>
        /// <returns>An entity or null if no entity can be found 
        /// with the provided id.</returns>
        public T GetEntityOfType<T>(Guid id) where T: Identifiable
        {
            if (_entities.ContainsKey(id))
            {
                return (T)_entities[id];
            }
            return null;
        }

        /// <summary>
        /// Get the first entity with the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns>An entity or null if no entity can be found 
        /// with the provided name.</returns>
        public T GetEntityByName<T>(string name) where T: Identifiable
        {
            var found = _entities.FirstOrDefault(e => e.Value.Name == name);
            if (found.Equals(new KeyValuePair<long, Element>()))
            {
                return null;
            }
            return (T)found.Value;
        }

        /// <summary>
        /// Get all entities of the specified Type.
        /// </summary>
        /// <typeparam name="T">The Type of element to return.</typeparam>
        /// <returns>A collection of elements of the specified type.</returns>
        public IEnumerable<T> AllEntitiesOfType<T>()
        {
            return _entities.Values.OfType<T>();
        }

        /// <summary>
        /// Serialize the model to JSON.
        /// </summary>
        public string ToJson() 
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, new JsonSerializerSettings(){
                Formatting = Formatting.Indented
            });
        }

        /// <summary>
        /// Deserialize a model from JSON.
        /// </summary>
        /// <param name="data">The JSON representing the model.</param>
        public static Model FromJson(string data)
        {
            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<Model>(data);
            JsonInheritanceConverter.Identifiables.Clear();
            return model;
        }

        internal Model(Dictionary<Guid, Identifiable> entities, Position origin)
        {
            _entities = entities;
            this.Origin = origin;
        }

        private void GetRootLevelElementData(object element)
        {
            var props = element.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach(var p in props)
            {
                var pValue = p.GetValue(element);
                if (typeof(Identifiable).IsAssignableFrom(p.PropertyType))
                {
                    var ident =(Identifiable)pValue;
                    Add(ident.Id, ident);
                }
                // GetRootLevelElementData(pValue);
            }
        }

        public void Add(Guid key, Identifiable value)
        {
            if(!_entities.ContainsKey(key))
            {
                _entities.Add(key, value);
            }
        }

        public bool ContainsKey(Guid key)
        {
            return _entities.ContainsKey(key);
        }

        public bool Remove(Guid key)
        {
            return _entities.Remove(key);
        }

        public bool TryGetValue(Guid key, out Identifiable value)
        {
            return _entities.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<Guid, Identifiable> item)
        {
            _entities.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _entities.Clear();
        }

        public bool Contains(KeyValuePair<Guid, Identifiable> item)
        {
            return _entities.Contains(item);
        }

        public void CopyTo(KeyValuePair<Guid, Identifiable>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<Guid, Identifiable> item)
        {
            return _entities.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<Guid, Identifiable>> GetEnumerator()
        {
            return _entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _entities.GetEnumerator();
        }
    }
}