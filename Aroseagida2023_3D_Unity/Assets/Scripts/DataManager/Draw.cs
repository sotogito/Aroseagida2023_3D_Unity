using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#region Draw.cs 주요 기능
/*
    1. Draw(그림 데이터를 생성하는 곳) & Drawn(생성된 그림데이터를 불러오는 곳)에서 사용되는 스크립트
    2. Line Renderer를 이용해 3D 그림그리기 구현
    3. 그림그리는 손 위치를 변경
*/
#endregion

namespace Letter
{
    public class Draw : MonoBehaviour
    {
        #region 변수
        public Text draw_LetterText;
        public Text draw_LetterNum;

        //DataManager로 넘기기 전 Line Renderer Position값을 받는 임의의 list 생성
        List<DrawnInfo> tempDrawnLines = new List<DrawnInfo>(); 

            //3D 그림그리기에 사용된 변수들
        List<Vector3> linePoints;
        float timer;
        public float timerdelay;
        GameObject newLine;
        LineRenderer drawLine; //선 하나당 좌표값이 저장되는 곳
        public float lineWidth;

        //public Material lineMaterial; 
        public GameObject drawingObject; //선이 렌더링되어 나오는 곳. 펜 오브젝트
            public bool switchHand;
            public GameObject drawingObject_Left;
            public GameObject drawingObject_right;

        public FlexibleColorPicker flexibleColorPicker; //Color Picker 팔레트

        public Canvas ColorPickerUi;

            //Ui 사용자 앞에 띄우기
        public Transform head;
        public float spawnDistance = 2;

        //Load 스크린 ui
        public GameObject loaderUI;
        public Slider progressSlider;

        #endregion

        public void SwitchDrawingHand() // 그림그리는 손을 변경
        {
            if(Input.GetKeyDown(KeyCode.Y))
            {
                switchHand = !switchHand;

                if(switchHand)
                {
                    drawingObject = drawingObject_Left;
                }
                else if(!switchHand)
                {
                    drawingObject = drawingObject_right;
                }
            }
        }


        void Start()
        {   


            ColorPickerUi.gameObject.SetActive(false);

            linePoints = new List<Vector3>();
            timer = timerdelay;

            //편지 ui
            draw_LetterText.text = DataManager.instance.nowPlayer.LetterText;
            draw_LetterNum.text = DataManager.instance.nowPlayer.LetterNum+"번째 편지";
            
            // 데이터가 있으면 그림그림
            LoadLines(DataManager.instance.nowPlayer.DrawnLines.ToArray());
        }


        void Update()
        { 
            #region 팔레트 Ui 띄우기
            if(Input.GetKeyDown(KeyCode.Space))
            {   
                ColorPickerUi.gameObject.SetActive(true);
                ColorPickerUi.transform.position = head.position + new Vector3(head.forward.x, 0,head.forward.z).normalized * spawnDistance;
            }
            ColorPickerUi.transform.LookAt(new Vector3(head.position.x, ColorPickerUi.transform.position.y, head.position.z));

            if(Input.GetKeyUp(KeyCode.Space))
            {
                ColorPickerUi.gameObject.SetActive(false);
            }
            #endregion


            #region 그림그리기 코드
            if(Input.GetKeyDown(KeyCode.E))             //마우스를 누를 때 -> 선 하나의 Position & Color
            {
                newLine = new GameObject();
                drawLine = newLine.AddComponent<LineRenderer>();
                drawLine.material = new Material(Shader.Find("Sprites/Default")); 
                //drawLine.material = lineMaterial;
                drawLine.startColor = flexibleColorPicker.color;
                drawLine.endColor = flexibleColorPicker.color;
                drawLine.startWidth = lineWidth;
                drawLine.endWidth = lineWidth;

            }
            
            if(Input.GetKey(KeyCode.E))               //마우스를 누르는 동안   -> 변하는 데이터를 받음, Position 값 Update됨
            {
                //마우스의 위치에 따라서 선이 생겨 따라옴
                Debug.DrawRay(Camera.main.ScreenToWorldPoint(Input.mousePosition), GetMousePosition(),Color.red);
                timer -= Time.deltaTime;
                if(timer<=0)
                {
                    drawLine.positionCount = linePoints.Count;
                    drawLine.SetPositions(linePoints.ToArray());
                    linePoints.Add(GetMousePosition());

                    timer = timerdelay;
                }
            }

            if(Input.GetKeyUp(KeyCode.E))           //마우스를 떌 떄  ->  생성된 선 하나(New Game Object)의 데이터 임시저장
            {
                /*
                foreach (Vector3 point in linePoints)
                {
                    Debug.Log("X: " + point.x + ", Y: " + point.y + ", Z: " + point.z);
                }
                Debug.Log("flexibleColorPicker Color"+flexibleColorPicker.color);
                #endregion
                */

                //데이터 받기
                DrawnInfo drawninfo = new DrawnInfo
                {
                    Points = linePoints.ToArray(),
                    Color = flexibleColorPicker.color
                };
                tempDrawnLines.Add(drawninfo);

                linePoints.Clear();
            }
            #endregion

            SwitchDrawingHand();
        
        }

        Vector3 GetMousePosition() //그림이 나오는 곳
        {
            /*
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return ray.origin + ray.direction * 10;
            */
            return drawingObject.transform.position;
        }

     
        public void Save() //[저장하고 나가기] Draw 씬에서만 활용
        {
                //임시로 저장했던 Line Renderer 데이터를 DataManager로 넘김
            DataManager.instance.nowPlayer.DrawnLines = tempDrawnLines;
            DataManager.instance.SaveData();
            StartCoroutine(LoadScene_Coroutine(0));
            //SceneManager.LoadScene(0);
        }


        #region Drawn 씬

        public void Out() //[나가기] Drawn 씬에서만 활용
        {
            StartCoroutine(LoadScene_Coroutine(0));
            //SceneManager.LoadScene(0);
        }

        public void LoadLines(DrawnInfo[] drawnInfos) //그림 불러오기
        {
            // 저장된 그림 정보를 이용하여 화면에 불러오는 작업 수행
            foreach (DrawnInfo drawnInfo in drawnInfos)
            {
                GameObject newLine = new GameObject();
                LineRenderer drawLine = newLine.AddComponent<LineRenderer>();
                drawLine.material = new Material(Shader.Find("Sprites/Default"));
                //drawLine.material = lineMaterial;
                drawLine.startColor = drawnInfo.Color;
                drawLine.endColor = drawnInfo.Color;
                drawLine.startWidth = lineWidth;
                drawLine.endWidth = lineWidth;
                drawLine.positionCount = drawnInfo.Points.Length;
                drawLine.SetPositions(drawnInfo.Points);
            }
        }
        #endregion

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
    }

    [System.Serializable]
    public class DrawnInfo
    {
        public Vector3[] Points;
        public Color Color;
    }


}