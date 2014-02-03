using UnityEngine;
using System.IO;
using System.Collections;
using System.Diagnostics;
using System.Text.RegularExpressions;

public enum formatingType{ 
	Timer = 0,		// 00'01"
	Decimal1, 	//   1.0
	Decimal2, 	//  1.00
    Decimal3,  // 1.000
    Decimal4, 	// 1.000
    Number1,  	//     1
    Number2,  	//    01
    Number3, 	//   001
    Number4		//  0001
};

public enum States{
    Idle = 0,
    Intro,
    Game,
    Score,
    Transition,
    Calibration,
    Looping,
    ANY,
    ALL
}

public enum ScreenFrame{
    In = 0,
    Out
}

public enum Hand{
    Right,
    Left,
    Both,
    Any,
}

public enum TColors{//TColorsolours
    Teal=0,
    Brown,
    Green,
    GreenLight,
    Olive,
    Purple,
    BlueNavy,
    BlueDark,
    Blue,
    BlueLight,
    Orange,
    Red,
    Black,
    Sliver,
    Grey,
    White
}

public class T : MonoBehaviour
{
    static public bool cameraControlable = false;



    static public string ciphers = "0123456789";
    //Static public T GetGameComponant<T>() where T : componant{}
    static public string[] sColours = new string[]{
        "teal",
        "Brown",
        "green",
        "lime",
        "olive",
        "purple",
        "navy",
        "Darkblue",
        "Blue",
        "lightblue",
        "orange",
        "red",
        "Black",
        "sliver",
        "grey",
        "white",
    };
    static public bool LowerEqual( Vector3 _vec3, float _value)
    {
        return( _vec3.x <= _value && _vec3.y <= _value && _vec3.z <= _value );
    } 
    static public bool Lower( Vector3 _vec3, float _value)
    {
        return( _vec3.x < _value && _vec3.y < _value && _vec3.z < _value );
    }    
    static public bool LowerPartial( Vector3 _vec3, float _value)
    {
        return( _vec3.x < _value || _vec3.y < _value || _vec3.z < _value );
    }

    static public bool BiggerEqual( Vector3 _vec3, float _value)
    {
        return( _vec3.x >= _value && _vec3.y >= _value && _vec3.z >= _value );
    }
    static public bool Bigger( Vector3 _vec3, float _value)
    {
        return( _vec3.x > _value && _vec3.y > _value && _vec3.z > _value );
    }
    static public bool BiggerPartial( Vector3 _vec3, float _value)
    {
        return( _vec3.x > _value || _vec3.y > _value || _vec3.z > _value );
    }

    static public float DecimalTrim( float _value, int _decimalCount )
    {
        float factor = Mathf.Pow(10.0f,_decimalCount);
        float result = (float)((int)(_value*factor))/factor;
        return result;
    }
    
    static public Vector3 DecimalTrim( Vector3 _value, int _decimalCount )
    {
        Vector3 result = new Vector3();
        result.x = DecimalTrim( _value.x, _decimalCount);
        result.y = DecimalTrim( _value.y, _decimalCount);
        result.z = DecimalTrim( _value.z, _decimalCount);
        return result;
    }

    static public float ToFloat( string _text)
    {
        Log("->Float construction<- param"+_text,TColors.Orange);
        float result = 0.0f;
        int unit = 0;
        int decimals = 0;
        int decimalNumber1 = 0;
        bool hasDecimal = false;
        
        for( int i = 0; i< _text.Length ; i++){
            //Letter i in text
            if( _text[i] == ',' || _text[i] == '.' ){
                hasDecimal = true;
            }
            for( int n = 0; n< ciphers.Length ; n++){
                //Ciffer
                if( _text[i] == ciphers[n] ){
                    if( !hasDecimal ){
                        if( unit != 0 ){
                        }else{
                            unit *= 10;//Add rank
                            unit += n;
                            Log("Unit ="+unit,TColors.Orange);
                        }
                    }else{
                        if( decimals != 0 ){
                        }else{
                            decimals *= 10;//Add rank
                            decimals += n;
                            decimalNumber1++;
                            Log("decimals ="+decimals,TColors.Orange);
                        }
                    }
                }
            }
        }
        result = (float)( unit + ( decimals * Mathf.Pow(10.0f,-decimalNumber1) ) ) ;
        Log("Result : "+result.ToString(),TColors.Orange);
        return result;
    }    

