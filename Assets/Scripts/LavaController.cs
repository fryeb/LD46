using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LavaController : MonoBehaviour
{
    void Start()
    {
        Debug.Assert(GetComponent<Collider2D>().isTrigger);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        TargetController target = col.GetComponent<TargetController>();
        if (target)
            target.Die();

        PlayerController player = col.GetComponent<PlayerController>();
        if (player)
        {
            GameObject cameraGO = Camera.main.gameObject;
            MenuController menu = cameraGO.GetComponentInChildren<MenuController>(true);
            menu.gameObject.SetActive(true);
        }
    }
}
