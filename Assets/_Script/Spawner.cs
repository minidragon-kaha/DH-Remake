using UnityEngine;

namespace DigiHero
{
    [AddComponentMenu("_DigiHero/Spawner")]
    public class Spawner : MonoBehaviour
    {
        [Tooltip("生成間隔")]
        [SerializeField] private float spawnTime;
        [Tooltip("生成物件Prefab")]
        [SerializeField] private GameObject spawnPrefab;
        [Tooltip("生成範圍")]
        [SerializeField] private float spawnRange;
        [Tooltip("生成數量")]
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