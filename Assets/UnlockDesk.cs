using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class UnlockDesk : MonoBehaviour
{
    [SerializeField] private GameObject unlockProgressObj;
       [SerializeField] private GameObject newDesk;
       [SerializeField] private Image progressBar;
       [SerializeField] private TextMeshProUGUI dollarAmount;
       [SerializeField] private int deskPrice,deskRemainPrice;
       [SerializeField] private float ProgressValue;
       public NavMeshSurface buildNavMesh;
    
    void Start()
    {
        dollarAmount.text = deskPrice.ToString("C0");
        deskRemainPrice = deskPrice;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerPrefs.GetInt("dollar") > 0)
        {
            ProgressValue = Mathf.Abs(1f - CalculateMoney() / deskPrice) ;

            if (PlayerPrefs.GetInt("dollar") >= deskPrice)
            {
                PlayerPrefs.SetInt("dollar", PlayerPrefs.GetInt("dollar") - deskPrice);

                deskRemainPrice = 0;
            }
            else
            {
                deskRemainPrice -= PlayerPrefs.GetInt("dollar");
                PlayerPrefs.SetInt("dollar", 0);
            }

            progressBar.fillAmount = ProgressValue;

            PlayerManager.PlayerManagerInstance.MoneyCounter.text = PlayerPrefs.GetInt("dollar").ToString("C0");
            dollarAmount.text = deskRemainPrice.ToString("C0");

            if (deskRemainPrice == 0)
            {
                GameObject desk = Instantiate(newDesk, new Vector3(transform.position.x, 2.2f, transform.position.z)
                    , Quaternion.Euler(0f, -90f, 0f));

                desk.transform.DOScale(1.1f, 1f).SetEase(Ease.OutElastic);
                desk.transform.DOScale(1f, 1f).SetDelay(1.1f).SetEase(Ease.OutElastic);
                
                unlockProgressObj.SetActive(false);
                
                buildNavMesh.BuildNavMesh();
            }

        }
    }

    private float CalculateMoney()
    {
        return deskRemainPrice - PlayerPrefs.GetInt("dollar");
    }
}
