using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

[System.Serializable]
public struct image
{
    public Image image1;
    public Image image2;
}

public class RandomCharacterAnswer : MonoBehaviour
{
    int c, k;
    int n = 0, sp = 0;
    string stringTemp = "";
    public static int currentMoney = 0;
    public static int indexMap;
    private string temp;
    bool check1 = true;
    bool check2 = true;


    public Image tryAgain;
    public Image HelpScreen;
    public Image EXACTLY;
    public Image WIN;
    public Image nextLV;
    public Image img1;
    public Image img2;
    public Image idiom;
    public Text ansNextLV;
    public Text lvText, T1, T2;
    public Text money;
    public Text Note;
    public Text txt;

    private List<int> tempIndex = new List<int>();
    private List<int> tempIndex1 = new List<int>();
    private List<int> temp1 = new List<int>();
    private string[] tp;
    private string[] temp2;

    public string[] str;
    public string[] answer;
    public Button[] chr;
    public Button[] ch;
    public Text[] chrAnswer;
    public Text[] textCharacter;
    public image[] lv;
    public bool[] checkActive = new bool[13];
    public bool[] Space = new bool[13];


    Animator anim;
    AudioSource audio;
    public AudioClip earMoney;
    public AudioClip click;
    public AudioClip falseAns;
    public AudioClip childYeeee;

    //version 2 keyboard
    [Header("Ver keyboard")]
    [SerializeField]
    InputField[] inputAnrwer;


    //infor
    // c: so ky tu cau tra loi bao gom ca dau space
    //ch = chAnswer 
    void Start()
    {
        audio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();

        //Lưu thông tin người chơi
        indexMap = PlayerPrefs.GetInt("Level");
        currentMoney = PlayerPrefs.GetInt("Coin");

        UIManager();
        StringProcessing();

        //
        lvText.text = "LV " + (indexMap + 1);
        money.text = currentMoney.ToString();
    }

    void Update()
    {
        lvText.text = "LV " + (indexMap + 1);
        money.text = currentMoney.ToString();
    }

    public void UIManager()
    {
        money.text = currentMoney.ToString();
        img1.sprite = lv[indexMap].image1.GetComponent<Image>().sprite;
        img2.sprite = lv[indexMap].image2.GetComponent<Image>().sprite;


        if (indexMap % 3 == 0)// ẩn những ô thừa
        {
            idiom.gameObject.SetActive(true);
        }


        answer[indexMap] = answer[indexMap].ToUpper();//chuyển thành chữ hoa
        c = answer[indexMap].Length;
        tp = answer[indexMap].Split(' ');
        sp = tp.Length - 1;
        for (int i = c; i < 26; i++)
        {
            ch[i].gameObject.SetActive(false);

            inputAnrwer[i].gameObject.SetActive(false);
        }
    }

