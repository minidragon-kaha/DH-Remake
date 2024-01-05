using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DigiHero
{
    public class Attacker : MonoBehaviour
    {
        [System.Serializable]
        private class AttackPoint
        {
            [Range(0f, 1f)]
            public float normalizedTime;

            public AudioClip audioClip;
        }

        [SerializeField] private Animator animator;
        [SerializeField] private string attackStateName;
        [SerializeField] private string idleStateName;
        [SerializeField] private AttackPoint[] attackPoints;
        [SerializeField] private float attackCooldown;

        private int idleStateHash = -1;
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

            if (animator.HasState(0, idleStateHash))
            {
                Debug.LogError("Attacker: No idle state found");
                return;
            }

            idleStateHash = Animator.StringToHash(idleStateName);
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
            if (target == null)
            {
                if (isPlayingAttackAnimation)
                {
                    isPlayingAttackAnimation = false;
                    animator.Play(idleStateHash);
                }
                return;
            }

            if (attackStateHash == -1 || idleStateHash == -1)
            {
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

            if (!isPlayingAttackAnimation)
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
                    animator.Play(idleStateHash);
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