local PlayerVoMgr = class("PlayerVoMgr")

function PlayerVoMgr:init()
    self.tblPlayerVo = nil
end

function PlayerVoMgr:GetVo(cmplyid, bclone)
    if self.tblPlayerVo == nil then
        self.LoadAllFromConfig()
    end
    if bclone == nil or bclone == true then
        return clone(self.tblPlayerVo[cmplyid])
    end
    return self.tblPlayerVo[cmplyid]
end

function PlayerVoMgr:LoadAllFromConfig()
    self.tblPlayerVo = {}
    local tab = TableConfig:GetTable("common_player_data");
    for k,v in pairs(tab) do
        if tonumber(v.id) < 10009989 then	--过滤机器人
            local vo = {commonPlayerId = v.id};
            local player = require("Logic/Vo/PlayerVo").new(vo)
            self.tblPlayerVo[v.id] = player;
        end
    end
end

return PlayerVoMgr.new()