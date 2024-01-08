using UnityEngine;

namespace DigiHero
{
    [AddComponentMenu("DigiHero/Spawner")]
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private float spawnTime;
        [SerializeField] private GameObject spawnPrefab;
        [SerializeField] private float spawnRange;
        [SerializeField] private int spawnAmount;

        private TaggedObject detectedObject;
        private float spawnTimer;

        public void UpdateDetectedObject(TaggedObject detectedObject)
        {
            if (this.detectedObject == detectedObject)
            {
                return;
            }

            this.detectedObject = detectedObject;
            spawnTimer = spawnTime;
        }

        private void Update()
        {
            if (detectedObject == null)
            {
                return;
            }

            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                spawnTimer = spawnTime;
                Spawn();
            }
        }

        private void Spawn()
        {
            for (int i = 0; i < spawnAmount; i++)
            {
                Vector3 spawnPosition = transform.position + UnityEngine.Random.insideUnitSphere * spawnRange;
                spawnPosition.y = 0;
                Instantiate(spawnPrefab, spawnPosition, Quaternion.identity);
            }
        }
    }
}