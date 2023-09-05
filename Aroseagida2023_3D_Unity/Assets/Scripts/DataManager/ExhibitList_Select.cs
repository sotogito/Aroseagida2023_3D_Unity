using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

#region ExhibitList_Select.cs 주요 기능
/*
    1. 전시List 띄우기
    2. nowSlot 랜덤생성 후 할당
    3. ScreenShot.cs에서 캡쳐한 사진을 업로드함
*/
#endregion

namespace Letter
{
    public class ExhibitList_Select : MonoBehaviour
    {
        
        #region 변수

        //전시에 띄울 편지 데이터
        public Text[] SlotText_LetterText;
        public Text[] SlotText_LetterNum;
        
        //저장된 데이터가 있는지 확인
        public bool[] savefile = new bool[5];
        //비어있는 데이터의 nowSlot값을 저장
        List<int> falseIndexes = new List<int>(); 

        //캡쳐이미지 업로드 panel
        public Image[] ScreenShotImg;

        //Load 스크린 ui
        public GameObject loaderUI;
        public Slider progressSlider;
        #endregion
    


        void Start()
        {
            falseIndexes.Add(-2); //데이터 임시저장
            
            #region 데이터 유무 학인
            for(int i = 0; i<5; i++)
            {
                if(File.Exists(DataManager.instance.path + $"{i}"))
                {
                    savefile[i] = true;
                    DataManager.instance.nowSlot = i;
                    DataManager.instance.LoadData();
                    SlotText_LetterText[i].text = DataManager.instance.nowPlayer.LetterText;
                    SlotText_LetterNum[i].text = DataManager.instance.nowPlayer.LetterNum + "번째 편지";
                    //print(SlotText_LetterText[i].text);
                    //print(SlotText_LetterNum[i].text);
                    UploadScreenShotImg(i);
                }
                else
                {
                    SlotText_LetterText[i].text = "없음!";
                    falseIndexes.Add(i);
                }  
            }
            DataManager.instance.DataClear(); 
            #endregion
            MakenowSlot();

        }

        public void UploadScreenShotImg(int NowSlot)
        {
            string fileName ="_Screenshot_" + NowSlot + ".png";
            string screenshotPath = Application.persistentDataPath +  "/AROSEAGIDA SAVE" + fileName;

            if(System.IO.File.Exists(screenshotPath))
            {
                byte[] imageDate = System.IO.File.ReadAllBytes(screenshotPath);
                Texture2D texture = new Texture2D(2,2);
                texture.LoadImage(imageDate);

                Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));

                ScreenShotImg[NowSlot].sprite = sprite;
            }
        }


        public void Slot(int slotnum) //전시장List 데이터 로드
        {
            DataManager.instance.nowSlot = slotnum;

            print(slotnum);

            if(savefile[slotnum]) //데이터가 있는 파일이라면 로드하기
            {
                DataManager.instance.LoadData();
            }
            GoDrew();
        }


        public void GoDrew() //drawn 씬으로 이동
        {
            StartCoroutine(LoadScene_Coroutine(2));
            //SceneManager.LoadScene(2);
        }
        public IEnumerator LoadScene_Coroutine(int num)
        {
            progressSlider.value = 0;
            loaderUI.SetActive(true);
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(num);
            asyncOperation.allowSceneActivation = false; // 오타 수정: "activition" → "activation"
            float progress = 0;
            while (!asyncOperation.isDone)
            {
                progress = Mathf.MoveTowards(progress, asyncOperation.progress, Time.deltaTime);
                progressSlider.value = progress; // 슬라이더 값 업데이트
                if (progress >= 0.9f)
                {
                    progressSlider.value = 1;
                    asyncOperation.allowSceneActivation = true;
                }
                yield return null;
            }
        }

        
        public void MakenowSlot() //DataManager로 넘길 랜덤 nowSlot 할당
        {
            //전시List에 데이터가 다 차있지 않을 때
            if(falseIndexes.Count > 1) 
            {
                int randomIndex = Random.Range(1, falseIndexes.Count); 
                DataManager.instance.nowSlot = falseIndexes[randomIndex]; //randomIndex = index값
            }

            //전시List에 데이터가 다 찼을 때
            else if(falseIndexes.Count <= 1) 
            {
                int randomIndex = Random.Range(0, 5);
                DataManager.instance.DataClear(); 
                DataManager.instance.nowSlot = randomIndex; //randomIndex = nowSlot값
            }

        }
        
    }

}
