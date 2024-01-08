using UnityEngine;
using Obvious.Soap;
using UnityEngine.Events;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/StatsContainer")]
    public class StatsContainer : MonoBehaviour
    {
        [SerializeField] private IntVariable moveSpeed;

        public float MoveSpeed { get { return (float)moveSpeed.Value; } }

        [SerializeField] private IntVariable hp;
        public int Hp { get { return hp.Value; } }

        [SerializeField] private UnityEvent<int> onHpChanged;

        private void Start()
        {
            hp.OnValueChanged += OnHpChanged;
        }

        private void OnHpChanged(int newValue)
        {
            onHpChanged.Invoke(newValue);
        }
    }
}
