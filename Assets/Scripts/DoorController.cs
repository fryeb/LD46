using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DoorController : MonoBehaviour
{
    public string sceneName = "Level0";

    private bool targetEntered = false;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.GetComponent<TargetController>() != null)
        {
            targetEntered = true;
        }

        if (col.GetComponent<PlayerController>() != null && targetEntered)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
    }
}
