using Data;
using Managers;
using UnityEngine;
using Zenject;

namespace Monobehaviors
{
    public class StartSceneEntry : MonoBehaviour
    {
        [Inject] private SceneLoader _loader;
        
        void Start()
        {
            _loader.LoadScene(GameConstants.MainMenuScene);
        }
    }
}
