using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour

{
    private bool CanTouchThis = true;

    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D coll)
    {
   //when the player touches the coin it increments score plays a sound and destroys itself
        if (coll.gameObject.tag == "Player" && CanTouchThis == true)
        {

            MainManager.Instance.CoinSound();
            Destroy(gameObject);



            CanTouchThis = false;
            MainManager.Instance.currentScore += 100;

        }
    }
}
