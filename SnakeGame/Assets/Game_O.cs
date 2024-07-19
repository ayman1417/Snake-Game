using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_O : MonoBehaviour
{
    public GameObject Snake_O;
    public GameObject Coins;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI state;
    public GameObject plane;
    int C = 5;
    int allTime = 25 * 150;
    int timeNow;
    List<Vector3> positions = new List<Vector3>();
    //List<Vector3> L_C= new List<Vector3>();
    public int numberEaten = 0;
    int numberOfCoins=3;
    bool more = true;
    bool timeTest = true;

    List<GameObject> Snake = new List<GameObject>();
    Vector3 direction = new Vector3(1, 0, 0);
    bool Loked = false;
    public bool Game_Over = false;

    // Start is called before the first frame update
    void Start()    
    {
        timeNow = allTime;
        timeText.text = $"Time: {timeNow/150}";
        for (int i = 0; i < C; i++)
        {
            positions.Add(new Vector3(i - C, 1, 0));

            GameObject New_Snake_O = Instantiate(Snake_O);
            New_Snake_O.transform.position = positions[i];

            Snake.Add(New_Snake_O);
        }
        StartCoroutine(Move_Snake());
        StartCoroutine(Create_Coins());
    }

    // Update is called once per frame
    void Update()
    {
        if (timeNow-- >=0 && timeTest)
        {
            timeText.text = $"Time: {timeNow/150}";
        }
        

        if (Loked == false)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) && direction.z == 0) { direction = new Vector3(0, 0, 1); Loked = true; }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && direction.z == 0) { direction = new Vector3(0, 0, -1); Loked = true; }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && direction.x == 0) { direction = new Vector3(1, 0, 0); Loked = true; }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction.x == 0) { direction = new Vector3(-1, 0, 0); Loked = true; }
        }

        if (Input.GetKeyDown(KeyCode.Escape)) SceneManager.LoadScene("SampleScene");
        coinsText.text = "Points : " + numberEaten.ToString();

        if (timeNow == 0 && numberEaten != 3)
        {
            state.text = "Game Over";
            state.color = Color.red;
            plane.SetActive(true);
            more = false;
        }
        else if (timeNow != 0 && numberEaten == 3)
        {
            state.text = "Success";
            state.color = Color.green;
            plane.SetActive(true);
            timeTest = false;
            more = false;
        }
    }
    IEnumerator Move_Snake()
    {
        yield return new WaitForSeconds(0.25f);
        if (Game_Over) { yield break; }
        positions.RemoveAt(0);
        positions.Add(positions[positions.Count - 1] + direction);

        for (int i = 0; i < positions.Count; i++)
        {
            Snake[i].transform.position = positions[i];
        }
       //Destroy(L_C);
        Loked = false;
        if (more)
        {
            StartCoroutine(Move_Snake());
        }
    }
    
    IEnumerator Create_Coins() 
    {
        yield return new WaitForSeconds(.01f);
        while (numberOfCoins!=0)
        {

        int x, z;
        bool Vaild_Location=true;
        
        do
        {
            x = Random.Range(-6, 6);
            z = Random.Range(-7, 7);
            Vaild_Location = true;
            for (int i = 0; i < positions.Count; i++)
            {
                if (positions[i].x == x && positions[i].z == z)
                    Vaild_Location = false;
            }


        } while (Vaild_Location==false);

         GameObject New_Coins = Instantiate(Coins);
            New_Coins.transform.position = new  Vector3(x,1,z);

            numberOfCoins--;
        }
    }
    
     
}
