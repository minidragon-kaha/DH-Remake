using Obvious.Soap;
using UnityEngine;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/AttackableTarget")]
    public class AttackableTarget : MonoBehaviour
    {
        public int Camp { get { return camp; } }
        [SerializeField] private int camp;
        [SerializeField] private ScriptableListAttackableObject attackableObjectList;

        private void OnEnable()
        {
            attackableObjectList.Add(this);
        }

        private void OnDisable()
        {
            attackableObjectList.Remove(this);
        }
    }
}