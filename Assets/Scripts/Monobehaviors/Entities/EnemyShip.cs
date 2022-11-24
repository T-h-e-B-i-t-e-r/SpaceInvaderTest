using System;
using UnityEngine;
using Zenject;

namespace Monobehaviors.Entities
{
    public class EnemyShip : MonoBehaviour
    {
        //[Inject]
        
        
        void Update()
        {
        
        }

        private void OnCollisionEnter(Collision collision)
        {
            gameObject.SetActive(false);
        }
    }
}
