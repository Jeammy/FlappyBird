using System;
using UnityEngine;

namespace Game
{
    public class Background : MonoBehaviour
    {
        [Header("Visuals")] [SerializeField] private Sprite sprite = null;
        [Range(0,100)] [SerializeField] private uint nbTiles  = 3;
        [SerializeField] private string sortingLayerName = "Default";
        [Header("Behaviour")] [Range(0,100)] [SerializeField] private float speed  = 0.5f;

        private Vector2 tileSize;
        private Vector3 initialPosition;
        private float offset;


        private void Start()
        {
            tileSize = sprite.bounds.size;
            for (uint i = 0; i < nbTiles; i++)
            {
                var tile = new GameObject(name: i.ToString());
                tile.transform.parent = transform;
                tile.transform.localPosition = tileSize.x * i * Vector3.right; 
                
                var spriteRenderer = tile.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = sprite;
                spriteRenderer.sortingLayerName = sortingLayerName;
            }
            
            //old
            //Appellé lorsque que le composant est créer
//            var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
//            Debug.Assert(condition: spriteRenderer != null, message: "no child sprite");
//            
//            tileSize = spriteRenderer.size;
              initialPosition = transform.position;
              offset = 0f;
        }

        private void Update()
        {
            offset = (offset + (speed * Time.deltaTime)) % tileSize.x;
            transform.position = initialPosition + Vector3.left * offset;
            
            //old
            //Appelé  à chaques frames.
            // Une translation
            /*if (transform.position.x <= -transform.localScale.x * 2)
            {
                var position = transform.position;
                position.x = 0;
                transform.position = position;
            }
            else
            {
                var position = transform.position;
                position.x -= 1 * Time.deltaTime;
                transform.position = position;
            }*/
            //transform.Translate(translation: translationSpeed);
        }
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            var size = sprite == null? Vector3.one : sprite.bounds.size;
            var center = transform.position;
            
            Gizmos.DrawWireCube(center , size);
        }
#endif        
    }
}