using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game
{
    [RequireComponent(typeof(TranslateMover))]
    public class Pipe : MonoBehaviour
    {
        [Header("Behaviour")]
        [SerializeField] private float delayInSeconds = 10f;
        [Header("Position")]
        [SerializeField] private float minY = -1f;
        [SerializeField] private float maxY = 1f;
        
        
        private GameController gameController;
        private TranslateMover mover;
        private PlayerSensor playerSensor;
        private ScoreEventChannel scoreEventChannel;
        private void Awake()
        {
            gameController = Finder.GameController;
            mover = GetComponent<TranslateMover>();
            playerSensor = GetComponentInChildren<PlayerSensor>();
            scoreEventChannel = Finder.ScoreEventChannel;
        }

        private void Start()
        {
            transform.Translate(Vector3.up * Random.Range(minY,maxY));
            
        }

        private void Update()
        {
            if (gameController.GameState == GameState.Playing)
            {
                mover.Move();
            }
        }
        
        private void OnEnable()
        {
            StartCoroutine(DestroyPipeRoutine());

            playerSensor.OnHit += OnHit;
        }

        private void OnDisable()
        {
            playerSensor.OnHit -= OnHit;

        }

        private void OnHit(GameObject other)
        {
            scoreEventChannel.NotifyScored();
        }
        
        private IEnumerator DestroyPipeRoutine()
        {
            yield return new WaitForSeconds(delayInSeconds); 
            Destroy(gameObject);
        }
    }
}