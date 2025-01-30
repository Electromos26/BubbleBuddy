using System;
using System.Collections;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Managers
{
    public class SpawnManager : Singleton<SpawnManager>
    {
        [SerializeField] private float delay = 2f;

        private void Start()
        {
            delay = Random.Range(1f, delay);
        }
        

        public void SpawnBubble(Vector3 spawnPos, BubbleCollectible bubbleCollectableDrop, float spawnRadius)
        {
            StartCoroutine(SpawnDelay(spawnPos, bubbleCollectableDrop, spawnRadius));
        }

        private IEnumerator SpawnDelay(Vector3 spawnPos, BubbleCollectible bubbleCollectableDrop, float spawnRadius)
        {
            yield return new WaitForSeconds(delay);
            var randomPoint = Random.insideUnitCircle * spawnRadius;
            var spawnPosition = spawnPos + new Vector3(randomPoint.x, 0f, randomPoint.y);
            Instantiate(bubbleCollectableDrop, spawnPosition, Quaternion.identity);
        }
    }
}