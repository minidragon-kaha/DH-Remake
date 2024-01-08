using UnityEngine;
using Obvious.Soap;

namespace DigiHero.Initial
{
    [UnityEngine.AddComponentMenu("_DigiHero/Initial/SetUpPlayer")]
    public class SetUpPlayer : StepBase
    {
        [Tooltip("英雄prefab")]
        [SerializeField] private StatsContainer heroPrefab;
        [Tooltip("英雄初始位置")]
        [SerializeField] private Vector3Variable startPositionVariable;

        public override void Process(System.Action onEnded)
        {
            StatsContainer hero = Instantiate(heroPrefab, startPositionVariable.Value, Quaternion.identity);

            Debug.Log("TODO: Set up hero's stats here");

            hero.gameObject.name = "Hero";
            onEnded?.Invoke();
        }
    }
}
