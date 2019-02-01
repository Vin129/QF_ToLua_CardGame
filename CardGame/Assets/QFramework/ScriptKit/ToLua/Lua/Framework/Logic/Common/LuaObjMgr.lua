
local LuaObjMgr = {}

local m_ObjList = {}

function LuaObjMgr.Awake(uniqueID,luaFileName,gameObject)
    if nil == m_ObjList[uniqueID] then   
        m_ObjList[uniqueID] = require(luaFileName).new(gameObject);
    end

    if nil ~= m_ObjList[uniqueID] then
        m_ObjList[uniqueID]:Awake();
    end
end

function LuaObjMgr.Start(uniqueID)
    if nil ~= m_ObjList[uniqueID] then
        m_ObjList[uniqueID]:Start();
    end
end

function LuaObjMgr.Update(uniqueID,deltaTime)
    if nil ~= m_ObjList[uniqueID] then
        m_ObjList[uniqueID]:Update(deltaTime);
    end
end

function LuaObjMgr.LateUpdate(uniqueID,deltaTime)
    if nil ~= m_ObjList[uniqueID] then
        m_ObjList[uniqueID]:LateUpdate(deltaTime);
    end
end

function LuaObjMgr.OnDestroy(uniqueID)
    if nil ~= m_ObjList[uniqueID] then
        m_ObjList[uniqueID]:OnDestroy(deltaTime);
        m_ObjList[uniqueID] = nil;
    end
end

return LuaObjMgr