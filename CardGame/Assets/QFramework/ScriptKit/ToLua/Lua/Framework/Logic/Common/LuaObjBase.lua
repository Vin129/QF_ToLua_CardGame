
LuaObjBase = class("LuaObjBase",ICommand)

function LuaObjBase:ctor(callerObj)
    self.mCallerObj = callerObj;
end

function LuaObjBase:Awake()
end

function LuaObjBase:Start()

end

function LuaObjBase:Update(deltaTime)

end

function LuaObjBase:LateUpdate(deltaTime)

end

function LuaObjBase:OnDestroy()

end