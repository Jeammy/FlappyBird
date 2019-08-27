using System;
using System.Collections;
using UnityEngine;

namespace Game
{
    public class PipeSpawnPoint : MonoBehaviour
    {
        [SerializeField] private GameObject pipePrefab;
        [SerializeField] private int spawnDelay = 4;

        private GameController gameController;
        private void Awake()
        {
            gameController = Finder.GameController;
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
                
                if(gameController.GameState != GameState.MainMenu && gameController.GameState !=GameState.GameOver)
                    Instantiate(pipePrefab, transform.position, Quaternion.identity);
            }
        }
    }
}