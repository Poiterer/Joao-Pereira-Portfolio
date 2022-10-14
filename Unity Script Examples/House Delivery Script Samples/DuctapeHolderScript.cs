using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DuctapeHolderScript : MonoBehaviour
{
    [Header("Ductape Prefab")]

    [SerializeField] private GameObject ductTapePrefab;

    [Space]
    [Header("References To Crate UI")]

    [SerializeField] private Image rechargeIcon;
    [SerializeField] private GameObject rechargeObject;
    [SerializeField] private TextMeshProUGUI chargesText;
    private bool usingIcon = false;


    [Space]
    [Header("Spawn Settings")]

    private int currentCharges = 0;
    [SerializeField] private int maxCharges = 3;
    [SerializeField] private float cooldownTime = 45f;
    
    [Space]
    [Header("Object Parent")]

    [SerializeField] private GameObject parentInHierarchy;


    public bool isRecharging { get; private set; }
    

    private void Start()
    {
        currentCharges = maxCharges;
        chargesText.text = currentCharges.ToString();
        isRecharging = false;

    }

    public GameObject Interact(GameObject character)
    {
        if (currentCharges > 0 && ductTapePrefab != null)
        {
            var instance = Instantiate(ductTapePrefab, parentInHierarchy.transform);
            instance.GetComponent<PickupComponent>().Pickup(character);
            currentCharges--;
            StartCoroutine(ChargeCooldown());
            return instance;
        }
        else
        {
            Debug.Log("No Charges, Wait Longer");
            return null;
        }
    }

    public void SetIconState(bool state)
    {
        rechargeObject.SetActive(state);
    }

    private IEnumerator ChargeCooldown()
    {
        float timer = 0f;

        bool isOwiningIcon = false; 

        rechargeIcon.gameObject.SetActive(true);

        chargesText.text = currentCharges.ToString();

        isRecharging = true;

        while (true)
        {
            if (usingIcon == false)
            {
                isOwiningIcon = true;
                rechargeObject.gameObject.SetActive(true);
                usingIcon = true;
            }

            if (isOwiningIcon)
            {
                rechargeIcon.fillAmount = timer / cooldownTime;
            }
            
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;

            if (timer >= cooldownTime)
            {
                if (currentCharges < maxCharges)
                {
                    currentCharges++;
                    chargesText.text = currentCharges.ToString();

                }

                if (isOwiningIcon)
                {
                    rechargeObject.gameObject.SetActive(false);
                    usingIcon = false;
                }
                break;
            }
        }

        isRecharging = false;
    }
}
