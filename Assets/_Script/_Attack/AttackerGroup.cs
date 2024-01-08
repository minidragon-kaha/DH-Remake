using UnityEngine;

namespace DigiHero.Attack
{
    [AddComponentMenu("_DigiHero/Attack/AttackerGroup")]
    public class AttackerGroup : MonoBehaviour
    {
        [Tooltip("攻擊者列表，所有攻擊者中同時只會有一個攻擊者攻擊，排在前面的攻擊者優先攻擊")]
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