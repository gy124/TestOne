using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MotionCtrl;
using System.IO;
using System.Drawing;
using System.ComponentModel;

namespace UI
{
    public class Product
    {
      public   const int MAXNUM = 999;
      public List<String> product_list = new List<string>();
      public Tray TrayGet, TrayBackOK, TrayBackTTNG, TrayBackPPNG, TrayBackAANG;
      #region 构造
      public Product()
        {
            TrayGet = new Tray(0);
            TrayBackOK = new Tray(1);
            TrayBackAANG = new Tray(2);         
            TrayBackPPNG = new Tray(3);
            TrayBackTTNG = new Tray(4);
            TrayList = new List<Tray>() { TrayGet, TrayBackOK, TrayBackTTNG, TrayBackPPNG };
         // testgroup = new TestGroup();

    }
      #endregion
      #region 单个模组状态
      public enum EM_CM_RES
        {
            [Description("待测")]
            UNTEST,
            [Description("空料")]
            NONE,
            [Description("OK")]
            OK,
            [Description("NG")]
            NG,
            [Description("错误")]
            ERR,
        }
        #endregion
        #region 单个模组测试信息

        public class TestRes
        {
            //工站名称
            public string gz_name;
            public int gz_num;// 工站的站号
            //工装编号
            public int test_box_num;
            //-1 --untest, 0--OK,ng_code
            public EM_CM_RES res;
            public double ct;

            public TestRes(string gz_name = "", int test_box_num = 0, EM_CM_RES res = EM_CM_RES.NONE,double ct = 0)
            {
                this.gz_name = gz_name;
                this.test_box_num = test_box_num;
                this.res = res;
                this.ct = ct;
            }
        }
        #endregion
        #region 模组位置编号信息
        public class CamModule
        {
            public string barcode;
            public int gz_num;// 工站的站号
            public TestRes cur_res = new TestRes();
            public List<TestRes> list_res = new List<TestRes>();
            public int index;//料盘索引
            public int col;//料盘列
            public int row;//料盘行
            public ST_XYZA pos;
           
            public string tray_barcode;
            public string LastAllRes;

            //复制测试模组
            public object Clone()
            {
                CamModule cm = new CamModule();
                cm.barcode = this.barcode;
                for (int i = 0; i < this.list_res.Count; i++)
                {
                    cm.list_res.Add(new TestRes(this.list_res[i].gz_name, this.list_res[i].gz_num, this.list_res[i].res, this.list_res[i].ct));
                }
                cm.cur_res = new TestRes(this.cur_res.gz_name, this.cur_res.gz_num, this.cur_res.res, this.cur_res.ct);
                cm.index = this.index;
                cm.col = this.col;
                cm.row = this.row;
                cm.pos = this.pos;
                cm.tray_barcode = this.tray_barcode;
             
                return cm;

            }
            public EM_CM_RES res
            {
                get
                {
                    if (list_res == null) return EM_CM_RES.NONE;
                    EM_CM_RES ng = EM_CM_RES.OK;
                    foreach (TestRes r in list_res)
                    {
                        if (r.res != EM_CM_RES.OK) ng = r.res;
                    }
                    //VAR.msg.AddMsg(Msg.EM_MSGTYPE.DBG,ng.ToString());
                    return ng;
                }
                set
                {
                    TestRes res = new TestRes();
                    res.res = value;
                    list_res.Add(res);
                }

            }
            //从服务器获取数据
            EM_RES GetResFrSvr()
            {

                return EM_RES.OK;
            }
            //数据上传服务器
            EM_RES SendResFrSvr()
            {

                return EM_RES.OK;
            }
        }
        #endregion
        #region 料盘
        public class Tray
        {
            //public event EventHandler DataChanged;
            public string barcode = "ABC";
            public int row = 4;
            public int col = 6;
            /// <summary>
            /// 左上角
            /// </summary>
            public ST_XYZA tl;
            /// <summary>
            /// 左下角
            /// </summary>
            public ST_XYZA bl;
            /// <summary>
            /// 右上角
            /// </summary>
            public ST_XYZA tr;
            public int id;
            public List<POS> PosList=new List<POS>();
            public POS id_pos;
            public POS xy_pos;
            public List<CamModule> list_tray_cam = new List<CamModule>();
           public  bool bUpdate;
            #region 描述符
            public string disc
            {
                get
                {
                    return string.Format("料盘{0}", id + 1);
                }
            }
            #endregion
            #region 参数存取
            public EM_RES LoadTrayCfg(string productname="")
            {
                productname = VAR.gsys_set.cur_product_name;
                //check
                if (productname.Length < 3)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化对应产品名{1}异常(<3个字符)!", disc, productname));
                    return EM_RES.PARA_ERR;
                }

                //产品参数
                string filename = string.Format("{0}\\product\\{1}\\traycfg.ini", Path.GetFullPath(".."), productname);

                if (!File.Exists(filename))
                {
                    File.Create(filename);
                    //VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化对应产品名{1}配置文件不存在!", disc, productname));
                    //return EM_RES.PARA_ERR;
                }

