using UnityEngine;
using Obvious.Soap;

namespace DigiHero.Initial
{
    public class SetUpPlayer : StepBase
    {
        [SerializeField] private StatsContainer heroPrefab;
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