    public void StringProcessing()//xử lý chuỗi
    {
        int m = 60;
        if (answer[indexMap].Length <= 13)
        {
            if (c % 2 == 1)
            {
                k = c / 2;
                ch[k].transform.localPosition = new Vector3(0, ch[k].transform.localPosition.y, ch[k].transform.localPosition.z);

                //ver 2
                //inputAnrwer[k].transform.localPosition = new Vector3(0, inputAnrwer[k].transform.localPosition.y, inputAnrwer[k].transform.localPosition.z);

                for (int i = 0; i < k; i++)
                {

                    ch[i].transform.localPosition = new Vector3(-m * (k - i), ch[i].transform.localPosition.y, ch[i].transform.localPosition.z);
                    ch[i + k + 1].transform.localPosition = new Vector3(m * (i + 1), ch[i + k + 1].transform.localPosition.y, ch[i + k + 1].transform.localPosition.z);

                    //ver 2
                    //inputAnrwer[i].transform.localPosition = new Vector3(-m * (k - i), inputAnrwer[i].transform.localPosition.y, inputAnrwer[i].transform.localPosition.z);
                    //inputAnrwer[i + k + 1].transform.localPosition = new Vector3(m * (i + 1), inputAnrwer[i + k + 1].transform.localPosition.y, inputAnrwer[i + k + 1].transform.localPosition.z);

                }

            }
            else
            {
                k = c / 2;
                ch[k].transform.localPosition = new Vector3(m / 2, ch[k].transform.localPosition.y, ch[k].transform.localPosition.z);
                ch[k - 1].transform.localPosition = new Vector3(-m / 2, ch[k - 1].transform.localPosition.y, ch[k - 1].transform.localPosition.z);

                //ver 2
                //inputAnrwer[k].transform.localPosition = new Vector3(m / 2, inputAnrwer[k].transform.localPosition.y, inputAnrwer[k].transform.localPosition.z);
                //inputAnrwer[k - 1].transform.localPosition = new Vector3(-m / 2, inputAnrwer[k - 1].transform.localPosition.y, inputAnrwer[k - 1].transform.localPosition.z);
                for (int i = 0; i < k - 1; i++)
                {
                    ch[i].transform.localPosition = new Vector3(-m * (k - i) + m / 2.0f, ch[i].transform.localPosition.y, ch[i].transform.localPosition.z);
                    ch[i + k + 1].transform.localPosition = new Vector3(m * (i + 1) + m / 2f, ch[i + k + 1].transform.localPosition.y, ch[i + k + 1].transform.localPosition.z);

                    //ver 2
                    //inputAnrwer[i].transform.localPosition = new Vector3(-m * (k - i) + m / 2.0f, inputAnrwer[i].transform.localPosition.y, inputAnrwer[i].transform.localPosition.z);
                    //inputAnrwer[i + k + 1].transform.localPosition = new Vector3(m * (i + 1) + m / 2f, inputAnrwer[i + k + 1].transform.localPosition.y, inputAnrwer[i + k + 1].transform.localPosition.z);

                }
            }
            temp = ArrangementText(PlusQuestion());
            for (int i = 0; i < temp.Length; i++)
            {
                //textCharacter[i].text = temp[i].ToString();
            }
            string[] arrList = answer[indexMap].Split(' ');
            int space = 0;
            for (int i = 0; i < arrList.Length - 1; i++)
            {
                space += i == 0 ? arrList[i].Length : arrList[i].Length + 1;
                chrAnswer[space].text = " ";
                chrAnswer[space].transform.parent.gameObject.SetActive(false);


            }

            //ver 2
            //for (int i = 0; i < answer[indexMap].Length; i++)
            //{
            //    print("cau tra loi 2 " + answer[indexMap]);
            //    if (answer[indexMap][i].ToString() == " ")
            //    {
            //        print(i);
            //        //ver 2
            //        inputAnrwer[i].text = " ";
            //        inputAnrwer[i].gameObject.SetActive(false);
            //    }
            //}
        }

        else
        {


            int t = 0, tem = 0;
            string newStr1, newStr2;
            for (int i = 0; i < tp.Length; i++)
            {
                t += tp[i].Length + 1;
                if (t > 13)
                {
                    int j;
                    for (j = 0; j < i; j++)
                    {
                        tem += tp[j].Length;
                    }
                    tem += (j - 1);

                    for (int k = tem; k < 13; k++)
                    {
                        ch[k].gameObject.SetActive(false);
                    }
                    break;
                }
            }
            newStr1 = answer[indexMap].Substring(0, tem);
            newStr2 = answer[indexMap].Substring(tem + 1);
            if (newStr1.Length % 2 == 1)
            {
                k = newStr1.Length / 2;
                ch[k].transform.localPosition = new Vector3(0, ch[k].transform.localPosition.y, ch[k].transform.localPosition.z);

                for (int i = 0; i < k; i++)
                {

                    ch[i].transform.localPosition = new Vector3(-m * (k - i), ch[i].transform.localPosition.y, ch[i].transform.localPosition.z);
                    ch[i + k + 1].transform.localPosition = new Vector3(m * (i + 1), ch[i + k + 1].transform.localPosition.y, ch[i + k + 1].transform.localPosition.z);
                }

            }
            else
            {
                k = newStr1.Length / 2;
                ch[k].transform.localPosition = new Vector3(m / 2, ch[k].transform.localPosition.y, ch[k].transform.localPosition.z);
                ch[k - 1].transform.localPosition = new Vector3(-m / 2, ch[k - 1].transform.localPosition.y, ch[k - 1].transform.localPosition.z);
                for (int i = 0; i < k - 1; i++)
                {
                    ch[i].transform.localPosition = new Vector3(-m * (k - i) + m / 2.0f, ch[i].transform.localPosition.y, ch[i].transform.localPosition.z);
                    ch[i + k + 1].transform.localPosition = new Vector3(m * (i + 1) + m / 2f, ch[i + k + 1].transform.localPosition.y, ch[i + k + 1].transform.localPosition.z);
                }
            }
            temp = PlusQuestion();
            temp = ArrangementText(temp);
            for (int i = 0; i < temp.Length; i++)
            {
                //textCharacter[i].text = temp[i].ToString();
            }
            string[] arrList = answer[indexMap].Split(' ');
            int space = 0;
            for (int i = 0; i < arrList.Length - 1; i++)
            {
                space += i == 0 ? arrList[i].Length : arrList[i].Length + 1;
                chrAnswer[space].text = " ";
                chrAnswer[space].transform.parent.gameObject.SetActive(false);
            }
            for (int k = 13; k < 26; k++)
            {
                checkActive[k - 13] = ch[k].gameObject.activeInHierarchy;
                Space[k - 13] = chrAnswer[k].text == " " ? true : false;
            }
            tem += 1;
            for (int k = 13 + (13 - tem); k < 26; k++)
            {
                ch[k].gameObject.SetActive(checkActive[k - 13 - (13 - tem)]);
                chrAnswer[k].text = Space[k - 13 - (13 - tem)] ? " " : "";
            }
            for (int k = 13; k < 13 + (13 - tem); k++)
            {
                ch[k].gameObject.SetActive(true);
                chrAnswer[k].text = "";
            }
            if (newStr2.Length % 2 == 1)
            {
                k = newStr2.Length / 2;
                ch[13 + k].transform.localPosition = new Vector3(0, ch[13 + k].transform.localPosition.y, ch[13 + k].transform.localPosition.z);

                for (int i = 0; i < k; i++)
                {

                    ch[i + 13].transform.localPosition = new Vector3(-m * (k - i), ch[i + 13].transform.localPosition.y, ch[i + 13].transform.localPosition.z);
                    ch[i + k + 14].transform.localPosition = new Vector3(m * (i + 1), ch[i + k + 14].transform.localPosition.y, ch[i + k + 14].transform.localPosition.z);
                }

            }
            else
            {
                k = newStr2.Length / 2;
                ch[13 + k].transform.localPosition = new Vector3(m / 2, ch[13 + k].transform.localPosition.y, ch[13 + k].transform.localPosition.z);
                ch[k + 12].transform.localPosition = new Vector3(-m / 2, ch[k + 12].transform.localPosition.y, ch[k + 12].transform.localPosition.z);
                for (int i = 0; i < k - 1; i++)
                {
                    ch[i + 13].transform.localPosition = new Vector3(-m * (k - i) + m / 2.0f, ch[i + 13].transform.localPosition.y, ch[i + 13].transform.localPosition.z);
                    ch[i + k + 14].transform.localPosition = new Vector3(m * (i + 1) + m / 2f, ch[i + k + 14].transform.localPosition.y, ch[i + k + 14].transform.localPosition.z);
                }
            }
        }
    }