                IniFile inf = new IniFile(filename);
                string section = string.Format("TRAY{0}", id);
                row = inf.ReadInteger(section, "TARY_ROW", 1);
                col = inf.ReadInteger(section, "TARY_COL", 1);
                //tl.x = inf.ReadDouble(section, "TL_X", 1.000);
                //tr.x = inf.ReadDouble(section, "TR_X", 1.000);
                //bl.x = inf.ReadDouble(section, "BL_X", 1.000);
                //tl.y = inf.ReadDouble(section, "TL_Y", 1.000);
                //tr.y = inf.ReadDouble(section, "TR_Y", 1.000);
                //bl.y = inf.ReadDouble(section, "BL_Y", 1.000);

                return EM_RES.OK;
            }

            public EM_RES SavTrayCfg(string productname)
            {
                //check
                if (productname.Length < 3)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化对应产品名{1}异常(<3个字符)!", disc, productname));
                    return EM_RES.PARA_ERR;
                }

                //产品参数
                string filename = string.Format("{0}\\product\\{1}\\traycfg.ini", Path.GetFullPath(".."), productname);

                if (!File.Exists(filename))
                {
                    File.Create(filename);
                 //   VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化对应产品名{1}配置文件不存在!", disc, productname));
                   // return EM_RES.PARA_ERR;
                }

                IniFile inf = new IniFile(filename);
                string section = string.Format("TRAY{0}", id);
                inf.WriteInteger(section, "TARY_ROW", row);
                inf.WriteInteger(section, "TARY_COL", col);
                //inf.WriteDouble(section, "TL_X", tl.x);
                //inf.WriteDouble(section, "TR_X", tr.x);
                //inf.WriteDouble(section, "BL_X", bl.x);
                //inf.WriteDouble(section, "TL_Y", tl.y);
                //inf.WriteDouble(section, "TR_Y", tr.y);
                //inf.WriteDouble(section, "BL_Y", bl.y);
                return EM_RES.OK;
            }
            #endregion

            public void InitTrayData()
            {
                list_tray_cam.Clear();
                list_cam.Clear();
                for (int r = 0; r < row; r++)
                {
                    for (int c = 0; c < col; c++)
                    {
                        CamModule cm = new CamModule();
                        cm.tray_barcode = this.barcode;

                        cm.row = r;
                        cm.col = c;
                        cm.index = r * col + c;
                        cm.barcode = this.barcode + cm.index.ToString();
                        cm.list_res.Clear();
                        TestRes res = new TestRes();
                        if (id == 0 || id == 1)
                            cm.cur_res.res = EM_CM_RES.UNTEST;
                        else
                            cm.cur_res.res = EM_CM_RES.NONE;
                        //Random rdm = new Random();
                        // res.res = (EM_CM_RES)rdm.Next(0, 4);
                        res.res = EM_CM_RES.NONE;

                        cm.list_res.Clear();
                        cm.list_res.Add(res);
                        Thread.Sleep(1);

                        list_tray_cam.Add(cm);
                        list_cam.Add(cm);
                    }
                }
            }

            public void UpTrayData()
            {
                //if (VAR.gsys_set.bfirst_station)
                //{
                //查询二维码并清除二维码
                foreach (CamModule cm in this.list_tray_cam)
                {
                    cm.list_res.Clear();
                    TestRes res = new TestRes();
                    if (id == 0 || id == 1)
                        cm.cur_res.res = EM_CM_RES.UNTEST;
                    else
                        cm.cur_res.res = EM_CM_RES.NONE;
                    res.res = EM_CM_RES.NONE;
                    cm.list_res.Add(res);

                }
                //}
                //else
                //{
                //    foreach (CamModule cm in this.list_tray_cam)
                //    {
                //      //  根据index下载数据
                //        //如果没有找到数据
                //         cm.cur_res.res = EM_CM_RES.NONE;

                //        if (id == 0 || id == 1)
                //            cm.cur_res.res = EM_CM_RES.UNTEST;
                //    }
                //}
            }
            
            public bool bEmpty
            {
                get 
                {
                    if (get_id == MAXNUM)   return true;
                    else return false;
                }
            }
            public bool bFull
            {             
                  get 
                {
                    if (put_id == MAXNUM)   return true;
                    else return false;
                }                              
            }
            public int get_id
            {
                get
                {
                         for (int i=0;i<list_mask.Count;i++)
                       {
                           if(list_mask[i] ==true)
                           {
                               return i;
                           }
                       }

                         return MAXNUM;
                }

            }
            public int put_id
            {
                get
                {
                    for (int i = 0; i < list_mask.Count; i++)
                    {
                        if (list_mask[i] == false)
                        {
                            return i;
                        }
                    }

                    return MAXNUM;
                }

            }
            int cam_id;
            public ST_XYZA[][] PointArray;//所有点位置
            public ST_XYZA[][] allpos
             {
                 get
                 {
                     if (PosList[0]!= null)
                     {
                         tl = PosList[0].pos_xyza;
                     }
                     else throw new Exception("料盘缺少位置定义");  
                     if (PosList[1] != null)
                     {
                         tr = PosList[1].pos_xyza;
                     }
                     else throw new Exception("料盘缺少位置定义");  
                     if (PosList[2] != null)
                     {
                         bl = PosList[2].pos_xyza;
                     }
                     else throw new Exception("料盘缺少位置定义");  
                   EM_RES ret=  LoadTrayCfg(VAR.gsys_set.cur_product_name);
                   if (ret == EM_RES.OK)
                   {
                     PointArray=  Utility.Array(tl, tr, bl, col, row); 
                       return PointArray;
                   }
                   else
                         throw new Exception("加载位置数据失败");
                 }
             }
            //List<CamModule> _list_cam;
            public List<CamModule> list_cam = new List<CamModule>();
            /// <summary>
            /// 有料标记列表
            /// </summary>
            public List<bool> list_mask = new List<bool>();

           
             public Tray(int id=0)
            {
                this.id = id;
                this.barcode = "ab" + id.ToString();
                bUpdate = false;
                //随机生成 
                //this.OnDataChanged(new EventArgs());
            }
          
