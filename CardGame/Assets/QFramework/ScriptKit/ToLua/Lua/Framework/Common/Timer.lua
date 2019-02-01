Timer = class("Timer")


function Timer:ctor()
    self:Init(nil);
end

function Timer:Init(callback)
    self.mCallback = callback
    self.mWaitForRemove = false;
    self.mTimeToStop = false;
end

function Timer:Remove()
    self.mWaitForRemvoe = true;
end

function Timer:ReadyForRemove()
    return self.mWaitForRemvoe;
end

function Timer:TimeToStop()
    self.mTimeToStop = true
end

function Timer:TimeToPlay()
    self.mTimeToStop = false
end

function Timer:HasStopped()
    return self.mTimeToStop
end