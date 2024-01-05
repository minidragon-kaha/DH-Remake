using UnityEngine;
using Obvious.Soap;

namespace DigiHero
{
    public class StatsContainer : MonoBehaviour
    {
        [SerializeField] private IntVariable moveSpeed;

        public float MoveSpeed { get { return (float)moveSpeed.Value; } }
    }
}