            public Tray(ST_XYZA star , ST_XYZA mrow, ST_XYZA mline,int row = 6, int col = 8, EM_CM_RES res = (EM_CM_RES )(- 1) )
            {
                this.row = row;
                this.col = col;           
                //随机生成
                list_cam.Clear();
                list_mask.Clear();
                for (int r = 0; r < row; r++)
                {
                    for (int c = 0; c < col; c++)
                    {
                        CamModule cm = new CamModule();
                        Random rdm = new Random();
                      
                        list_cam.Add(cm);
                        list_mask.Add(false);//生成满盘
                        Thread.Sleep(1);
                    }
                }
                tl = star;
                tr = mrow;
                bl = mline;
                PointArray = Utility.Array(tl, tr, bl, col, row); 
            }

            public void NewTray(bool bmk = false, EM_CM_RES res = EM_CM_RES.UNTEST)//true 空盘，false满盘
            {

                InitTrayData();
                list_mask.Clear();
                for (int r = 0; r < row; r++)
                {
                    for (int c = 0; c < col; c++)
                    {
                       
                        list_mask.Add(bmk);//生成满盘
                        Thread.Sleep(1);
                    }
                }
                PointArray = Utility.Array(tl, tr, bl, col, row); 
                for (int n = 0; n < list_cam.Count; n++)
                {
                    list_cam[n].cur_res.res = res;
                    if (list_cam[n].list_res == null) list_cam[n].list_res = new List<TestRes>();
                    list_cam[n].list_res.Clear();
                    list_cam[n].list_res.Add(list_cam[n].cur_res);
                }
                
            }
            //public int count_ok
            //{
            //    get
            //    {
            //        if (list_cam == null || list_cam.Count == 0) return 0;
            //        int cnt = 0;
            //        foreach (CamModule cm in list_cam)
            //        {
            //            if (cm.res == EM_CM_RES.OK) cnt++;
            //        }
            //        return cnt;
            //    }
            //}

            //public int count_ng
            //{
            //    get
            //    {
            //        if (list_cam == null || list_cam.Count == 0) return 0;
            //        int cnt = 0;
            //        foreach (CamModule cm in list_cam)
            //        {
            //            if (cm.res != EM_CM_RES.OK) cnt++;
            //        }
            //        return cnt;
            //    }
            //}

            //从服务器获取数据
            //数据上传服务器
            /// <summary>
            /// 阵列计算点位
            /// </summary>
            //public EM_RES update_cam_id()
            //{
            //   for (int i=0;i<=list_mask.Count;i++)
            //   {
            //       if(list_mask[i] ==false)
            //       {
            //         cam_id=i;
            //           break;
            //       }
            //   }

            //   return EM_RES.OK;
            //}
            # region 料盘定位
            public EM_RES ToPosId(POS mpos, ref bool bquit, int id = -1)
            {
                EM_RES ret = EM_RES.OK;
                if (mpos != null)
                    ret = ToPosId(ref bquit, mpos.AxisX, mpos.AxisY, id);
                else
                    return EM_RES.ERR;
                return ret;
            }

