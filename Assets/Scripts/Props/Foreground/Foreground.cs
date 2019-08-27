using UnityEngine;

namespace Game
{
    public class Foreground : MonoBehaviour
    {
        //private SpriteRenderer spriteRenderer;

        //[SerializeField] private Vector2 translationSpeed = Vector2.left * Time.deltaTime;
        
        private void Awake()
        {
            //Appellé lorsque que le composant est créer
            //spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }
        
        private void Update()
        {
            //Appelé  à chaques frames.
            // Une translation
            if (transform.position.x <= -transform.localScale.x)
            {
                var position = transform.position;
                position.x = 0;
                transform.position = position;
            }
            else
            {
                var position = transform.position;
                position.x -= 3 * Time.deltaTime;
                transform.position = position;
            }
            //transform.Translate(translation: translationSpeed);
        }
    }
}