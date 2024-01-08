using Obvious.Soap;
using UnityEngine;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/Rotater")]
    public class Rotater : MonoBehaviour
    {
        [SerializeField] private FloatVariable rotateSpeed;
        [SerializeField] private Vector3Reference targetForward;

        public void UpdateMovingVector(Vector3 movingVector)
        {
            targetForward.Value = movingVector;
        }

        private void Update()
        {
            if (targetForward == Vector3.zero)
                return;

            transform.forward = Vector3.Lerp(transform.forward, targetForward, rotateSpeed.Value * Time.deltaTime);
        }
    }
}