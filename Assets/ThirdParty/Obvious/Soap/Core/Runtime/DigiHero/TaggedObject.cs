using System.Collections.Generic;
using UnityEngine;
using Obvious.Soap;

namespace DigiHero
{
    [RequireComponent(typeof(Collider))]
    public class TaggedObject : MonoBehaviour
    {
        public List<string> Tags { get { return new List<string>(tags); } }
        [SerializeField] private List<string> tags = new List<string>();
        [SerializeField] private ScriptableListTaggedObject scriptableListTaggedObject;

        private void OnEnable()
        {
            scriptableListTaggedObject.Add(this);
        }

        private void OnDisable()
        {
            scriptableListTaggedObject.Remove(this);
        }
    }
}