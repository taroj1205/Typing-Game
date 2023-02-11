using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public Text sText;
    public Text hText;

    int correct;
    int wrong;
    int score;
    float speed;
    string _wString;

    void ShowStats()
    {
        // Read all the lines of the file into a string array
        string[] statsData = File.ReadAllLines(GlobalVariables.statsPath);

        // split the line with comma separator
        string[] parts = statsData[0].Split(',');
        // assign the first part as correct
        correct = int.Parse(parts[0]);
        // assign the second part as wrong
        wrong = int.Parse(parts[1]);
        UnityEngine.Debug.Log(correct + "," + wrong + "," + score);

        // Accuracy
        float accuracy = 0;
        if (wrong == 0)
        {
            accuracy = 100;
        }
        else if (correct == 0 && wrong > 0)
        {
            accuracy = 0;
        }
        else
        {
            accuracy = correct / (float)(correct + wrong) * 100.0f;
            accuracy = Mathf.Round(accuracy * 10f) / 10f;
        }

        // Score
        score = correct - wrong;

        // combine stats and accuracy into one line
        string stats = "Accuracy: " + accuracy + "%" + "\nScore: " + score;

        // add color based on score
        if (score > 0)
        {
            sText.text = "<color=#1fd755>" + stats + "</color>";
        }
        else if (score >= 0)
        {
            sText.text = "<color=#1fd755>" + stats + "</color>";
        }
        else if (score < 0)
        {
            sText.text = "<color=#e06c75>" + stats + "</color>";
        }
    }

    void ShowHistory()
    {
        string[] lines = File.ReadAllLines(GlobalVariables.historyPath);
        int words = lines.Length;
        if (words > 0)
        {
            // Read all the lines of the file into a string array
            string[] history = File.ReadAllLines(GlobalVariables.historyPath);
            words = history.Length;

            if (words > 0)
            {
                string[] wLines = File.ReadAllLines(GlobalVariables.historyPath);
                int word_count = wLines.Length;
                _wString = string.Concat(word_count);
            }
            else
            {
                _wString = "0";
            }

            sText.text += "Words: " + _wString + "\n";

            // Clear the hText UI element
            hText.text = "";

            // Iterate through the array and add each line to the hText UI element
            for (int i = 0; i < history.Length; i++)
            {
                // split the line with comma separator
                string[] parts = history[i].Split(',');
                // assign the first part as jstring
                string estring = parts[0];
                // assign the second part as estring
                string jstring = parts[1];
                // join estring and jstring with colon separator
                string line = estring + ": " + jstring;
                if (i == 0)
                {
                    hText.text += line + " <" + "\n";
                }
                else
                {
                    hText.text += line + "\n";
                }
            }
        }
        else if (words == 0)
        {
            hText.text = "Start Typing...";
        }
        else
        {
            hText.text = "Error...";
        }
    }

    void Awake()
    {
        if (SceneManager.GetActiveScene().name == "End")
        {
            ShowStatsAndHistory();
        }
        else
        {
            SceneManager.UnloadSceneAsync("End");
        }
    }

    void ShowStatsAndHistory()
    {
        // Read all the lines of the stats file into a string array
        string[] statsData = File.ReadAllLines(GlobalVariables.statsPath);

        // split the line with comma separator
        string[] parts = statsData[0].Split(',');
        // assign the first part as correct
        correct = int.Parse(parts[0]);
        // assign the second part as wrong
        wrong = int.Parse(parts[1]);
        speed = int.Parse(parts[2]);
        UnityEngine.Debug.Log(correct + "," + wrong + "," + score);

        // Accuracy
        if (wrong == 0)
        {
            sText.text = "Accuracy: <color=#1fd755>" + "100%" + "</color>" + "\n";
        }
        else if (correct == 0 && wrong > 0)
        {
            sText.text = "Accuracy: <color=#e06c75>" + "0%" + "</color>" + "\n";
        }
        else
        {
            float accuracy = correct / (float)(correct + wrong) * 100.0f;
            accuracy = Mathf.Round(accuracy * 10f) / 10f;
            if (accuracy > 80)
            {
                string accuracyText = accuracy.ToString();
                sText.text = "Accuracy: <color=#1fd755>" + accuracyText + "%" + "</color>" + "\n";
            }
            else if (accuracy <= 80)
            {
                string accuracyText = accuracy.ToString();
                sText.text = "Accuracy: <color=#e06c75>" + accuracyText + "%" + "</color>" + "\n";
            }
        }

        if (Mathf.Round(speed) % 100 == 0)
        {
            sText.text += "Speed: <color=#e06c75>" + speed.ToString() + "</color>" + "\n";
        }
        else
        {
            sText.text += "Speed: <color=#1fd755>" + speed.ToString() + "</color>" + "\n";
        }

        // Score
        score = correct - wrong;
        if (score > 0)
        {
            string scoreText = score.ToString();
            sText.text += "Score: <color=#1fd755>" + "+" + scoreText + "</color>" + "\n";
        }
        else if (score >= 0)
        {
            string scoreText = score.ToString();
            sText.text += "Score: <color=#1fd755>" + scoreText + "</color>" + "\n";
        }
        else if (score < 0)
        {
            string scoreText = score.ToString();
            sText.text += "Score: <color=#e06c75>" + "-" + scoreText + "</color>" + "\n";
        }

        // Show history
        ShowHistory();
    }
}