    static public bool IsKeyValid( string _keyName)
    {        
        if( PlayerPrefs.HasKey( _keyName) ){
            T.Log("<color=grey>Search " + _keyName + " in playerPref :</color>-->ok", TColors.Green);
            return true;
        }
        T.Log("<color=grey>Search "+_keyName+" in playerPref :</color>-->Bad Request",TColors.Brown);
        return false;
    }
    
    static public int GetFromPref_i( string _keyName)
    {
        return PlayerPrefs.GetInt( _keyName );
    }
    static public float GetFromPref_f( string _keyName)
    {   
        return PlayerPrefs.GetFloat( _keyName );
    }
    static public string GetFromPref_s( string _keyName)
    {
        return PlayerPrefs.GetString( _keyName );
    }
    static public Vector3 GetFromPref_v( string _keyName)
    {
        Vector3 result = new Vector3();
        result.x = GetFromPref_f(_keyName+"_x" );
        result.y = GetFromPref_f(_keyName+"_y" );
        result.z = GetFromPref_f(_keyName+"_z" );
        return result ;
    }

    static public void SaveToPref_i( string _keyName, int _value ){
        PlayerPrefs.SetInt( _keyName, _value );
    }    
    static public void SaveToPref_f( string _keyName, float _value ){
        PlayerPrefs.SetFloat( _keyName, _value );
    }
    static public void SaveToPref_s( string _keyName, string _value ){
        PlayerPrefs.SetString( _keyName, _value );
    }    
    static public void SaveToPref_v( string _keyName, Vector3 _value){
        SaveToPref_f(_keyName,-255.0f);
        SaveToPref_f(_keyName+"_x",_value.x);
        SaveToPref_f(_keyName+"_y",_value.y);
        SaveToPref_f(_keyName+"_z",_value.z);
    }

    static public Transform LoadT( string _name )
    {
        GameObject newGameObject = GameObject.Find( _name );
        if( newGameObject == null ){
            Log("GameObject called <color=brown>"+_name+"<b>NOT</b> FOUND</color> in the scene",TColors.White);
            newGameObject = new GameObject();
        }
        return newGameObject.transform;
    }

    static public GameObject LoadGO( string _name )
    {
        GameObject newGameObject = GameObject.Find( _name );
        if( newGameObject == null ){
            Log("GameObject called <color=brown>"+_name+"<b>NOT</b> FOUND</color> in the scene",TColors.White);
            newGameObject = new GameObject();
        }
        return newGameObject;
    }

    static public TextMesh LoadTxMsh( string _name )
    {
        GameObject go = GameObject.Find( _name );
        if( go != null ){
            TextMesh newTextMesh = go.GetComponent( typeof( TextMesh ) )as TextMesh;
            Log("TextMesh called <color=brown>"+_name+"<b>NOT</b> FOUND</color> in the scene",TColors.White);
            return newTextMesh;
        }
        return null;
    }

    static public void ToggleCameraControl()
    {
        cameraControlable = !cameraControlable;
        Log("Camera control "+cameraControlable);
    }
    
    static public void Visible( GameObject _go )
    {
        if( _go != null ){
            Visibility v = _go.GetComponent (typeof(Visibility)) as Visibility;
            if( v != null ){
                v.SetVisible();
            }else
                T.Log(_go+" has <color=brown><b>NO</b></color> Visibility script",TColors.White);
        }
        else
            T.Log(_go+" <color=brown><b>NOT</b></color> Valid",TColors.White);
    }

