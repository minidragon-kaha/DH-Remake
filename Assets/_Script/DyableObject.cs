using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/DyableObject")]
    public class DyableObject : MonoBehaviour
    {
        [Tooltip("數值容器")]
        [SerializeField] private StatsContainer statsContainer;
        [Tooltip("移動動量應用器")]
        [SerializeField] private MotionApplier motionApplier;
        [Tooltip("動畫控制器")]
        [SerializeField] private Animator animator;
        [Tooltip("死亡狀態名稱")]
        [SerializeField] private string diedStateName;
        [Tooltip("死亡音效")]
        [SerializeField] private AudioClip diedClip;
        [Tooltip("死亡事件延遲時間")]
        [SerializeField] private float delayDiedEventTime = 1f;
        [Tooltip("死亡事件，會在死亡延遲時間後觸發")]
        [SerializeField] private UnityEvent onDied;
        [Tooltip("復活事件延遲時間")]
        [SerializeField] private float delayReviveEventTime = 1f;
        [Tooltip("復活事件，會在復活延遲時間後觸發")]
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
