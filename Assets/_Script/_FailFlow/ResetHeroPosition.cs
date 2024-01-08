using Obvious.Soap;
using UnityEngine;

namespace DigiHero.FailFlow
{
    [UnityEngine.AddComponentMenu("_DigiHero/FailFlow/ResetHeroPosition")]
    public class ResetHeroPosition : StepBase
    {
        [Tooltip("英雄")]
        [SerializeField] private ScriptableListGameObject heroGameObjectInstanceList;
        [Tooltip("英雄初始位置")]
        [SerializeField] private Vector3Variable startPositionVariable;

        public override void Process(System.Action onEnded)
        {
            Debug.Log("TODO: reset hero's stats here");

            for (int i = 0; i < heroGameObjectInstanceList.Count; i++)
            {
                heroGameObjectInstanceList[i].transform.position = startPositionVariable.Value;
            }

            onEnded?.Invoke();
        }
    }
}
