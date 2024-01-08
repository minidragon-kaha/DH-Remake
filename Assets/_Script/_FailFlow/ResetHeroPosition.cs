using Obvious.Soap;
using UnityEngine;

namespace DigiHero.FailFlow
{
    [UnityEngine.AddComponentMenu("DigiHero/FailFlow/ResetHeroPosition")]
    public class ResetHeroPosition : StepBase
    {
        [SerializeField] private ScriptableListGameObject heroGameObjectInstanceList;
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