    public string ArrangementText(string textIn)
    {

        textIn = textIn.ToUpper();
        for (int i = 0; i < textIn.Length; i++)
        {
            temp1.Add((int)(textIn[i]) == 32 ? Random.Range(65, 90) : (int)(textIn[i]));
        }
        temp1.Sort();
        string result = "";
        for (int i = 0; i < temp1.Count; i++)
        {
            result += ((char)temp1[i]).ToString();
        }
        return result;
    }

    public void Continue()
    {
        if (indexMap < 99)
        {
            audio.clip = click;
            audio.Play();
            indexMap++;
            img1.sprite = lv[indexMap].image1.GetComponent<Image>().sprite;
            img2.sprite = lv[indexMap].image2.GetComponent<Image>().sprite;
            PlayerPrefs.SetInt("Level", indexMap);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            ShareRateAds.shareRateAds.ShowIn();
        }
        else
        {
            audio.clip = click;
            audio.Play();
            WIN.gameObject.SetActive(true);

        }
    }


    public char randomCharacter()//trả về 1 chữ trong bảng chữ cái
    {
        int x = (Random.Range(65, 90));
        char c = (char)x;
        return c;
    }

    public string PlusQuestion(string question = "")//nối chuỗi đáp án với 1 chữ cái bất kì
    {
        question = answer[indexMap];

        while (question.Length < 27)
        {
            question = question + randomCharacter();
        }
        return question;

    }

