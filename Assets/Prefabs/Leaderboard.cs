using UnityEngine;
using UnityEngine.UI;
using LootLocker.Requests;
using TMPro;
public class Leaderboard : MonoBehaviour
{
    public Text PlayerScore;
    public TextMeshProUGUI MemberId;
    public int ID = 18164;
    int MaxScore = 100;
    public Text[] Entries;
    public Text[] name;
    private void OnEnable()
    {

    }

    public void Start()
    {


        LootLockerSDKManager.StartGuestSession((response) =>
        {
            if (!response.success)
            {
                Debug.Log("error starting LootLocker session");

                return;
            }

            Debug.Log("successfully started LootLocker session");
        });

        /*
        LootLockerSDKManager.StartSession("player", (response) =>
         {
             if (response.success)
             {
                 Debug.LogError("Success");
             }
             else
             {
                 Debug.LogError("Failed");
             }


         });*/
    }
    private void Update()
    {
        MemberId.text = PlayerPrefs.GetString("Username");
        //    PlayerScore.text = PlayerPrefs.GetInt("kills", 0).ToString();
    }
    public void ShowScore()
    {

        string leaderboardKey = "18164";
        int count = 5;

        LootLockerSDKManager.GetScoreList(leaderboardKey, MaxScore, 0, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");

                LootLockerLeaderboardMember[] score = response.items;
                LootLockerLeaderboardMember[] Name = response.items;
                for (int i = 0; i < score.Length; i++)
                {
                    Entries[i].text = (score[i].rank + ".    " + score[i].score);
                }
                for (int i = 0; i < Name.Length; i++)
                {
                    //name[i].text = PlayerPrefs.GetString("Username").ToString();
                    name[i].text = (Name[i].player.name);
                    //  name[i].text = MemberId.text;

                }
                if (score.Length < MaxScore)
                {
                    for (int i = score.Length; i < MaxScore; i++)
                    {
                        print(MaxScore);
                        Entries[i].text = (i + 1).ToString() + ".";
                    }

                }
            }
            else
            {
                Debug.Log("failed: " + response.Error);
            }
        });





        /*

        LootLockerSDKManager.GetScoreList(ID, MaxScore, (response) =>
         {
             if (response.success)
             {
                 LootLockerLeaderboardMember[] score = response.items;
                 LootLockerLeaderboardMember[] Name = response.items;
                 for (int i = 0; i < score.Length; i++)
                 {
                     Entries[i].text = (score[i].rank + ".    " + score[i].score);
                 }
                 for (int i = 0; i < Name.Length; i++)
                 {
                      //name[i].text = PlayerPrefs.GetString("Username").ToString();
                     name[i].text = (Name[i].player.name);
                      //  name[i].text = MemberId.text;

                  }
                 if (score.Length < MaxScore)
                 {
                     for (int i = score.Length; i < MaxScore; i++)
                     {
                         Entries[i].text = (i + 1).ToString() + ".";
                     }

                 }
             }
             else
             {
                 Debug.LogError("Failed");
             }
         });


        */
    }
    public void SubmitScore(Text scoree)
    {
        string memberID = MemberId.text;
        int leaderboardID = ID;
        int score = int.Parse(scoree.text);

        LootLockerSDKManager.SubmitScore(memberID, score, leaderboardID, (response) =>
        {
            if (response.statusCode == 200)
            {
                Debug.Log("Successful");
            }
            else
            {
                Debug.Log("failed: " + response.Error);
            }
        });


        /*
                Debug.LogError(int.Parse(PlayerScore.text));
                     LootLockerSDKManager.SubmitScore(MemberId.text,0,ID,(response)=>
            //    LootLockerSDKManager.SubmitScore("Player", int.Parse(PlayerScore.text), ID, (response) =>

                {
                    if (response.success)
                       {
                           Debug.LogError("Score Submitted in Leaderboard" + PlayerScore.text);
                       }
                       else
                       {
                           Debug.LogError("Score failed to store in leaderboard");
                       }

                   });
            } */
    }
}
