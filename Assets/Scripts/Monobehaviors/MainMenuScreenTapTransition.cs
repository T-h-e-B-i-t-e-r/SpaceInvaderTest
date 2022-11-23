using Data;
using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Monobehaviors
{
    public class MainMenuScreenTapTransition : MonoBehaviour, IPointerDownHandler
    {
        [Inject] private SceneLoader _sceneLoader;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _sceneLoader.LoadScene(SceneConstants.GameplayScene);
        }
    }
}
