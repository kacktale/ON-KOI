using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToAttack : MonoBehaviour
{
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<Player>();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LightAttack()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
}
