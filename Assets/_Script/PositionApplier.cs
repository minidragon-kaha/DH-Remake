using Obvious.Soap;
using UnityEngine;

namespace DigiHero
{
    public class PositionApplier : MonoBehaviour
    {
        [SerializeField] private Vector3Variable positionVariable;
        [SerializeField] private Vector3Reference offset;

        private void Update()
        {
            transform.position = positionVariable.Value + offset.Value;
        }
    }
}