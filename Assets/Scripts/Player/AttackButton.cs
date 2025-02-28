using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AttackButton : MonoBehaviour
{
    [SerializeField] GameObject Cooldown_Go;
    [SerializeField] Image Cooldown_Img;
    [SerializeField] TextMeshProUGUI Cooldown_Text;
    public bool IsAbilityOnCooldown = false;
    public void StartCoolDown(float cd_duration)
    {
        Cooldown_Go.gameObject.SetActive(true);
        Cooldown_Img.fillAmount = 1;

        StartCoroutine(Cooldown_Coro(cd_duration));
    }


    IEnumerator Cooldown_Coro(float cd_Duration)
    {
        IsAbilityOnCooldown = true;
        float cd_Timer = cd_Duration;

        while (cd_Timer > 0)
        {
            Cooldown_Text.text = $"{cd_Timer:F1}s";
            Cooldown_Img.fillAmount = cd_Timer / cd_Duration;
            yield return null;
            cd_Timer -= Time.deltaTime;
        }
        IsAbilityOnCooldown = false;
        Cooldown_Go.gameObject.SetActive(false);
    }
}
