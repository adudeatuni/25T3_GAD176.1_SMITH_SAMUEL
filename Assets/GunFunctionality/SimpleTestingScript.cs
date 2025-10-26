using UnityEngine;
using FPSystem.Core;

public class SimpleTestingScript : FPSystemCore
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack(true);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }


    }
}
