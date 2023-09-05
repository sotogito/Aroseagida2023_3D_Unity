using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Networking;

#region PrevLetterList_Select.cs 주요 기능
/*
    2. [편지보기] -> [그림그리기] & [넘어가기] 버튼
    3. [그림그리기] -> 서버에서 최근 편지 10개 받아오기
    4. [넘어가기] -> 서버로 is_active 필드값 변경 요청(true기본값 -> fasle)
*/
#endregion

namespace Letter
{
    public class PrevLetterList_Select : MonoBehaviour
    {   
        private const string DjangoApiUrl = "http://127.0.0.1:8000/api/get_prevletter_list/";


        #region 변수 & Ui

        //클릭된 버튼의 값 = index값
        public int BtnNum;

        public Text[] BtnText;
    
        //서버에서 받아온 데이터를 임시 저장하는 곳
        public string[] PrevLetter_text;
        public int[] PrevLetter_id;
  
        //[그림그리기]&[넘어가기] 버튼
        public Button drawButton;
        public Button skipButton;

        //Load 스크린 ui
        public GameObject loaderUI;
        public Slider progressSlider;

        #endregion

     
        

        void Start()
        {
            drawButton.gameObject.SetActive(false);
            skipButton.gameObject.SetActive(false);
        }


        public void nowClick(int btnnum) //클릭된 LetterList 버튼의 값을 index로 받음
        {
            BtnNum = btnnum;

            print("버튼의 index값 : " + BtnNum);
            print("편지 text : " + PrevLetter_text[BtnNum]);
            print("편지 id : " + PrevLetter_id[BtnNum]);

            drawButton.gameObject.SetActive(true);
            skipButton.gameObject.SetActive(true);
        }


        #region 버튼ui에 지정할 Function

        public void nowDraw() //[그림그리기]
        {
            //DataManager로 선택된 편지 데이터 옮기기
            DataManager.instance.nowPlayer.LetterNum = PrevLetter_id[BtnNum];
            DataManager.instance.nowPlayer.LetterText = PrevLetter_text[BtnNum];
            DataManager.instance.nowPlayer.IsDrawn = true;
            
            DataManager.instance.test();
            
            DataManager.instance.SaveData();
            GoDraw();
        }

        public void nowSkip() //[넘어가기]
        {
            //is_active 필드 값 변경 true -> false
            StartCoroutine(UpdateIsActve(PrevLetter_id[BtnNum]));
            BtnText[BtnNum].text = "삭제 되었습니다";
        }
        
        #endregion


        public void GoDraw() //draw 씬으로 이동
        {
            
            StartCoroutine(LoadScene_Coroutine(1));
            //SceneManager.LoadScene(3);
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


        public void MakeLetterList() //서버에서 받은 10개의 데이터 편지 List로 띄우기
        {
            //BtnNum(버튼의Num) = 배열의 index
            for(int i = PrevLetter_id.Length-1; i>=0; i--)
            {
                BtnText[i].text = PrevLetter_text[i];
            }
        }


        public void ReadingLetters() //[편지보기] 버튼 클릭 시 (MakeLetter() -> GetPrevLetterList())
        {
            //서버에서 PrevLetter 최근 데이터 10개 받아오기
            StartCoroutine(GetPrevLetterList());
        }


        #region GetPrevLetterList()+[System.Serializable]
        private IEnumerator GetPrevLetterList()
        {
            using (UnityWebRequest request = UnityWebRequest.Get(DjangoApiUrl))
            {
                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Error: " + request.error);
                }
                else
                {
                    string jsonResponse = request.downloadHandler.text;
                    print(request.downloadHandler.text);
                    PrevLetterListResponse response = JsonUtility.FromJson<PrevLetterListResponse>(jsonResponse);
                    PrevLetter_text = response.prevletters;
                    PrevLetter_id = response.prevletters_id;
                    MakeLetterList();
                }
            }
        }
        [System.Serializable]
        public class PrevLetterListResponse
        {  
            public string[] prevletters;
            public int[] prevletters_id;
        }
        #endregion
    

        #region UpdateIsActve() [넘어가기]->is_active:false
        private IEnumerator UpdateIsActve(int dataId)
        {
            string apiUrl = "http://127.0.0.1:8000/api/update_is_active/";
            WWWForm form = new WWWForm();
            form.AddField("data_id", dataId);

            using (UnityWebRequest request = UnityWebRequest.Post(apiUrl, form))
            {

                yield return request.SendWebRequest();

                if (request.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Error: " + request.error);
                }
                else
                {
                    Debug.Log("is_active field updated successfully.");
                }
            }
        }
        #endregion

    }
}



