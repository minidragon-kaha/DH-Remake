using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/DyableObject")]
    public class DyableObject : MonoBehaviour
    {
        [SerializeField] private StatsContainer statsContainer;
        [SerializeField] private MotionApplier motionApplier;
        [SerializeField] private Animator animator;
        [SerializeField] private string diedStateName;
        [SerializeField] private AudioClip diedClip;
        [SerializeField] private float delayDiedEventTime = 1f;
        [SerializeField] private UnityEvent onDied;
        [SerializeField] private float delayReviveEventTime = 1f;
        [SerializeField] private UnityEvent onRevived;

        private int diedStateHash;

        private void Start()
        {
            diedStateHash = Animator.StringToHash(diedStateName);
        }

        private readonly object motionApplierLocker = new object();
        private bool isDied = false;

        private float delayDiedEventTimer = 0f;
        private float delayReviveEventTimer = 0f;

        private void Update()
        {
            if (delayDiedEventTimer > 0f)
            {

                delayDiedEventTimer -= Time.deltaTime;
                if (delayDiedEventTimer <= 0f)
                {
                    delayDiedEventTimer = 0f;
                    onDied.Invoke();
                }
            }

            if (delayReviveEventTimer > 0f)
            {
                delayReviveEventTimer -= Time.deltaTime;
                if (delayReviveEventTimer <= 0f)
                {
                    delayReviveEventTimer = 0f;
                    onRevived.Invoke();
                }
            }
        }

        public void OnHpChanged(int cur)
        {
            if (cur <= 0 && !isDied)
            {
                if (animator != null && animator.HasState(0, diedStateHash))
                {
                    animator.Play(diedStateHash);
                }

                motionApplier.Pause(motionApplierLocker);

                if (diedClip != null)
                {
                    Debug.Log("Play died clip");
                }

                isDied = true;

                delayDiedEventTimer = delayDiedEventTime;
            }
            else if (cur > 0 && isDied)
            {
                motionApplier.Resume(motionApplierLocker);
                delayReviveEventTimer = delayReviveEventTime;
            }
        }
    }
}
