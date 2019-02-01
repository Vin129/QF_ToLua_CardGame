--
-- Author: Your Name
-- Date: 2016-10-29 00:25:47
--

FRAMEWORK_INITED = false

if not FRAMEWORK_INITED then
	require ("Framework/Define")
	require ("Framework/Utility")
	require ("Framework/MsgDispatcher")
	require ("Framework/Function")
	require ("Framework/FSM")	
	require ("Framework/LuaBehaviour")

	--创建lua文件
	function CreateLuaFile(luaFilePath,gameObject)
		log("CreateLuaFile:"..luaFilePath)
		local luaTable = nil
		luaTable = require(luaFilePath).new()
		return luaTable
	end

	FRAMEWORK_INITED = true
	log("Init Sucess")
end 