            public EM_RES ToPosId(ref bool bquit, AXIS axX, AXIS axY,  int id = -1)
            {
                int mrow = 0;
                int mcol = 0;
                try
                {
                    POS idpos;
                    ST_XYZA mpos;
                    if (bquit)
                        return EM_RES.QUIT;
                    EM_RES ret = EM_RES.OK;
                    if (bEmpty) return EM_RES.PARA_ERR;
                    if (id == -1)
                    {
                        id = get_id; //自动检索id
                    }
                    if (!(id > -1 && id < col * row))
                        return EM_RES.PARA_ERR;
            
                    mrow = id / col ;
                    mcol = id % col ;
                    idpos = new POS(axX, axY, null, null, "料盘位置", 1, 7);
                    mpos = allpos[mrow][mcol];
                    idpos.WriteUpdatePos(mpos);
                    ret = idpos.MoveTo(ref bquit, true);
                    return ret;
                }
                catch
                {
                    
                    VAR.ErrMsg(  "料盘取料ToPosId()未知错误！");
                        return EM_RES.ERR;
                }
            }
            public EM_RES ToPosXY(ref bool bquit, int mrow = 1, int mcol = 1)
             {               
                 POS idpos;
                 ST_XYZA mpos;
                 try
                 {
                     if (bquit)
                         return EM_RES.QUIT;
                     EM_RES ret = EM_RES.OK;
                  

                         if (mrow > 0 && mrow <= row && mcol > 0 && mcol <= col)
                         {
                             idpos = new POS(PosList[0].AxisX, PosList[0].AxisY, PosList[0].AxisZ, PosList[0].AxisA, "料盘位置", 1, 7);
                             idpos.WriteUpdatePos(allpos[mrow][mcol]);
                             ret = idpos.MoveTo(ref bquit);
                             if (ret == EM_RES.OK)
                                 return EM_RES.OK;

                         }
                     return EM_RES.ERR;
                 }
                 catch (Exception e)
                 {

                     VAR.ErrMsg(e.ToString());
                     return EM_RES.ERR;
                 }


             }
            public EM_RES ToPosXY(ref bool bquit, AXIS axX, AXIS axY, int mrow = 1, int mcol = 1)
            {
                try
                {
                    POS idpos;
                    if (bquit)
                        return EM_RES.QUIT;
                    EM_RES ret = EM_RES.OK;
     
                        if (mrow > 0 && mrow <= row && mcol > 0 && mcol <= col)
                        {
                            idpos = new POS(axX, axY, null, null, "料盘位置", 1, 7);                     
                            idpos.WriteUpdatePos(allpos[mrow][mcol]);
                            ret = idpos.MoveTo(ref bquit);
                            if (ret == EM_RES.OK)
                                return EM_RES.OK;

                        }
                    return EM_RES.ERR;
                }
                catch (Exception e)
                {

                    VAR.ErrMsg(e.ToString());
                    return EM_RES.ERR;
                }

            }
            #endregion
            public EM_RES UpdateCamModulePos()
            {
                //make pos
               
                //update cammodule pos
                if (PointArray != null)
                {
                    foreach (CamModule cm in list_cam)
                    {
                        if (cm.row < PointArray.Length && cm.col < PointArray[0].Length)
                        {
                            cm.pos = allpos[cm.row][cm.col];
                        }
                        else
                            break;
                       
                    }
                }
                return EM_RES.ERR;
            }

            public EM_RES GetCamModule(ref CamModule cm, int idx)
            {
                //超范围
                if (idx >= list_cam.Count || idx >= list_mask.Count)
                {
                    cm = null;
                    return EM_RES.PARA_OUTOFRANG;
                }
                //空料
                if (list_mask.ElementAt(idx) == false)
                {
                    cm = null;
                    return EM_RES.PARA_ERR;
                }

                cm = list_cam.ElementAt(idx);
                return EM_RES.OK;
            }
            public EM_RES GetCamModule(ref CamModule cm,int row,int col)
            {
                return GetCamModule(ref cm,row * this.col + col);
            }
            /// <summary>
            /// 根据标记未从头选择
            /// </summary>
            public EM_RES GetCamModule(ref CamModule cm, EM_CM_RES res = EM_CM_RES.UNTEST )
            {
                for (int n = 0; n < list_cam.Count; n++)
                {
                    if ( list_mask[n]==true &&list_cam[n].res == res)
                    {
                        cm =list_cam[n];
                        return EM_RES.OK;
                    }
                }
                return EM_RES.END;
            }

