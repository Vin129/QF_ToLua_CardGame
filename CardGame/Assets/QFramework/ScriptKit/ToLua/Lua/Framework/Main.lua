require("Common/LuaExtension")
require("Logic/Command/ICommand")
require("Logic/Common/UIView")
require("Logic/Common/DragItem")
require("Logic/Common/LuaConst")
require("Logic/Common/GameConfig")
require("Logic/Common/MsgConst")
require("Logic/Common/LuaObjBase")
require("Logic/Common/LuaObjMgr")
require("Logic/Data/ViewData")
require("Logic/Data/Protocol")
require("Logic/Command/CommandConst")
require("proto/Opcode")
require("Network/SocketProxy")
require("Logic/Util/SortTypeConst")

--初始化Base
require("Logic/Model/BaseModel")
require("Logic/Vo/BaseVo")

--主入口函数。从这里开始lua逻辑
CommandManager = require("Logic/Command/CommandManager")
WindowNameConst = require("Logic/Common/WindowNameConst")
WindowMgr = require("Logic/Common/WindowMgr")
LuaObjMgr = require("Logic/Common/LuaObjMgr")
CommonPrefabs = require("Logic/CommonPrefabs")
ServerDef = require("Network/ServerDef")
Network = require("Network/Network")
LocalStorage = require("Logic/Common/LocalStorage")
TableConfig = require("Logic/Common/TableConfig")
LocalText=require("Logic/LocalText")
TimerMgr = require("Common/TimerMgr")
User = require("Logic/Data/User")
InFormMgr = require("Logic/Data/InFormMgr")
UIUtil = require("Logic/Util/UIUtil")
PropertyUtil = require("Logic/Util/PropertyUtil")
JumpUtil = require("Logic/Util/JumpUtil")
AudioUtil = require("Logic/Util/AudioUtil")
SortUtil = require("Logic/Util/SortUtil")
ReportUtil = require("Logic/Util/ReportUtil")
TipsTypeConst = require("Logic/Util/TipsTypeConst")
local l_stateMgr = require("GameState/StateMachine")
RedpointMgr = require("Logic/Common/RedpointMgr")
GuideMgr = require("Logic/Common/GuideMgr")
PlayerVoMgr = require("Logic/Common/PlayerVoMgr")

function Main()					
	l_stateMgr:Init()
	CommandManager:PostCommand(CommandConst.EnterState,'Init')
end

function Update(fDeltaTime)
	if nil ~= TimerMgr then
		TimerMgr:Update(fDeltaTime);
	end
	CommandManager:Update()
	if nil ~= l_stateMgr then	
		l_stateMgr:Update(fDeltaTime);
	end
end

function Destroy()
	if nil ~= l_stateMgr then
		l_stateMgr:Uninit()
	end
	l_stateMgr = nil;
end

-- called by NetManager.cs
function DistributePacket(opcode,bytes)
	SocketProxy:Distribute(opcode,bytes)
end

-- called by NetManager.cs
function GetOpcodeName(opcode)
	if Opcode.NamesMap[opcode] then
		return Opcode.NamesMap[opcode]
	else
		return "Unknown opcode!"
	end
end

-- called by GSNetManager.cs
function SendHeartBeat()
	SocketProxy:SendPacket(Opcode.C2GS_Heartbeat,proto.proto.heartbeat_pb.C2GS_Heartbeat())
	SocketProxy:HandleProto(Opcode.GS2C_Heartbeat,function (resp)
		LuaHelper.GetGSNetManager():ReceiveHeartBeat()
		TimerMgr:SetServerTime(resp.serverTime)
	end)
end

-- called by GSNetManager.cs
function ReconnectServer(connected)
	if connected then
		SocketProxy.reconnectFailedTime = 0
		local rect = proto.proto.connect_pb.C2GS_Connect()
		rect.uid = ServerDef.U_SERVER_ID.."-"..ServerDef.U_ID
		rect.token = ServerDef.U_TOKEN
		rect.channel = ServerDef.ChannelId

		SocketProxy:SendPacket(Opcode.C2GS_Connect,rect)
		SocketProxy:HandleProto(Opcode.GS2C_Connect,function (recvData)
			-- reconnected
			
			log("reconnected!")
			WindowMgr:HideWaiting()
			UIUtil:showTip("连接服务器成功")
			SocketProxy:ClearAllHandleProto()
			User:EngineMgr():OnReconnect()
			dump(recvData)

			if l_stateMgr.mCurState == l_stateMgr.mStateList['MainTown'] then
				if recvData.gameTeamInfo then
					User:TeamMgr():init(recvData.gameTeamInfo)
				end
			end
		end)
	else
		if SocketProxy.reconnectFailedTime < 3 then
			SocketProxy.reconnectFailedTime = SocketProxy.reconnectFailedTime + 1
			WindowMgr:ShowWaiting(nil,true)
			UIUtil:showTip("重连失败！尝试第"..tostring(SocketProxy.reconnectFailedTime).."次重连")
		else
			SocketProxy:ShowLostConnection()
		end
	end
end

-- 切后台
function ApplicationFocusFalse()
	log("ApplicationFocusFalse")
	User:EngineMgr():OnApplicationFocusFalse()
end

-- 切回来
function ApplicationFocusTrue()
	log("ApplicationFocusTrue")
	User:EngineMgr():OnApplicationFocusTrue()
end

-- called by LocalizedText.cs component
function GetLocalText(code)
	return LocalText:GetString(code);
end
