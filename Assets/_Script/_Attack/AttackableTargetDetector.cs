using Obvious.Soap;
using UnityEngine;
using UnityEngine.Events;

namespace DigiHero.Attack
{
    [AddComponentMenu("_DigiHero/Attack/AttackableTargetDetector")]
    public class AttackableTargetDetector : MonoBehaviour
    {
        private AttackableTarget Target { get; set; }

        [Tooltip("目標陣營")]
        [SerializeField] private int targetCamp;
        [Tooltip("偵測範圍")]
        [SerializeField] private IntVariable detectRangeVariable;
        [Tooltip("攻擊目標列表，將會從中找出最近的目標")]
        [SerializeField] private ScriptableListAttackableObject attackableObjectList;
        [Tooltip("當目標更新時，會觸發此事件")]
        [SerializeField] private UnityEvent<AttackableTarget> onTargetUpdated;

        private void Update()
        {
            AttackableTarget tempTarget = null;
            for (int i = 0; i < attackableObjectList.Count; i++)
            {
                if (attackableObjectList[i].Camp == targetCamp)
                {
                    if (Vector3.Distance(transform.position, attackableObjectList[i].transform.position) > detectRangeVariable.Value)
                    {
                        continue;
                    }

                    if (tempTarget == null)
                    {
                        tempTarget = attackableObjectList[i];
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, attackableObjectList[i].transform.position) < Vector3.Distance(transform.position, tempTarget.transform.position))
                        {
                            tempTarget = attackableObjectList[i];
                        }
                    }
                }
            }

            if (tempTarget != Target)
            {
                Target = tempTarget;
                onTargetUpdated?.Invoke(Target);
            }
        }
    }
}