    static public void Invisible( GameObject _go )
    {
        if( _go != null ){
            Visibility v = _go.GetComponent (typeof(Visibility)) as Visibility;
            if( v != null ){
                v.SetInvisible();
            }else
                T.Log(_go+" has <color=brown><b>NO</b></color> Visibility script",TColors.White);
        }
        else
            T.Log(_go+" <color=brown><b>NOT</b></color> Valid",TColors.White);
    }

    static public void ToggleVisibility( GameObject _go )
    {
        if( _go != null ){
            Visibility v = _go.GetComponent (typeof(Visibility)) as Visibility;
            if( v != null ){
                v.Toggle();
            }else
                T.Log(_go+" has <color=brown><b>NO</b></color> Visibility script",TColors.White);
        }
        else
            T.Log(_go+" <color=brown><b>NOT</b></color> Valid",TColors.White);
    }
    
    static public void Visible( string _assetName )
    {
        GameObject go = LoadGO (_assetName);
        if( go != null ){
            Visibility v = go.GetComponent (typeof(Visibility)) as Visibility;
            if( v != null ){
                v.SetVisible();                
            }else            
                T.Log(go+" has <color=brown><b>NO</b></color> Visibility script",TColors.White);           
        }
        else
            T.Log(go+" <color=brown><b>NOT</b></color> Valid",TColors.White);
    }
    
    static public void Invisible( string _assetName )
    {
        GameObject go = LoadGO (_assetName);
        if( go != null ){
            Visibility v = go.GetComponent (typeof(Visibility)) as Visibility;
            if( v != null ){
                v.SetInvisible();                
            }else            
                T.Log(go+" has <color=brown><b>NO</b></color> Visibility script",TColors.White);           
        }
        else
            T.Log(go+" <color=brown><b>NOT</b></color> Valid",TColors.White);
    }
    
    static public void ToggleVisibility( string _assetName )
    {
        GameObject go = LoadGO (_assetName);
        if( go != null ){
            Visibility v = go.GetComponent (typeof(Visibility)) as Visibility;
            if( v != null ){
                v.Toggle();
                T.Log(_assetName+" Visibility toggle",TColors.White);                
            }else            
                T.Log(_assetName+" has <color=brown><b>NO</b></color> Visibility script",TColors.White);           
        }
        else
            T.Log(_assetName+" <color=brown><b>NOT</b></color> Valid",TColors.White);
    }
  
    static public bool GetHierarchyVisibility( Transform _transform )
    {
        int iChildCount = _transform.childCount;
        bool result = false;
        if( iChildCount == 0 )return false;
        for(int i = 0; i < iChildCount; i++){
            GameObject child = _transform.GetChild(0).gameObject;
            if( child.renderer != null ){
                return child.renderer.enabled;
            }
            else result = GetHierarchyVisibility( child );        
        }
        return result;     
    } 
    
    static public bool GetHierarchyVisibility( GameObject _object )
    {
        if( !(_object.renderer == null ))
            return _object.renderer.enabled ;
        bool result = false;
        Transform parent = _object.transform;
        int iChildCount = parent.childCount; 
        
        for(int i = 0; i < iChildCount; i++){
            GameObject child = parent.GetChild(0).gameObject;
            if( child.renderer != null ){
                return child.renderer.enabled;
            }
            else result = GetHierarchyVisibility( child); 
        }
        return result;      
    }
    
    static public void SetHierarchyVisibility( Transform _transform, bool _state )
    {
        if (!(_transform.renderer == null)) _transform.renderer.enabled = _state;
        if( _transform.childCount != 0 ){
            //foreach child: Hide his hierachy
            for(int i = 0; i < _transform.childCount; i++){
                GameObject child = _transform.GetChild(i).gameObject;
                SetHierarchyVisibility( child, _state ); 
            }
        }
    }

    static public void SetHierarchyVisibility( GameObject _object, bool _state )
    {
        //Hide Root if is rendered
        if( !(_object.renderer == null )) _object.renderer.enabled = _state;
        Transform parent = _object.transform;
        if( parent.childCount != 0 ){
            //foreach child: Hide his hierachy
            for(int i = 0; i < parent.childCount; i++){
                GameObject child = parent.GetChild(i).gameObject;
                SetHierarchyVisibility( child, _state ); 
            }
        }
    }
    
