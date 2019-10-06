using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    GameObject player;
    Image healthBar_Image;
    public bool takingDamage = false;

    // Start is called before the first frame update
    void Start()
    {
        healthBar_Image = this.GetComponent<Image>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        int hp = player.GetComponent<PlayerControl>().playerHealth;

        if (hp <= 0) { healthBar_Image.color = new Color(0f, 0f, 0f, 1f); }
        else if (hp <= 25) { healthBar_Image.color = new Color(1f, 0f, 0f, 1f); }
        else if (hp <= 50) { healthBar_Image.color = new Color(1f, 0.5f, 0f, 1f); }
        else if (hp <= 75) { healthBar_Image.color = new Color(1f, 1f, 0f, 1f); }
        else { healthBar_Image.color = new Color(0f, 1f, 1f, 1f); }
    }

    public IEnumerator ShakeHealth(int intensity = 10, int shakeAmt = 5) {
        float x = GetComponent<RectTransform>().anchoredPosition.x;
        float x_original = x;
        float y = GetComponent<RectTransform>().anchoredPosition.y;
        float y_original = y;

        for (int i = 0; i < shakeAmt; i++) {
            x = x_original + Random.Range(-intensity, intensity);
            y = y_original + Random.Range(-intensity, intensity);
            GetComponent<RectTransform>().anchoredPosition = new Vector2(x,y);
            yield return new WaitForSeconds(0.1f);
            Debug.Log("OG pos: " + x_original + " " + y_original);
        }
        GetComponent<RectTransform>().anchoredPosition = new Vector2(x_original, y_original);
        Debug.Log("OG pos: " + GetComponent<RectTransform>().anchoredPosition);
        takingDamage = false;

        yield return null;
    }

}
