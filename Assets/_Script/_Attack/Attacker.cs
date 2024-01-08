using UnityEditor.EditorTools;
using UnityEngine;

namespace DigiHero.Attack
{
    [AddComponentMenu("_DigiHero/Attack/Attacker")]
    public class Attacker : MonoBehaviour
    {
        public bool IsHavingTarget { get; private set; } = false;
        public bool isCanAttack = true;

        [System.Serializable]
        private class AttackPoint
        {
            [Tooltip("攻擊點的正規化時間，範圍為 0 ~ 1")]
            [Range(0f, 1f)]
            public float normalizedTime;
            [Tooltip("攻擊點的音效")]
            public AudioClip audioClip;
        }

        [Tooltip("攻擊動畫播放器")]
        [SerializeField] private Animator animator;
        [Tooltip("攻擊動畫的名稱")]
        [SerializeField] private string attackStateName;
        [Tooltip("攻擊點")]
        [SerializeField] private AttackPoint[] attackPoints;
        [Tooltip("攻擊冷卻時間")]
        [SerializeField] private float attackCooldown;

        private int attackStateHash = -1;

        private AttackableTarget target;

        private void Awake()
        {
            if (animator == null && !TryGetComponent(out animator))
            {
                Debug.LogError("Attacker: No animator found");
                return;
            }

            if (animator.HasState(0, attackStateHash))
            {
                Debug.LogError("Attacker: No attack state found");
                return;
            }

            attackStateHash = Animator.StringToHash(attackStateName);
        }

        // TODO: when attackable target is null while attacking ?????
        public void UpdateTarget(AttackableTarget attackableTarget)
        {
            if (target == attackableTarget)
            {
                return;
            }

            target = attackableTarget;
            curAttackPointIndex = 0;
            attackTimer = 0f;
            isPlayingAttackAnimation = false;
        }

        private float cooldownTimer = 0f;
        private float attackTimer = 0f;
        private int curAttackPointIndex = 0;
        private bool isPlayingAttackAnimation = false;

        private void Update()
        {
            IsHavingTarget = (target != null);

            if (target == null || !isCanAttack)
            {
                if (isPlayingAttackAnimation)
                {
                    isPlayingAttackAnimation = false;
                    attackTimer = 0f;
                }
                return;
            }

            if (cooldownTimer >= 0f)
            {
                cooldownTimer -= Time.deltaTime;

                if (cooldownTimer <= 0f)
                {
                    curAttackPointIndex = 0;
                    attackTimer = 0f;
                }

                return;
            }

            if (!isPlayingAttackAnimation && attackStateHash != -1)
            {
                isPlayingAttackAnimation = true;
                animator.Play(attackStateHash);
            }

            attackTimer += Time.deltaTime;
            if (attackTimer >= attackPoints[curAttackPointIndex].normalizedTime * animator.GetCurrentAnimatorStateInfo(0).length)
            {
                Attack(curAttackPointIndex);
                curAttackPointIndex++;

                if (curAttackPointIndex >= attackPoints.Length)
                {
                    isPlayingAttackAnimation = false;
                    cooldownTimer = attackCooldown;
                }
            }
        }

        private void Attack(int index)
        {
            Debug.Log("Attck " + index + " play " + (attackPoints[index].audioClip == null ? "null" : attackPoints[index].audioClip.name));
        }
    }
}