''�������ʼ��
''�ҹ���(ECI2400_2,192.168.0.102)
''X1(����),X2(Զ��),Z1(��̬),Z2(DUST)
BASE(0,1,2,3)
ATYPE=4,4,4,4
INVERT_STEP=256*100+6,256*100+6,256*100+6,256*100+6
UNITS=1,1,1,1
FS_LIMIT=10000000,10000000,10000000,10000000
RS_LIMIT=-10000000,-10000000,-10000000,-10000000
DATUM_IN=1,3,5,7
REV_IN=1,3,6,8
INVERT_IN(1,ON)
INVERT_IN(3,ON)
INVERT_IN(5,ON)
INVERT_IN(7,ON)
FWD_IN=2,4,5,7
INVERT_IN(2,ON)
INVERT_IN(4,ON)
INVERT_IN(6,ON)
INVERT_IN(8,ON)
ALM_IN=24,25,26,27
INVERT_IN(24,ON)
INVERT_IN(25,ON)
INVERT_IN(26,ON)
INVERT_IN(27,ON)

'''''''''''''''''''��Դ����
SETCOM(19200,8,1,0,0,0)
global dim sumchk	
global dim tempchar                    '���յ�һ���ֽ�
global dim getbuff(20)                 '���յ�֧����
global dim getnum                      '���յ��ֽ��� 
getnum = 0
global dim chset 
chset = 0 
global dim chcur 
chcur = 1 
runtask 0,get_char                    '���������ַ�������
'���ڽ���
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


''''''��λ��������Ӧ����
while 1
	if IN(0)  THEN 			'���(EMG)IN0������
		RAPIDSTOP(2)			'ֹͣ�������˶�	
	ENDIF
	setch()
wend
end
