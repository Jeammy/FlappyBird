using UnityEngine;

namespace Game
{
    public class PlayerSensor : MonoBehaviour
    {
        public event SensorEventHandler OnHit;
        private void OnTriggerEnter2D(Collider2D other)
        {
            var stimuli = other.gameObject.GetComponent<PlayerStimuli>();
            if (stimuli != null)
            {
                var parent = other.transform.parent;
                var gameObject = parent != null ? parent.gameObject : other.gameObject;
            
                if (OnHit != null) OnHit(other.gameObject);
                //Debug.Log("collision with" + other.gameObject.name);
            }
        }
    }
}