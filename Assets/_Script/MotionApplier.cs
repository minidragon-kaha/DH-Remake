using UnityEngine;
using Obvious.Soap;
using System.Collections.Generic;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/MotionApplier")]
    public class MotionApplier : MonoBehaviour
    {
        [Tooltip("移動動量")]
        [SerializeField] private Vector3Reference movingVector;
        [Tooltip("數值容器")]
        [SerializeField] private StatsContainer statsContainer;

        private List<object> pauseLockers = new List<object>();

        public void UpdateMovingVector(Vector3 movingVector)
        {
            this.movingVector.Value = movingVector;
        }

        public void Pause(object locker)
        {
            pauseLockers.Add(locker);
        }

        public void Resume(object locker)
        {
            if (pauseLockers.Contains(locker))
                pauseLockers.Remove(locker);
        }

        private void Update()
        {
            if (pauseLockers.Count > 0)
                return;

            transform.position += movingVector.Value * statsContainer.MoveSpeed * Time.deltaTime;
        }
    }
}