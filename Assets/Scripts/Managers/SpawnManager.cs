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
        [SerializeField][Min(0.1f)] private float bubbleDelayMin = 2f;
        [SerializeField][Min(1f)] private float bubbleDelayMax = 5f;
        
        [SerializeField] private BubbleCollectible bubbleCollectableDrop;
        [SerializeField] private float spawnRadius;
        
        
        private float delay;
        private void Start()
        {
            delay = Random.Range(bubbleDelayMin, bubbleDelayMax);
        }

        private void OnEnable()
        {
            Event.OnEnemyDied += SpawnBubble;
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
           // var coinToss = Random.Range(0, 2);
           // if (coinToss == 0)
                Instantiate(bubbleCollectableDrop, spawnPosition, Quaternion.identity);
        }
    }
}