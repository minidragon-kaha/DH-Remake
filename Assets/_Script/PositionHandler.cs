using UnityEngine;
using Obvious.Soap;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/PositionHandler")]
    public class PositionHandler : MonoBehaviour
    {
        [Tooltip("寫入位置資訊")]
        [SerializeField] private Vector3Variable positionVariable;

        private void Update()
        {
            positionVariable.Value = transform.position;
        }
    }
}

