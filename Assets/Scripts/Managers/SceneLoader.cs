using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneLoader
    {
        // TODO?: handle disposal of scenes in memory, save for now
        private List<AsyncOperationHandle<SceneInstance>> _handles = new List<AsyncOperationHandle<SceneInstance>>();
        
        public void LoadScene(string sceneAddressableKey)
        {
            Addressables.LoadSceneAsync(sceneAddressableKey, LoadSceneMode.Single).Completed += OnSceneLoaded;
        }

        private void OnSceneLoaded(AsyncOperationHandle<SceneInstance> asyncOperationHandle)
        {
            if (asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
            {
                Debug.Log("Scene Loaded successfully");
                if (!_handles.Contains(asyncOperationHandle))
                {
                    _handles.Add(asyncOperationHandle);        
                }
            }
        }
    }
}
