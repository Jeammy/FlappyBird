using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class PipeSpawnPoint : MonoBehaviour
    {
        [SerializeField] private int spawnDelay = 4;
        
        private PrefabFactory prefabFactory;
        private GameController gameController;
        private void Awake()
        {
            gameController = Finder.GameController;
            prefabFactory = Finder.PrefabFactory;
        }

        private void OnEnable()
        {
            StartCoroutine(SpawnPipeRoutine());
        }

        private IEnumerator SpawnPipeRoutine()
        {
            while (isActiveAndEnabled)
            {
                yield return new WaitForSeconds(spawnDelay);

                if (gameController.GameState != GameState.MainMenu && gameController.GameState != GameState.GameOver)
                    prefabFactory.CreatePipePair(transform.position, Quaternion.identity, null);
            }
        }
    }
}