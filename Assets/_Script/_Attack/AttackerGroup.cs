using UnityEngine;

namespace DigiHero.Attack
{
    [AddComponentMenu("DigiHero/Attack/AttackerGroup")]
    public class AttackerGroup : MonoBehaviour
    {
        [Tooltip("The attackers in this group, will auto get from children if not set")]
        [SerializeField] private Attacker[] attackers;

        private void Awake()
        {
            if (attackers == null || attackers.Length == 0)
                attackers = GetComponentsInChildren<Attacker>();

            if (attackers == null || attackers.Length == 0)
            {
                Debug.LogError("AttackerGroup: No attacker found");
                return;
            }

            for (int i = 0; i < attackers.Length; i++)
            {
                attackers[i].isCanAttack = false;
            }
        }

        private void Update()
        {
            for (int i = 0; i < attackers.Length; i++)
            {
                attackers[i].isCanAttack = false;
            }
            for (int i = 0; i < attackers.Length; i++)
            {
                if (attackers[i].IsHavingTarget)
                {
                    attackers[i].isCanAttack = true;
                    break;
                }
            }
        }
    }
}