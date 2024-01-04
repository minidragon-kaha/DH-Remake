using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigiHero
{
    [RequireComponent(typeof(Collider))]
    public class TaggedObject : MonoBehaviour
    {
        public List<string> Tags { get { return new List<string>(tags); } }
        [SerializeField] private List<string> tags = new List<string>();

    }
}