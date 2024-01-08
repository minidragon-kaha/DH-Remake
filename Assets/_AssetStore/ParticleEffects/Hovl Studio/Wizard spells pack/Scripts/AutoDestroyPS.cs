using UnityEngine;

//This code destroys the particle's GameObject once it's Start Time is over.
public class AutoDestroyPS : MonoBehaviour
{
    private float timeLeft;

    private void Start()
    {
        ParticleSystem system = GetComponent<ParticleSystem>();
        var main = system.main;
        timeLeft = main.startLifetimeMultiplier + main.duration;
        Destroy(gameObject, timeLeft);
    }
}