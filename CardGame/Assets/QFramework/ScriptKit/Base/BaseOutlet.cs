using QFramework;
public class BaseOutlet : Singleton<BaseOutlet>
{
    protected BaseOutlet()
    {

    }
	public override void OnSingletonInit(){
		//初始化  加载动态语言
		switch(ScriptBaseSetting.NOW_SCRIPT_TYPE){
			case 1 :
				LuaMain.getInstance();
			break;
		}
	}

	public void Init(){
		
	}
}
