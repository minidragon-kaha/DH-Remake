using UnityEngine;
using Obvious.Soap;

namespace DigiHero
{
    [AddComponentMenu("DigiHero/PositionHandler")]
    public class PositionHandler : MonoBehaviour
    {
        [SerializeField] private Vector3Variable positionVariable;

        private void Update()
        {
            positionVariable.Value = transform.position;
        }
    }
}

