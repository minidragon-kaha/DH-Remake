using Obvious.Soap;
using UnityEngine;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/Rotater")]
    public class Rotater : MonoBehaviour
    {
        [Tooltip("旋轉速度")]
        [SerializeField] private FloatReference rotateSpeed;
        [Tooltip("移動動量")]
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