    public void clickButton(Text index)
    {
        if ((chrAnswer[0].text + chrAnswer[1].text + chrAnswer[2].text + chrAnswer[3].text + chrAnswer[4].text +
        chrAnswer[5].text + chrAnswer[6].text + chrAnswer[7].text + chrAnswer[8].text + chrAnswer[9].text +
        chrAnswer[10].text + chrAnswer[11].text + chrAnswer[12].text + chrAnswer[13].text + chrAnswer[14].text +
        chrAnswer[15].text + chrAnswer[16].text + chrAnswer[17].text + chrAnswer[18].text + chrAnswer[19].text +
        chrAnswer[20].text + chrAnswer[21].text + chrAnswer[22].text + chrAnswer[23].text + chrAnswer[24].text +
        chrAnswer[25].text).Length < c)
        {
            audio.clip = click;
            audio.Play();
            int min = 0;
            //chr[int.Parse(index.name)].gameObject.SetActive(false);

            for (int i = 0; i < 26; i++)
            {
                if (chrAnswer[i].text == "" && ch[i].gameObject.activeInHierarchy)
                {
                    min = i;
                    break;
                }
            }

            ch[min].gameObject.GetComponentInChildren<Text>().text = index.text;
            ch[min].gameObject.name = index.name;
            n++;
            if (chrAnswer[0].text + chrAnswer[1].text + chrAnswer[2].text + chrAnswer[3].text + chrAnswer[4].text +
                chrAnswer[5].text + chrAnswer[6].text + chrAnswer[7].text + chrAnswer[8].text + chrAnswer[9].text +
                chrAnswer[10].text + chrAnswer[11].text + chrAnswer[12].text + chrAnswer[13].text + chrAnswer[14].text +
                chrAnswer[15].text + chrAnswer[16].text + chrAnswer[17].text + chrAnswer[18].text + chrAnswer[19].text +
                chrAnswer[20].text + chrAnswer[21].text + chrAnswer[22].text + chrAnswer[23].text + chrAnswer[24].text +
                chrAnswer[25].text == answer[indexMap])
            {
                StartCoroutine(ShowNextLV());
                audio.clip = earMoney;
                audio.Play();

            }
            if (n == c - sp && chrAnswer[0].text + chrAnswer[1].text + chrAnswer[2].text + chrAnswer[3].text + chrAnswer[4].text +
                chrAnswer[5].text + chrAnswer[6].text + chrAnswer[7].text + chrAnswer[8].text + chrAnswer[9].text + chrAnswer[10].text +
                chrAnswer[11].text + chrAnswer[12].text + chrAnswer[13].text + chrAnswer[14].text + chrAnswer[15].text + chrAnswer[16].text +
                chrAnswer[17].text + chrAnswer[18].text + chrAnswer[19].text + chrAnswer[20].text + chrAnswer[21].text + chrAnswer[22].text +
                chrAnswer[23].text + chrAnswer[24].text + chrAnswer[25].text != answer[indexMap])
            {
                n = (chrAnswer[0].text + chrAnswer[1].text + chrAnswer[2].text + chrAnswer[3].text + chrAnswer[4].text +
                    chrAnswer[5].text + chrAnswer[6].text + chrAnswer[7].text + chrAnswer[8].text + chrAnswer[9].text +
                    chrAnswer[10].text + chrAnswer[11].text + chrAnswer[12].text + chrAnswer[13].text + chrAnswer[14].text +
                    chrAnswer[15].text + chrAnswer[16].text + chrAnswer[17].text + chrAnswer[18].text + chrAnswer[19].text +
                    chrAnswer[20].text + chrAnswer[21].text + chrAnswer[22].text + chrAnswer[23].text + chrAnswer[24].text +
                    chrAnswer[25].text).Length;
                audio.clip = falseAns;
                audio.Play();
                StartCoroutine(ShowTryAgain());
            }

        }



    }

    //ver 2

    public void onChangeValueInputFeild()
    {
        audio.clip = click;
        audio.Play();
    }

    public void onEndIntputFeild()
    {
        //ver 2
        if ((sumKeyAnswer()).Length < c)
        {
            //audio.clip = click;
            //audio.Play();
            n++;
            //ch[min].gameObject.GetComponentInChildren<Text>().text = index.text;
            checkInputFieldFail();
        }
        if (sumKeyAnswer() == answer[indexMap])
        {
            StartCoroutine(ShowNextLV());
            audio.clip = earMoney;
            audio.Play();

        }
        if (n == c - sp && sumKeyAnswer() != answer[indexMap])
        {
            n = (sumKeyAnswer()).Length;
            audio.clip = falseAns;
            audio.Play();
            StartCoroutine(ShowTryAgain());
        }
    }

