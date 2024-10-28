using UnityEngine;

namespace RPG.Combat
{
    public class CombatTarget : MonoBehaviour 
    {
        public void OnBeingHit() 
        {
            print("Ouch!");
        }
    }
}