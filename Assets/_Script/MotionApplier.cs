using UnityEngine;
using Obvious.Soap;

namespace DigiHero
{
    public class MotionApplier : MonoBehaviour
    {
        [SerializeField] private Vector3Reference movingVector;

        public void UpdateMovingVector(Vector3 movingVector)
        {
            this.movingVector.Value = movingVector;
        }

        private void Update()
        {
            transform.position += movingVector.Value * Time.deltaTime;
        }
    }
}