using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerGetAward : MonoBehaviour
{
    public GameObject HomuraSword;
    public GameObject PraxisMegalance;

    public void getAward(AwardType type)
    {
        if(type == AwardType.PraxisMegalance)
        {
            turnToPraxisMegalance();
        }
        else
        {

        }

        void turnToPraxisMegalance()
        {
            HomuraSword.SetActive(false);
            PraxisMegalance.SetActive(true);
        }
    }
}
