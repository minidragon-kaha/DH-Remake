using UnityEngine;
using Obvious.Soap;

namespace DigiHero
{
    public class MotionApplier : MonoBehaviour
    {
        [SerializeField] private Vector3Variable movingVector;

        private void Update()
        {
            transform.position += movingVector.Value * Time.deltaTime;
        }
    }
}