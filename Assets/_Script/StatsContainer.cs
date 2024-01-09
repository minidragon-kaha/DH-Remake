using UnityEngine;
using Obvious.Soap;
using UnityEngine.Events;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/StatsContainer")]
    public class StatsContainer : MonoBehaviour
    {
        [Tooltip("移動速度")]
        [SerializeField] private IntReference moveSpeed;

        public float MoveSpeed { get { return (float)moveSpeed.Value; } }

        [Tooltip("生命值")]
        [SerializeField] private IntReference hp;
        public int Hp { get { return hp.Value; } }

        [Tooltip("當生命值更新時，會觸發此事件")]
        [SerializeField] private UnityEvent<int> onHpChanged;

        private void Start()
        {
            if (hp.Variable != null)
            {
                hp.Variable.OnValueChanged += OnHpChanged;
            }
            else
            {
                hp.OnLocalValueChanged += OnHpChanged;
            }
        }

        private void OnHpChanged(int newValue)
        {
            onHpChanged.Invoke(newValue);
        }
    }
}
