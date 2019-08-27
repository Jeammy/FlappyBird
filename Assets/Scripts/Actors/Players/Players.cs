using System;
using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(FlapMover))]
    
    public class Players : MonoBehaviour
    {
        [SerializeField] private float targetMainMenuHeight =0f;
        
        private GameController gameController;
        private FlapMover flapMover;
        private HazardSensor sensor;
        private PlayerDeathEventChannel playerDeathEventChannel;
        
        private void Awake()
        {
            //Appellé lorsque que le composant est créer
            gameController = Finder.GameController;
            playerDeathEventChannel = Finder.PlayerDeathEventChannel;
            flapMover = GetComponent<FlapMover>();
            sensor = GetComponentInChildren<HazardSensor>();
        }
        
        private void Update()
        {
            //Appelé  à chaques frames.
            var gameState = gameController.GameState;
            
            if (gameState == GameState.MainMenu)
            {
                if (transform.position.y < targetMainMenuHeight) 
                    flapMover.Flap();
            }
            else if (gameState == GameState.Playing)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    flapMover.Flap();
            }
        }
        
        private void OnEnable()
        {
            //appelé lorsque le composant est activé. avant start.
            sensor.OnHit += OnHit;
        }

        private void OnDisable()
        {
            //Appelé lorsque le composant est disactivé, mais pas lorsqu'il est désactivé au depart.
            sensor.OnHit -= OnHit;
        }

        private void OnHit(GameObject other)
        {
            Die();
        }

        [ContextMenu("Die")]
        private void Die()
        {
            Destroy(gameObject);
            playerDeathEventChannel.NotifyPlayerDeath();
        }

        private void Start()
        {
            //Appelé lorsque le composant est utilisé a la premiere frame
        }


        private void FixedUpdate()
        {
            //Appelé à interval régulier, lorsque l'engin de physique se met à jour.
        }

        private void OnDestroy()
        {
            //Appelé lorsque le composant est détruit. Cela ne veut pas dire que le gameObject est détruit
        }
    }
}