using System;
using System.Collections;
using Enemy;
using Events;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Managers
{
    public class SpawnManager : Singleton<SpawnManager>
    {
        public GameEvent Event;
        [SerializeField] [Min(0.1f)] private float bubbleDelayMin = 2f;
        [SerializeField] [Min(1f)] private float bubbleDelayMax = 5f;
        [SerializeField] private int maxCollectables = 5;
        [SerializeField] private BubbleCollectible bubbleCollectableDrop;

        [Header("Spawn Settings")] [SerializeField]
        private float spawnRadius;

        [SerializeField][Range(0.1f,1f)] private float spawnProbability = 0.5f;
        [SerializeField] private float probabilityStartWave = 5f;


        private float delay;
        private int activeCollectables;

        private void Start()
        {
            delay = Random.Range(bubbleDelayMin, bubbleDelayMax);
        }

        private void OnEnable()
        {
            Event.OnEnemyDied += SpawnBubble;
            Event.OnCollectablePickup += Collected;
        }

        private void OnDisable()
        {
            Event.OnEnemyDied -= SpawnBubble;
            Event.OnCollectablePickup -= Collected;
        }


        private void SpawnBubble(EnemyBase enemyBase)
        {
            var spawnPos = enemyBase.transform.position;
            StartCoroutine(SpawnDelay(spawnPos, bubbleCollectableDrop, spawnRadius));
        }

        private IEnumerator SpawnDelay(Vector3 spawnPos, BubbleCollectible bubbleCollectableDrop, float spawnRadius)
        {
            yield return new WaitForSeconds(delay);
            var randomPoint = Random.insideUnitCircle * spawnRadius;
            var spawnPosition = spawnPos + new Vector3(randomPoint.x, 0f, randomPoint.y);

            var num = 0f;
            if (WaveManager.Instance.CurrentWave > probabilityStartWave)
            {
                num = Random.value;
            }

            if (num < spawnProbability && maxCollectables > activeCollectables)
            {
                Instantiate(bubbleCollectableDrop, spawnPosition, Quaternion.identity);
                activeCollectables++;
            }

            yield return null;
        }


        public void ReduceSpawnProbability()
        {
            if (WaveManager.Instance.CurrentWave > probabilityStartWave && spawnProbability > 0.15f)
                spawnProbability -= 0.05f;
        }
        
        private void Collected()
        {
           activeCollectables--;
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }
    }
}