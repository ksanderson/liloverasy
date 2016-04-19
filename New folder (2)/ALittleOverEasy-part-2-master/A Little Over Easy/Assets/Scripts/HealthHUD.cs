using UnityEngine;
using System.Collections;

using System.Collections.Generic;

public class HealthHUD : MonoBehaviour
{
    [SerializeField]
    private GameObject LevelManager;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject deathScreen;
    [SerializeField]
    private GameObject deathText;
    [SerializeField]
    private GameObject egg;
    [SerializeField]
    private GameObject crackedEgg;
    private Stack<GameObject> eggs;
    private int localHealth = 0;

    // Use this for initialization
    void Start ()
    {
        eggs = new Stack<GameObject>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        int playersHealth = player.GetComponent<PlayerHealth>().health;
        int healthDelta = playersHealth - localHealth;
        if (healthDelta > 0)
        {
            GetHealth();
        }
        else if (healthDelta < 0)
        {
            LoseHealth();
        }
        if (localHealth > 0)
        {
            deathScreen.SetActive(false);
            deathText.SetActive(false);
        }
        else
        {
            deathScreen.SetActive(true);
            deathText.SetActive(true);
            Destroy(player);
            StartCoroutine(Reload());
        }
    }

    void GetHealth()
    {
        RectTransform eggTrans;
        GameObject go;
        if (localHealth % 2 == 0)
        {
            go = crackedEgg;
            eggTrans = crackedEgg.GetComponent<RectTransform>();
        }
        else
        {
            go = egg;
            eggTrans = egg.GetComponent<RectTransform>();
            Destroy(eggs.Pop());
        }
        GameObject anEgg = Instantiate(go, new Vector2(transform.position.x + eggTrans.sizeDelta.x * eggs.Count + eggTrans.sizeDelta.x / 2, transform.position.y - eggTrans.sizeDelta.y / 2), Quaternion.identity) as GameObject;
        anEgg.transform.SetParent(this.transform);
        eggs.Push(anEgg);
        localHealth++;
    }

    void LoseHealth()
    {
        Destroy(eggs.Pop());
        if (localHealth % 2 == 0)
        {
            RectTransform eggTrans = crackedEgg.GetComponent<RectTransform>();
            GameObject anEgg = Instantiate(crackedEgg, new Vector2(transform.position.x + eggTrans.sizeDelta.x * eggs.Count + eggTrans.sizeDelta.x / 2, transform.position.y - eggTrans.sizeDelta.y / 2), Quaternion.identity) as GameObject;
            anEgg.transform.SetParent(this.transform);
            eggs.Push(anEgg);
        }
        localHealth--;
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(3.0f);
        LevelManager.GetComponent<LevelManager>().LoadScene("menu");
    }
}
