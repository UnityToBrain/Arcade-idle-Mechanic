using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class Printer : MonoBehaviour
{
   [SerializeField] private Transform[] PapersPlace = new Transform[10];
   [SerializeField] private GameObject paper;
   public float PaperDeliveryTime,YAxis;
   public int CountPapers;
    void Start()
    {
        for (int i = 0; i < PapersPlace.Length; i++)
        {
            PapersPlace[i] = transform.GetChild(0).GetChild(i);
        }

        StartCoroutine(PrintPaper(PaperDeliveryTime));
    }

    public IEnumerator PrintPaper(float Time)
    {
        var PP_index = 0;
        
        while (CountPapers < 100)
        {
            GameObject NewPaper = Instantiate(paper, new Vector3(transform.position.x, -3f, transform.position.z),
                quaternion.identity, transform.GetChild(1));

            NewPaper.transform.DOJump(new Vector3(PapersPlace[PP_index].position.x, PapersPlace[PP_index].position.y + YAxis,
                PapersPlace[PP_index].position.z), 2f, 1, 0.5f).SetEase(Ease.OutQuad);

            if (PP_index < 9)
                PP_index++;
            else
            {
                PP_index = 0;
                YAxis += 0.17f;
            }
            
            yield return new WaitForSecondsRealtime(Time);

        }
    }
}
