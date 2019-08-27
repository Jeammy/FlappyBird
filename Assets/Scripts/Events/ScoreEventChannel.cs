using UnityEngine;

namespace Game
{
    public class ScoreEventChannel : MonoBehaviour
    {
        public event ScoreEventHandler OnScored;
        
        public void NotifyScored()
        {
            if (OnScored != null) OnScored();
        }
    }

    public delegate void ScoreEventHandler();
}