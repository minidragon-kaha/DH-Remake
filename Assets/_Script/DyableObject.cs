using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DigiHero
{
    public class DyableObject : MonoBehaviour
    {
        [SerializeField] private StatsContainer statsContainer;
        [SerializeField] private MotionApplier motionApplier;
        [SerializeField] private Animator animator;
        [SerializeField] private string diedStateName;
        [SerializeField] private AudioClip diedClip;
        [SerializeField] private UnityEvent onDied;
        [SerializeField] private UnityEvent onRevived;

        private int diedStateHash;

        private void Start()
        {
            diedStateHash = Animator.StringToHash(diedStateName);
        }

        private readonly object motionApplierLocker = new object();
        private bool isDied = false;

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

                onDied.Invoke();
            }
            else if (cur > 0 && isDied)
            {
                motionApplier.Resume(motionApplierLocker);
                onRevived.Invoke();
            }
        }
    }
}
