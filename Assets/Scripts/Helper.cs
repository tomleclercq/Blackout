using UnityEngine;
using System.Collections;

public enum KeysHelp
{    
    F,
    T,
    N,
    C,
    H,
    Delete,
    Escape,
    kp4,
    kp6,
    MAX
};

public class Helper : MonoBehaviour {

	static public KeyCode[] staticInputs = new KeyCode[]
	{
        KeyCode.F,
		KeyCode.T,
		KeyCode.N,
		KeyCode.C,
		KeyCode.H,
		KeyCode.Delete,
		KeyCode.Escape,
        KeyCode.Keypad4,
        KeyCode.Keypad6,
	};

	static public string[] staticControls = new string[]
	{
		"F. . . . . . .Toggle FPS",
		"C. . . . . . .Toggle cursor",
		"H. . . . . . .Toggle help",
        "N. . . . . . .Next scene",        
        "Delete . . . .Erase data",       
		"ESC/LMB RMB. .Quit"
	};
    
    public const int I_MAX_LOG = 550;
    public const float F_TIME_BEFORE_QUIT = 0.75f;
    public static bool bShownFPS = false;
    public static bool bShowDepthmap = false ; 
  
    public string W0="____";
    public Vector2 vWindowPosition0 = new Vector2(130,85);  
    public Vector2 vWindowSize0 = new Vector2(200,128);
    public string W1="____";
    public Vector2 vWindowPosition1 = new Vector2(130,85);    
    public Vector2 vWindowSize1 = new Vector2(200,128);
    
    private static string[] sLogs = new string[I_MAX_LOG+2];
    private static int iLogCount = 0; 
    private static int iLogID = 0; 
    
    private float fInputDelay = 0; 
    public string[] sGameControls;
    
    static private Rect rWindow;
    
    private Vector2 vScreenSize = new Vector2(Screen.width, Screen.height);
    
    private bool bShowHelp = false; 
    private bool bShowCursor = false;
    static private bool bInit = false;
    

    private bool bMouseTrigged = false ;   

    private Rect rScreenCenter;    
    private Rect rScreenFooter;
        
    private GUIStyle textStyle;

    void Awake()
    {       
        if( !bInit ) Init();
        textStyle = new GUIStyle();
        textStyle.normal.textColor = Color.white;
        textStyle.wordWrap = true;
        textStyle.richText = true; 
        sLogs[0] = "BEGIN" ;
        sLogs[I_MAX_LOG] = "END" ;
        for( int i = 1; i < I_MAX_LOG ; i++){
            sLogs[i] = "" ;
        }
        iLogID = 1 ;
        iLogCount = 1;
    }    
        
	void Update()
	{        
        if( !bInit ) Init();
        vScreenSize = new Vector2(Screen.width, Screen.height);
        
        rScreenCenter = new Rect( 
                                 (vScreenSize.x/2)-(vWindowSize0.x/2) + vWindowPosition0.x,
                                 (vScreenSize.y  - vWindowSize0.y ) + vWindowPosition0.y,
                                 vWindowSize0.x,            
                                 vWindowSize0.y
                                 );  
        rScreenFooter = new Rect( 
                                 (vScreenSize.x/2)-(vWindowSize1.x/2) + vWindowPosition1.x,
                                 (vScreenSize.y  - vWindowSize1.y ) + vWindowPosition1.y,
                                vWindowSize1.x,            
                                vWindowSize1.y
                                );    
        UpdateInputs();        
	}
	
    private void UpdateInputs()
    {

        if( Input.GetMouseButton( 0 ) && Input.GetMouseButton( 1 ) ){
            fInputDelay += Time.deltaTime;
            if( fInputDelay > F_TIME_BEFORE_QUIT ){
                bMouseTrigged = true;
                T.LogW("Quit in "+(F_TIME_BEFORE_QUIT-fInputDelay).ToString("f2") );
            }else{
                bMouseTrigged = false;
            }
        }

        if (Inputs.IsTrigger(KeysHelp.Escape) || bMouseTrigged)
        {
            fInputDelay = 0.0f;
            Application.Quit();
        }


        if (Inputs.IsTrigger(KeysHelp.Delete))
        {
            PlayerPrefs.DeleteAll ();
            T.LogW("Player pref erased");
        }

        if (Inputs.IsTrigger(KeysHelp.C))
        {
            bShowCursor = !bShowCursor;
            Screen.showCursor = bShowCursor;
            T.Log("Cursor toggle",TColors.BlueNavy);  
        }

        if (Inputs.IsTrigger(KeysHelp.H))
            bShowHelp =!bShowHelp;

        if (Inputs.IsTrigger(KeysHelp.F))
        {
            bShownFPS = !bShownFPS;
        }               
    }
    
	void OnGUI()
	{
		if( bShowHelp ){	
            GUI.Window( 0, rScreenCenter, windowAction, "Help" ); // should be centered. 
		}
		if( bShownFPS ){
			GUI.Window( 0, rScreenFooter, windowFPS, "FPS" ); 		    
        }
	}
	public static string GetLog()
    {
        return Helper.sLogs[iLogID];
    }

    public static void Init()
    {
        if( bInit ) return;        
        ArrayList Inputs = new ArrayList ();
        for( int i=0; i<staticInputs.Length; ++i){
            Inputs.Add(staticInputs[i]);
        }
        bInit = true;
    }
    
    public static void AddLog( string _log )
    {
        if( iLogCount < I_MAX_LOG )
        {
            sLogs[ iLogCount ] = _log;
            iLogCount++;
        }
        else{
            iLogCount = 0;
            sLogs[ iLogCount ] = _log;
            iLogCount++;
        }
        Helper.iLogID = iLogCount-1;
    }
    
    public static void ToggleDepthDisplay()
    {
        bShowDepthmap = !bShowDepthmap;
        T.ToggleVisibility( "[iisu]_DepthImage" );
    }

	private void windowFPS( int _windowID )
	{	
        GUILayout.TextField( FPSCounter.fFrameCount+" Fps" )	;
        GUI.DragWindow(new Rect(0, 0, 10000, 20));   
	}
	    
	private void windowAction( int _windowID )
	{	
        
        for( int i = 0 ; i < sGameControls.Length ; i++ ){
            GUILayout.TextField( sGameControls[i] );
            GUILayout.Space( 2 );
        }
		for( int i = 0 ; i < staticControls.Length ; i++ ){
            GUILayout.TextField( staticControls[i] );
            GUILayout.Space( 2 );
        }    
   		if (GUILayout.Button("Quit"))
            bShowHelp =!bShowHelp;
        GUI.DragWindow(new Rect(0, 0, 10000, 20));    
	}
}