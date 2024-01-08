using Obvious.Soap;
using UnityEngine;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/PositionApplier")]
    public class PositionApplier : MonoBehaviour
    {
        [Tooltip("位置資訊來源")]
        [SerializeField] private Vector3Variable positionVariable;
        [Tooltip("偏移量")]
        [SerializeField] private Vector3Reference offset;

        private void Update()
        {
            transform.position = positionVariable.Value + offset.Value;
        }
    }
}