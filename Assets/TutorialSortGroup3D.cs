using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialSortGroup3D : SortGroup3D {

    public override void CheckSortStateAnswer()
    {
        int previousRank = currentSortRanking[0];
        bool sorted = true;

        for (int i = 1; i < currentSortRanking.Length; i++)
        {
            if (previousRank < currentSortRanking[i])
            {
                previousRank = currentSortRanking[i];
                continue;
            }
            else
            {
                sorted = false;
                break;
            }
        }

        if (sorted)
        {
            ColorAnswersGreen();
        }
        else
        {
            ColorAnswersRed();
        }
    }
}