    static public void SetHierarchyEnable( Transform _transform, bool _state )
    {
        //Hide Root if is rendered
        if( !(_transform.gameObject.activeSelf == false )) _transform.gameObject.SetActive(_state);
        if( _transform.childCount != 0 ){
            //foreach child: Hide his hierachy
            for(int i = 0; i < _transform.childCount; i++){
                GameObject child = _transform.GetChild(i).gameObject;
                SetHierarchyEnable( child, _state ); 
            }
        }
    }
    
    static public void SetHierarchyEnable( GameObject _object, bool _state )
    {
        //Hide Root if is rendered
        if( !(_object.activeSelf == false )) _object.SetActive(_state);
        Transform parent = _object.transform;
        if( parent.childCount != 0 ){
            //foreach child: Hide his hierachy
            for(int i = 0; i < parent.childCount; i++){
                GameObject child = parent.GetChild(i).gameObject;
                SetHierarchyEnable( child, _state ); 
            }
        }
    }

	static public void CameraControls()
	{
		//Zoom out
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			if (Camera.main.fieldOfView<=100)
				Camera.main.fieldOfView +=2;
			if (Camera.main.orthographicSize<=20)
				Camera.main.orthographicSize +=0.5f;
		}
		
		//Zoom In
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			if (Camera.main.fieldOfView>2)
				Camera.main.fieldOfView -=2;
			if (Camera.main.orthographicSize>=1)
				Camera.main.orthographicSize -=0.5f;
		}
		
		//Orth/Persp
		if (Input.GetKeyUp(KeyCode.O))
		{
			if (Camera.main.orthographic == true)
				Camera.main.orthographic = false;
			else
				Camera.main.orthographic=true;
		}
	}

	static public Vector2 V2( float _value )
	{
		return new Vector2( _value, _value );
	}
		
	static public Vector2 Vectorize2( float _value )
	{
		return V2(_value );
	}
	
	static public Vector3 V3( float _value )
	{
		return new Vector3( _value, _value, _value );
	}
		
	static public Vector3 Vectorize3( float _value )
	{
		return V3 ( _value );
	}
		
	static public Vector4 V4( float _value )
	{
		return new Vector4( _value, _value, _value, _value );
	}
		
	static public string FormatText( float _valueToFormat, formatingType _fType )
	{
		return FormatText( _valueToFormat, _fType, false,"" );
	}
	
	static public string FormatText( float _valueToFormat, formatingType _fType, bool _ajusted ,string _unit )
	{
		int iUnit = (int)_valueToFormat;
        int iDecimal = (int)((_valueToFormat-iUnit)*100);
        string valueString="";
        string sExtra = "";
		
		switch( _fType ){

            case formatingType.Timer :
                if( (int)_valueToFormat < 10 )
                    if( iDecimal < 10 )
                        valueString = "0"+iUnit+"\"0"+iDecimal;
                else
                    valueString = "0"+iUnit+"\""+iDecimal;
                else
                    if( iDecimal < 10 )
                        valueString = iUnit+"\"0"+iDecimal;
                else
                    valueString = iUnit+"\""+iDecimal;
                break;
                
            case formatingType.Decimal1: 
                if( _valueToFormat < 10.0f )
                    sExtra = "0";               
                valueString = (_valueToFormat).ToString("f1")+" "+_unit;
                break;                
            case formatingType.Decimal2:
                //if( _valueToFormat < 10.0f )
                 //   sExtra = "0";
                valueString = sExtra+(_valueToFormat).ToString("f2")+" "+_unit;
                break;
            case formatingType.Decimal3:
                if( _valueToFormat < 10.0f )
                    sExtra = "0";             
                valueString = (_valueToFormat).ToString("f3")+" "+_unit;
                break;
            case formatingType.Decimal4:
                if( _valueToFormat < 10.0f )
                    sExtra = "0";            
                valueString = (_valueToFormat).ToString("f4")+" "+_unit;
                break;

            case formatingType.Number1 :
                valueString = iUnit.ToString("D1");
                break;
            case formatingType.Number2 :
                valueString = iUnit.ToString("D2");
                break;                
            case formatingType.Number3 :
                valueString = iUnit.ToString("D3");
                break;                
            case formatingType.Number4 :
                valueString = iUnit.ToString("D4");
                break;  
		}
        if( _ajusted )return AjustAlignement(_valueToFormat)+valueString;
        return valueString;
	}
	
	static public string AjustAlignement( float _valueToFormat )
	{
		if( _valueToFormat >= 10 && _valueToFormat < 100 ){
			return " ";
		}
		if( _valueToFormat < 10 ){
			return "  ";
		}
		return"";
	}
    
    static public void LogArray( ref int[] _array )
    {
        for( int i = 0 ; i <_array.Length; i++){        
            Log(_array.ToString()+"["+i+"] ="+_array[i],TColors.White);  
        }
    }
    
    static public void LogArray( ref float[] _array )
    {
        for( int i = 0 ; i <_array.Length; i++){        
            Log(_array.ToString()+"["+i+"] ="+_array[i],TColors.White);  
        }
    }
    
    static public void LogArray( ref string[] _array )
    {
        for( int i = 0 ; i <_array.Length; i++){        
            Log(_array.ToString()+"["+i+"] ="+_array[i],TColors.White);  
        }
    }
    
    static public void LogArray( ref Vector3[] _array )
    {        
        for( int i = 0 ; i <_array.Length; i++){        
            Log(_array.ToString()+"["+i+"] ="+_array[i],TColors.White);  
        }
    }
    
    static public void LogArray( ref Object[] _array )
    {
        for( int i = 0 ; i <_array.Length; i++){        
            T.Log(_array.ToString()+"["+i+"] ="+_array[i],TColors.White);  
        }
    }

    static public void DbgLog( string _message )
    {
        #if DEBUG
            Log("<color=brown>PrintDebug</color>"+_message);
        #endif
    }
    
    static public void DbgLog( string _message, TColors _color )
    {        
        #if DEBUG
            Log("<color=brown>PrintDebug</color>"+_message,_color );
        #endif
    }

    static public void Log( string _message )
    {
        string callData = GetCallParams();    
        UnityEngine.Debug.Log(callData+_message);                     
        Helper.AddLog( _message );
    }
    
    static public void Log( string _message, TColors _color)
    {
        string callData = GetCallParams();         
        UnityEngine.Debug.Log(callData+"<color="+sColours[(int)_color]+">"+_message+"</color>");                
        Helper.AddLog( _message );      
    }

    static public void LogW( string _message)
    {       
        string callData = GetCallParams();         
        UnityEngine.Debug.LogWarning(callData+_message);                
        Helper.AddLog( _message ); 
    }
    
    static public void LogW( string _message, TColors _color )
    {        
        string callData = GetCallParams();         
        UnityEngine.Debug.LogWarning(callData+"<color="+sColours[(int)_color]+">"+_message+"</color>");                
        Helper.AddLog( _message ); 
    }  
    
    static private string GetCallParams()
    {
        for( int i = 2; i> 0; i-- ){
            StackFrame CallStack = new StackFrame(i, true);
            if( CallStack != null &&  CallStack.GetMethod() != null ){
                string sMethodNameFull = CallStack.GetMethod().ToString();
                Regex regex = new Regex(string.Format("\\{0}.*?\\{1}", "(", ")"));
                string sMethodNameShort = regex.Replace(sMethodNameFull, string.Empty);
                sMethodNameShort = sMethodNameShort.Substring(sMethodNameShort.IndexOf(' ') + 1);
                
                string sFilenameFull = CallStack.GetFileName();
                string sFilename = Path.GetFileName(sFilenameFull);
                int line = CallStack.GetFileLineNumber();

                return "<color=grey>>"+sFilename+"/"+sMethodNameShort+":"+line+"-></color>\n" ;      
            }      
        }
        return "" ;    
    }  
}