    //ver 2
    private string sumKeyAnswer()
    {
        return (inputAnrwer[0].text + inputAnrwer[1].text + inputAnrwer[2].text + inputAnrwer[3].text + inputAnrwer[4].text +
                inputAnrwer[5].text + inputAnrwer[6].text + inputAnrwer[7].text + inputAnrwer[8].text + inputAnrwer[9].text +
                inputAnrwer[10].text + inputAnrwer[11].text + inputAnrwer[12].text + inputAnrwer[13].text + inputAnrwer[14].text +
                inputAnrwer[15].text + inputAnrwer[16].text + inputAnrwer[17].text + inputAnrwer[18].text + inputAnrwer[19].text +
                inputAnrwer[20].text + inputAnrwer[21].text + inputAnrwer[22].text + inputAnrwer[23].text + inputAnrwer[24].text +
                inputAnrwer[25].text);
    }
    //ver 2
    public void checkInputFieldFail()
    {
        for (int i = 0; i < c; i++)
        {
            if(inputAnrwer[i].text.Length > 1)
            {
                inputAnrwer[i].text = "";

            }
        }
    }


    public void clickButtonChar(Text index)
    {
        if (index.text != "")
        {
            audio.clip = click;
            audio.Play();
            index.text = "";
            //chr[int.Parse(index.transform.parent.name)].gameObject.SetActive(true);
            n--;
        }
    }

    IEnumerator ShowNextLV()
    {
        EXACTLY.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        audio.clip = childYeeee;
        audio.Play();
        currentMoney += 5;
        PlayerPrefs.SetInt("Coin", currentMoney);
        EXACTLY.gameObject.SetActive(false);
        ansNextLV.text = answer[indexMap];
        txt.text = str[Random.Range(0, str.Length)];
        anim.SetTrigger("NEXTLV");
    }

    IEnumerator ShowTryAgain()
    {
        tryAgain.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        tryAgain.gameObject.SetActive(false);
        n = n - sp;
    }


    //các hàm bên dưới chạy vào khi click vào các button 
    public void ClearAnswer()
    {

        audio.clip = click;
        audio.Play();
        n = 0;
        Note.gameObject.SetActive(true);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        for (int i = 0; i < chr.Length; i++)
        {
            chr[i].gameObject.SetActive(true);
        }
        for (int i = 0; i < chrAnswer.Length; i++)
        {
            chrAnswer[i].text = chrAnswer[i].text == " " ? " " : "";
        }
    }

    public void ActiveMenuHelp()
    {
        audio.clip = click;
        audio.Play();
        HelpScreen.gameObject.SetActive(true);
    }

    public void BackMenuHelp()
    {
        audio.clip = click;
        audio.Play();
        HelpScreen.gameObject.SetActive(false);
    }

    public void SkipPuzzle()
    {

        if (currentMoney >= 50)
        {
            audio.clip = childYeeee;
            audio.Play();
            //audio.clip = click;
            HelpScreen.gameObject.SetActive(false);
            currentMoney -= 45;
            PlayerPrefs.SetInt("Coin", currentMoney);
            ansNextLV.text = answer[indexMap];
            txt.text = str[Random.Range(0, str.Length)];
            anim.SetTrigger("NEXTLV");
        }

    }

    public void ShowPhoto1()
    {
        if (check1 == true && currentMoney >= 15)
        {
            audio.clip = earMoney;
            audio.Play();
            check1 = false;
            currentMoney -= 15;
            PlayerPrefs.SetInt("Coin", currentMoney);
            T1.text = lv[indexMap].image1.name;
            HelpScreen.gameObject.SetActive(false);
        }

    }

    public void ShowPhoto2()
    {
        if (check2 == true && currentMoney >= 15)
        {
            audio.clip = earMoney;
            audio.Play();
            check2 = false;
            currentMoney -= 15;
            PlayerPrefs.SetInt("Coin", currentMoney);
            T2.text = lv[indexMap].image2.name;
            HelpScreen.gameObject.SetActive(false);
        }

    }

    public void WatchVideo()
    {
        audio.clip = click;
        audio.Play();
        ShareRateAds.shareRateAds.ShowBasedVideo();

    }

    public GameObject AreYouSure;
    public void ResetLevel()
    {
        AreYouSure.SetActive(true);
       
    }
    public void Sure()
    {
        AreYouSure.SetActive(false);
        indexMap = 0;
        currentMoney = 0;
        PlayerPrefs.SetInt("Level", indexMap);
        PlayerPrefs.SetInt("Coin", currentMoney);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void NotSure()
    {
        AreYouSure.SetActive(false);
    }
}
