using Obvious.Soap;
using UnityEngine;
using UnityEngine.AI;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/PositionApplier")]
    public class PositionApplier : MonoBehaviour
    {
        [Tooltip("位置資訊來源")]
        [SerializeField] private Vector3Variable positionVariable;
        [Tooltip("偏移量")]
        [SerializeField] private Vector3Reference offset;
        [Tooltip("是否使用導航網格")]
        [SerializeField] private bool useNavmesh = false;

        private void Update()
        {
            transform.position = positionVariable.Value + offset.Value;
            if (useNavmesh
                && NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 100, NavMesh.AllAreas))
            {
                transform.position = hit.position;
            }
        }
    }
}