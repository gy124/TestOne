''轴参数初始化
''转台(ECI2400_2,192.168.0.100)
''OTP
BASE(0)
ATYPE=4
INVERT_STEP=256*100+6
UNITS=1
FS_LIMIT=10000000
RS_LIMIT=-10000000
DATUM_IN=1
REV_IN=2
FWD_IN=-1
INVERT_IN(1,ON)
INVERT_IN(2,ON)
ALM_IN=24
INVERT_IN(24,ON)
INVERT_IN(5,ON)
INVERT_IN(6,ON)
INVERT_IN(7,ON)

'''''''''''''''''''光源控制
SETCOM(19200,8,1,0,0,0)
global dim sumchk	
global dim tempchar                    '接收的一个字节
global dim getbuff(20)                 '接收的支付串
global dim getnum                      '接收的字节数 
getnum = 0
global dim chset 
chset = 0 
global dim chcur 
chcur = 1 
runtask 0,get_char                    '启动接收字符串任务
'串口接收
global sub get_char()		 
	while 1
	GET #0,tempchar
	if tempchar = 76 then getnum = 0
	getbuff(getnum) = tempchar
	getnum = getnum + 1 
	if(getnum >=6 and getbuff(1)=71) then
		getnum =0				
		sumchk = getbuff(0)+getbuff(1)+getbuff(2)+getbuff(3)+getbuff(4)
		if sumchk = getbuff(5) then 
			chcur =getbuff(2)
			getbuff(5) =0
			TRACE chcur
		endif
	endif
	wend
end sub

global sub setch()
	if chcur <> chset then		
		PUTCHAR #0,76,66,0,1,0,143
		getnum =0	
		getbuff(5) =0
		if chset = 0 then 
			PUTCHAR #0,76,71,0,0,0,147
			endif
		if chset = 1 then 
			PUTCHAR #0,76,71,1,0,0,148
			endif
		if chset = 2 then 
			PUTCHAR #0,76,71,2,0,0,149
			endif
		if chset = 3 then 
			PUTCHAR #0,76,71,3,0,0,150
			endif
		DELAY(200)
	endif
end sub


''''''下位机特殊响应处理
while 1
	if IN(0)  THEN 			'如果(EMG)IN0被按下
		RAPIDSTOP(2)			'停止所有轴运动	
	ENDIF
	if in_scan(0,23) then                '扫描IN0-23口电平变化
		if in_event(4) < 0 then          'IN0下降沿触发
		OP(0,OFF)
		OP(1,OFF)
		endif
	endif
	 ' 0/270°感应时禁止反转
	if IN(6) AND IN(7) then
		OP(1,OFF)
	' 270°感应时禁止正转
	elseif IN(7) then
		OP(0,OFF)
	endif
	
	setch()
wend
end
