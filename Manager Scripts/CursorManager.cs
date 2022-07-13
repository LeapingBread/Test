using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : Singleton<CursorManager>

{ 
   Vector3 mouseOnWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,0));
    bool getPoint;
    [Header("Sound")]
    [SerializeField] AudioName clickPointSoundFX;
    [Header("DrawLine")]
    [SerializeField]LineRenderer linePerfeb;
    Transform lineParent;
    [SerializeField] float animationDuration;
    [HideInInspector]
    public bool isDrawing;
    [HideInInspector]
    public Point firstPoint;
    [HideInInspector]
    public Point endPoint;
    [HideInInspector]
    public Point lastPoint;
    [HideInInspector]
    public Point currentPoint;
    [HideInInspector]
    public bool levelPass;

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
    }
    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    }
    void OnAfterSceneLoadedEvent()
    {
        if(SceneManager.GetActiveScene().name != "Menu")
        lineParent = GameObject.FindWithTag("LineParent").transform;
        Init();
        
    }
    void Init()
    {
        levelPass = false;
        firstPoint = null;
        endPoint = null;
        lastPoint = null;
        currentPoint = null;
    }

    
    private void Update()
    {
        getPoint = ClickedObject();
        if(getPoint&& Input.GetMouseButtonDown(0))
        {
            
            Clicked(ClickedObject().gameObject);
            if (firstPoint != null && endPoint != null)
            {
                StartCoroutine(LevelPassRoutine());
            }
        }
    }
    IEnumerator LevelPassRoutine()
    {
        yield return new WaitUntil(() => !isDrawing);
        yield return DrawLineRoutine(endPoint, firstPoint);
        levelPass = true;
        EventHandler.CallMiniGameFinishedEvent();
    }
   public IEnumerator DrawLineRoutine(Point a, Point b)
    {
        yield return new WaitUntil(()=> !isDrawing);
        float startTime = Time.time;
        Vector3 startPos = a.transform.position;
        Vector3 endPos = b.transform.position;
        var line = Instantiate(linePerfeb, lineParent);
        line.SetPosition(0, startPos);
        line.SetPosition(1, endPos);
        Vector3 pos = startPos;
        while(pos != endPos)
        {
            isDrawing = true;
            float t = (Time.time-startTime) / animationDuration;
            pos = Vector3.Lerp(startPos, endPos, t);
            line.SetPosition(1, pos);
            yield return null;
        }
        isDrawing = false;
    }
    void Clicked(GameObject clickedObject)
    {
        switch ( clickedObject.tag)
        {
            case "Point":
               
                var point = clickedObject.GetComponent<Point>();
                    if (point != null && point.canClick)
                    {
                    if (clickPointSoundFX != AudioName.None)
                        EventHandler.CallPlaySoundEvent(clickPointSoundFX);
                    point.FadeOut();
                        currentPoint = point.GetCurrentClikedPoint();
                        if (currentPoint.previousPoint != null)
                            lastPoint = currentPoint.previousPoint;
                        point.SetPoint();
                        if (point.isFirst)
                            firstPoint = point;
                        if(point.isEnd)
                            endPoint = point;
                        if (currentPoint != null && lastPoint != null)
                          StartCoroutine(DrawLineRoutine(lastPoint,currentPoint));
                    
                    }
               
                break;
        }
    }


    public Collider2D ClickedObject()
    {
            return Physics2D.OverlapPoint(mouseOnWorldPos);
    }
}
