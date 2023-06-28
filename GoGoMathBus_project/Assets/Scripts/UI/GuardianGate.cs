using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.TextCore.Text;
using UnityEngine.Windows;

public class GuardianGate : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI infoText;
    [SerializeField] TextMeshProUGUI passwordText;
    [SerializeField] float delayTime;
    [SerializeField] string loadSceneName;
    [SerializeField] string normalText;
    [SerializeField] string wrongText;

    WaitForSeconds delay;
    Coroutine verifyCo;
    string inputPassword = "";
    string tempPassword = "123456"; //¿”Ω√

    void Start()
    {
        delay = new WaitForSeconds(delayTime);
    }

    public void OnNumberBtn(int num)
    {
        if (inputPassword.Length < tempPassword.Length)
        {
            inputPassword += num;
            passwordText.text += "*";

            if (inputPassword == tempPassword)
            {
                //æ¿≥—±Ë
                if(verifyCo != null)
                    StopCoroutine(verifyCo);
                verifyCo = StartCoroutine(Verify(true));
            }
            else if (inputPassword.Length == tempPassword.Length)
            {
                //∆≤∑»¿Ω «•Ω√
                if (verifyCo != null)
                    StopCoroutine(verifyCo);
                verifyCo = StartCoroutine(Verify(false));
            }
        }
    }

    IEnumerator Verify(bool state)
    {
        if (state)
            GameManager.Instance.LoadScene(loadSceneName, true);
        else
        {
            infoText.text = wrongText;
            inputPassword = "";
            passwordText.text = "";
            yield return delay;
            infoText.text = normalText;
        }
        verifyCo = null;
    }
}
