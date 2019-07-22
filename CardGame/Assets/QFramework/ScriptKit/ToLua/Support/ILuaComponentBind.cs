namespace ScriptKit {
    public interface ILuaComponentBind
    {
        string LuaPath { get; set; }
        void BindLuaComponent();
        void LuaDispose();
        void CallLuaFunction(string funcName);
    }
}