            public EM_RES UpdateCamModuleSta(CamModule cm)
            {
                for(int n = 0;n<list_cam.Count;n++)
                {
                    if (cm.index == list_cam[n].index)
                    {
                        list_cam[n] = cm;
                        return EM_RES.OK;
                    }
                }
                return EM_RES.ERR;
            }
       }
        public List<Tray> TrayList;
        #endregion
        #region  测试
        public class TestGroup
        {
            private static readonly Object TestComLock = new object();
            public string disc = "测试工站";
            public enum EM_TEST_STA
            {
                NONE,
                UNTEST,
                DOING,
                OK,
                NG,
                ERR,
                UNUSED,
                UNKOWN
            }

            public enum EM_TEAM_STA
            {
                [Description("未知")]
                UNKNOWN,
                [Description("就绪")]
                REDAY,
                [Description("上下料中")]
                LOAD,
                [Description("测试中")]
                TEST,
                [Description("复位中")]
                HOME,
                [Description("错误")]
                ERR,
                [Description("停止")]
                STOP
            }
            #region 测试组
            public class TestTeam
            {
                #region 测试工位
                public class Test
                {
                    public int id;//组内测试工位ID
                    public CamModule cm = new CamModule();//测试模组信息
                    public EM_TEST_STA status;//测试状态
                    public EM_TEST_STA laststatus;
                    public int unknowtimes = 0;
                    public bool buse;
                    public int place_times = 0;

                    public Test()
                    { }
                    public Test(int id)
                    {
                        this.id = id;
                        // cm = null;
                        status = EM_TEST_STA.NONE;
                    }
                    public string disc
                    {
                        get
                        {
                            return string.Format("测试工位{0}", id + 1);
                        }
                    }
                }
                #endregion
                public int id;
                public int test_num;//组内工位个数           
                public List<Test> list_test = new List<Test>();//测试工位     
                public EM_TEAM_STA status;
                public int demo_ct = 0;
                public bool demo_test_start = false;
                public TestTeam()
                { }
                public TestTeam(int id)
                {
                    this.id = id;
                    status = EM_TEAM_STA.REDAY;
                    //list_test.Clear();
                    //for (int i = 0; i < test_num; i++)
                    //{
                    //    Test test = new Test(i);
                    //    list_test.Add(test);
                    //}
                }
                #region 描述符
                public string disc
                {
                    get
                    {
                        return string.Format("工站{0}", id + 1);
                    }
                }
                #endregion

                #region 测试组参数存取
                public EM_RES LoadCfg(string productname)
                {
                    //check
                    if (productname.Length < 3)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化对应产品名{1}异常(<3个字符)!", disc, productname));
                        return EM_RES.PARA_ERR;
                    }

                    //产品参数
                    string filename = string.Format("{0}\\product\\{1}\\testcfg.ini", Path.GetFullPath(".."), productname);

                    if (!File.Exists(filename))
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化对应产品名{1}配置文件不存在!", disc, productname));
                        return EM_RES.PARA_ERR;
                    }

                    IniFile inf = new IniFile(filename);
                    string section = string.Format("TEST_TEAM{0}", id);
                    test_num = inf.ReadInteger(section, "TEST_NUM", 1);
                    list_test.Clear();
                    for (int i = 0; i < test_num; i++)
                    {
                        Test test = new Test(i);
                        list_test.Add(test);
                    }
                    for (int i = 0; i < test_num; i++)
                    {
                        list_test[i].buse = inf.ReadBool(section, string.Format("TEST_BUSE{0}", i), false);
                        //                         if (!list_test[i].buse)
                        //                         {
                        //                             list_test[i].status = EM_TEST_STA.UNUSED;
                        //                         }
                    }
                    return EM_RES.OK;
                }

                public EM_RES SaveCfg(string productname, int testnum, bool ballupdate = false)
                {
                    //check
                    if (productname.Length < 3)
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化对应产品名{1}异常(<3个字符)!", disc, productname));
                        return EM_RES.PARA_ERR;
                    }

                    //产品参数
                    string filename = string.Format("{0}\\product\\{1}\\testcfg.ini", Path.GetFullPath(".."), productname);

                    if (!File.Exists(filename))
                    {
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化对应产品名{1}配置文件不存在!", disc, productname));
                        return EM_RES.PARA_ERR;
                    }

                    IniFile inf = new IniFile(filename);
                    string section = string.Format("TEST_TEAM{0}", id);
                    test_num = testnum;
                    inf.WriteInteger(section, "TEST_NUM", test_num);
                    if (ballupdate)
                    {
                        list_test.Clear();
                        for (int i = 0; i < test_num; i++)
                        {
                            Test test = new Test(i);
                            list_test.Add(test);
                            list_test[i].buse = true;
                        }
                    }

                    for (int i = 0; i < test_num; i++)
                    {
                        inf.WriteBool(section, string.Format("TEST_BUSE{0}", i), list_test[i].buse);
                        if (!list_test[i].buse)
                        {
                            list_test[i].status = EM_TEST_STA.UNUSED;
                        }
                    }
                    return EM_RES.OK;
                }
                #endregion

                #region 加载与保存测试组
                public EM_RES LoadTestTeam(string productname)
                {
                    EM_RES ret;
                    ret = LoadCfg(productname);
                    if (ret != EM_RES.OK) return ret;
                    return EM_RES.OK;
                }

                public EM_RES SaveTestTeam(string productname, int testnum, bool ballupdate = false)
                {
                    EM_RES ret;
                    ret = SaveCfg(productname, testnum, ballupdate);
                    if (ret != EM_RES.OK) return ret;
                    return EM_RES.OK;
                }
                #endregion

                #region 检测空的测试位
                public Test GetCurPlaceTest(List<Test> listtest)
                {
                    foreach (Test CurPlaceTest in listtest)
                    {
                        if (CurPlaceTest.status == EM_TEST_STA.NONE)
                        {
                            return CurPlaceTest;
                        }

                    }
                    return null;
                }
                #endregion

                #region 检测完成的测试位
                public Test GetCurPickTest(List<Test> listtest)
                {
                    foreach (Test CurPickTest in listtest)
                    {
                        if (CurPickTest.status != EM_TEST_STA.NONE)
                        {
                            return CurPickTest;
                        }

                    }
                    return null;
                }
                #endregion



            }
            #endregion
            public int testteam_num;
            public static bool IsConnect = false;
            public List<TestTeam> list_testteam = new List<TestTeam>();
            public TestTeam cur_testteam = new TestTeam();


            public TestGroup()
            {

                //list_testteam.Clear();
                //for (int i = 0; i < testteam_num; i++)
                //{
                //    TestTeam testteam = new TestTeam(i);
                //    list_testteam.Add(testteam);
                //}
            }





            #region 测试群参数存取
            public EM_RES LoadCfg(string productname)
            {
                //check
                if (productname.Length < 3)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化对应产品名{1}异常(<3个字符)!", disc, productname));
                    return EM_RES.PARA_ERR;
                }

                //产品参数
                string filename = string.Format("{0}\\product\\{1}\\testcfg.ini", Path.GetFullPath(".."), productname);

                if (!File.Exists(filename))
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化对应产品名{1}配置文件不存在!", disc, productname));
                    return EM_RES.PARA_ERR;
                }

                IniFile inf = new IniFile(filename);
                string section = string.Format("TEST_GROUP");
                testteam_num = inf.ReadInteger(section, "TESTTEAM_NUM", 1);
                return EM_RES.OK;
            }

            public EM_RES SaveCfg(string productname, int team_num)
            {
                //check
                if (productname.Length < 3)
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化对应产品名{1}异常(<3个字符)!", disc, productname));
                    return EM_RES.PARA_ERR;
                }

                //产品参数
                string filename = string.Format("{0}\\product\\{1}\\testcfg.ini", Path.GetFullPath(".."), productname);

                if (!File.Exists(filename))
                {
                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, string.Format("{0}初始化对应产品名{1}配置文件不存在!", disc, productname));
                    return EM_RES.PARA_ERR;
                }

                IniFile inf = new IniFile(filename);
                string section = string.Format("TEST_GROUP");
                testteam_num = team_num;
                inf.WriteInteger(section, "TESTTEAM_NUM", testteam_num);
                return EM_RES.OK;
            }
            #endregion


            #region 读写测试群数据
            public EM_RES LoadTestGroup(string productname)
            {
                EM_RES ret;
                ret = LoadCfg(productname);
                if (ret != EM_RES.OK) return ret;
                list_testteam.Clear();
                for (int i = 0; i < testteam_num; i++)
                {
                    TestTeam testteam = new TestTeam(i);
                    list_testteam.Add(testteam);
                    testteam.LoadTestTeam(productname);

                }
                return EM_RES.OK;
            }

            public EM_RES SaveTestGroup(string productname, int team_num, int test_num, bool ballupdate = false)
            {
                EM_RES ret;
                ret = SaveCfg(productname, team_num);
                if (ret != EM_RES.OK) return ret;
                if (ballupdate)
                {
                    list_testteam.Clear();
                    for (int i = 0; i < testteam_num; i++)
                    {
                        TestTeam testteam = new TestTeam(i);
                        list_testteam.Add(testteam);
                        testteam.SaveTestTeam(productname, test_num, ballupdate);

                    }
                }
                else
                {
                    for (int i = 0; i < testteam_num; i++)
                    {
                        list_testteam[i].SaveTestTeam(productname, test_num);
                    }
                }
                return EM_RES.OK;
            }
            #endregion


            #region 检测空的测试组
            public void GetCurTestTeam()
            {
                foreach (TestTeam curtesteam in list_testteam)
                {
                    if (curtesteam.status == EM_TEAM_STA.REDAY)
                    {
                        cur_testteam = curtesteam;
                        break;
                    }
                }
                cur_testteam = null;
            }
            #endregion

            #region 与测试机通信

            #region 复位
            public EM_RES ResetTM(ref string ErrMes, bool isdemo = false)
            {
                lock (TestComLock)
                {
                    StringBuilder sb = new StringBuilder(1024);
                    int nCount = 0;
                    try
                    {
                        if (isdemo) return EM_RES.OK;
                        //if (!HL.SetDeviceStatus("rest", 4, sb, ref nCount))
                        //{
                        //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "复位测试机数据握手失败!");
                        //    return EM_RES.ERR;
                        //}
                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.NOR, "复位测试机数据握手成功!");
                        return EM_RES.OK;
                    }
                    catch (Exception ex)
                    {
                        ErrMes = "复位测试机产生异常!信息:" + ex.ToString();
                        return EM_RES.ERR;
                    }
                }

            }
            #endregion

            //#region 启动模组测试
            //public EM_RES StartTM(Product.TestGroup.TestTeam tt, ref string ErrMes, bool isdemo = false)
            //{
            //    lock (TestComLock)
            //    {
            //        StringBuilder sb = new StringBuilder(1024);
            //        int nCount = 0;
            //        try
            //        {
            //            if (isdemo) return EM_RES.OK;
            //            string msg = String.Format("start,{0}", tt.id);
            //            foreach (Product.TestGroup.TestTeam.Test tt_test in tt.list_test)
            //            {
            //                if (!tt_test.buse)
            //                {
            //                    msg = msg + "," + "unused";
            //                }
            //                else
            //                {
            //                    msg = msg + "," + tt_test.cm.barcode;
            //                }
            //            }
            //            //if (!HL.SetDeviceStatus(msg, msg.Length, sb, ref nCount))
            //            //{
            //            //    ErrMes = string.Format(("启动工站{0}数据握手失败!"), tt.id);
            //            //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, ErrMes);
            //            //    return EM_RES.ERR;
            //            //}
            //            VAR.msg.AddMsg(Msg.EM_MSGTYPE.NOR, "启动工站{0}数据握手成功!");
            //            return EM_RES.OK;
            //        }
            //        catch (Exception ex)
            //        {
            //            ErrMes = string.Format(("启动工站{0}产生异常!信息:" + ex.ToString()), tt.id);
            //            return EM_RES.ERR;
            //        }
            //    }

            //}
            //#endregion

            //public EM_RES ReStartTM(int id, ref string ErrMes, bool isdemo = false)
            //{
            //    lock (TestComLock)
            //    {
            //        StringBuilder sb = new StringBuilder(1024);
            //        int nCount = 0;
            //        try
            //        {
            //            if (isdemo) return EM_RES.OK;
            //            string msg = String.Format("continue,{0}", id);
            //            //foreach (Product.TestGroup.TestTeam.Test tt_test in tt.list_test)
            //            //{
            //            //    msg = msg + "," + tt_test.cm.barcode;
            //            //}
            //            //if (!HL.SetDeviceStatus(msg, msg.Length, sb, ref nCount))
            //            //{
            //            //    ErrMes = string.Format(("重新启动工站{0}数据握手失败!"), id);
            //            //    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, ErrMes);
            //            //    return EM_RES.ERR;
            //            //}
            //            VAR.msg.AddMsg(Msg.EM_MSGTYPE.NOR, "重新启动工站{0}数据握手成功!");
            //            return EM_RES.OK;
            //        }
            //        catch (Exception ex)
            //        {
            //            ErrMes = string.Format(("重新启动工站{0}产生异常!信息:" + ex.ToString()), id);
            //            return EM_RES.ERR;
            //        }
            //    }
            //}
            #region 读取解释数据
            //public EM_RES GetDataTM(ref string ErrMes, bool isdemo = false)
            //{
            //    if (isdemo) return EM_RES.OK;
            //    lock (TestComLock)
            //    {
            //        // return EM_RES.OK;
            //        StringBuilder sb = new StringBuilder(1024);
            //        string Stemp;
            //        int nCount = 0;
            //        // bool ReadStatus = true;
            //        try
            //        {

            //            //if (HL.SetDeviceStatus("state", 5, sb, ref nCount) == false)
            //            //{
            //            //    //VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, "Set测试机状态错误!");
            //            //    // return EM_RES.ERR;
            //            //    //  ReadStatus = false;
            //            //}
            //            if (nCount > 0)
            //            {
            //                //解释数据
            //                Stemp = sb.ToString();
            //                //                            VAR.msg.AddMsg(Msg.EM_MSGTYPE.SAVE_NOR, Stemp);
            //                string[] teamsub = Stemp.Split(';');
            //                string[] testsub, cmsub;
            //                if (teamsub.Length < list_testteam.Count)
            //                {
            //                    ErrMes = "接收测试数据工站个数小于实际个数!";
            //                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, ErrMes);
            //                    return EM_RES.PARA_ERR;

            //                }
            //                for (int i = 0; i < list_testteam.Count; i++)
            //                {
            //                    testsub = teamsub[i].Split(',');
            //                    if (testsub.Length - 1 < list_testteam.Count)
            //                    {
            //                        ErrMes = "接收测试数据工站内的工位个数小于实际个数!";
            //                        VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, ErrMes);
            //                        return EM_RES.PARA_ERR;

            //                    }
            //                    for (int j = 0; j < list_testteam[i].list_test.Count; j++)
            //                    {
            //                        cmsub = testsub[j].Split('_');
            //                        if (list_testteam[i].id == int.Parse(cmsub[0]) && list_testteam[i].list_test[j].id == int.Parse(cmsub[1]))
            //                        {
            //                            //工站状态
            //                            switch (cmsub[2])
            //                            {
            //                                case "ready":
            //                                    list_testteam[i].status = EM_TEAM_STA.UNKNOWN;
            //                                    break;
            //                                case "resetting":
            //                                    list_testteam[i].status = EM_TEAM_STA.HOME;
            //                                    break;
            //                                case "running":
            //                                    list_testteam[i].status = EM_TEAM_STA.TEST;
            //                                    break;
            //                                case "wait":
            //                                    list_testteam[i].status = EM_TEAM_STA.REDAY;
            //                                    break;
            //                                case "error":
            //                                    list_testteam[i].status = EM_TEAM_STA.ERR;
            //                                    break;
            //                                case "unknow":
            //                                case "stop":
            //                                    list_testteam[i].status = EM_TEAM_STA.STOP;
            //                                    break;
            //                            }
            //                            if (list_testteam[i].status == EM_TEAM_STA.REDAY && list_testteam[i].list_test[j].status == EM_TEST_STA.DOING)
            //                            {
            //                                ////ng返回为"ng-0" 0为NG类型
            //                                //Regex r = new Regex("-");
            //                                //Match ngcode = r.Match(cmsub[3]);
            //                                if (ngcode.Success)
            //                                {
            //                                    string[] ngcodesub = cmsub[3].Split('-');
            //                                    cmsub[3] = ngcodesub[0];
            //                                }
            //                                switch (cmsub[3])
            //                                {
            //                                    case "ok":
            //                                        list_testteam[i].list_test[j].status = EM_TEST_STA.OK;
            //                                        break;
            //                                    case "error":
            //                                        list_testteam[i].list_test[j].status = EM_TEST_STA.NG;
            //                                        list_testteam[i].list_test[j].unknowtimes++;
            //                                        break;
            //                                    case "ng":
            //                                        list_testteam[i].list_test[j].status = EM_TEST_STA.NG;
            //                                        break;
            //                                    case "unkown":
            //                                        list_testteam[i].list_test[j].status = EM_TEST_STA.NG;
            //                                        list_testteam[i].list_test[j].unknowtimes++;
            //                                        //                                                     if (list_testteam[i].list_test[j].laststatus != list_testteam[i].list_test[j].status)

            //                                        break;
            //                                }
            //                                if (list_testteam[i].list_test[j].unknowtimes >= 3)
            //                                {
            //                                    list_testteam[i].list_test[j].unknowtimes = 0;
            //                                    ErrMes = string.Format("测试组{0}，工位{1}测试超时次数过多/r/n" + "请停机查看", i + 1, j + 1);
            //                                    VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, ErrMes);
            //                                    return EM_RES.PARA_ERR;
            //                                }
            //                                list_testteam[i].list_test[j].laststatus = list_testteam[i].list_test[j].status;
            //                            }
            //                        }
            //                        else
            //                        {
            //                            ErrMes = "接收测试数据工站号与工位号顺序不对!";
            //                            VAR.msg.AddMsg(Msg.EM_MSGTYPE.ERR, ErrMes);
            //                            return EM_RES.PARA_ERR;
            //                        }
            //                    }
            //                }
            //            }
            //            return EM_RES.OK;
            //        }
            //        catch (Exception ex)
            //        {
            //            ErrMes = "读测试机数据产生异常,信息:" + ex.ToString();
            //            return EM_RES.ERR;
            //        }

            //    }

            //}
            #endregion

            #endregion
        }
        public TestGroup testgroup;
        #endregion
        public string name = "";

        #region  加载
        public EM_RES LoadDat(string name = "", bool ifLoadTest = true)
        {
            EM_RES ret;
            //加载料盘数据
            foreach (Tray t in TrayList)
            {
                ret = t.LoadTrayCfg(name);
                if (ret != EM_RES.OK) return ret;
                t.InitTrayData();
            }
            //if (ifLoadTest)
            //{
            //    ret = testgroup.LoadTestGroup(name);
            //    if (ret != EM_RES.OK) return ret;
            //}
            return EM_RES.OK;
        }
        #endregion
        #region  保存
        public EM_RES SaveTrayDat(string name = "")
        {
            EM_RES ret;
            foreach (Tray t in TrayList)
            {
                ret = t.SavTrayCfg(name);
                if (ret != EM_RES.OK) return ret;

            }
            //ret = testgroup.SaveTestGroup(name);
            //if (ret != EM_RES.OK) return ret;
            return EM_RES.OK;
        }
        #endregion
        #region 加载产品list
        //加载产品列表，列表里无当前产品时返回true
        public bool LoadProductList( string cur_product = "")
        {
              System.Windows.Forms.ComboBox cb;
             cb = new System.Windows.Forms.ComboBox();
                try
                {
                    cb.Items.Clear();
                    product_list.Clear();
                DirectoryInfo dir = new DirectoryInfo(Path.GetFullPath("..") + "\\product\\");
                foreach (DirectoryInfo dChild in dir.GetDirectories("*"))
                {
                    if (dChild.Name.Length > 2)

                        cb.Items.Add(dChild.Name.ToString());
                    product_list.Add(dChild.Name.ToString());
                }
                int idx = cb.Items.IndexOf(cur_product);
                if (idx < 0) idx = 0;
                cb.SelectedIndex = idx;
                if (cur_product != cb.SelectedItem.ToString()) return true;
                else return false;
            }
            catch
            {
                VAR.ErrMsg("加载产品列表失败");
                return false;
            }
        }
        public bool LoadProductList(System.Windows.Forms.ListBox listbox, string cur_product = "")
        {
            listbox.Items.Clear();
            product_list.Clear();
            DirectoryInfo dir = new DirectoryInfo(Path.GetFullPath("..") + "\\product\\");
            foreach (DirectoryInfo dChild in dir.GetDirectories("*"))
            {
                if (dChild.Name.Length > 2)
                {
                    product_list.Add(dChild.Name.ToString());
                    listbox.Items.Add(dChild.Name.ToString());
                }
            }
            int idx = listbox.Items.IndexOf(cur_product);
            if (idx < 0) idx = 0;
            listbox.SelectedIndex = idx;
            if (cur_product != listbox.SelectedItem.ToString()) return true;
            else return false;
        }
        #endregion
    }
}
