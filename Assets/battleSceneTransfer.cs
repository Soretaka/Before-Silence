using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class battleSceneTransfer : MonoBehaviour
{
//     public string sceneToLoad;

//     public void OnTriggerEnter2D(Collider2D other) {
//         if(other.CompareTag("Player")){
//             SceneManager.LoadScene(sceneToLoad);
//         }
//     }
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            playerStorage.initialValue = playerPosition;
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
