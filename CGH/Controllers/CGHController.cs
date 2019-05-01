using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CGH.Models;
using Dapper;
using System.Web.Configuration;   // 透過Web.config管理DB連結字串
using System.Data;
using System.Data.SqlClient;
using System.Web.Security;
namespace CGH.Controllers
{
    [Authorize(Roles = "999")]
    public class CGHController : Controller
    {
        private CGHContext _db = new CGHContext();

        Models.Repo.IProve _IP = new Models.Repo.Prove();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {

                _db.Dispose();
            }
            base.Dispose(disposing);
        }
        // GET: CGH
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CertificateList()
        {
            //第一種寫法：  //*** 查詢結果是一個 IQueryable *******************************************
            IQueryable<CertificateContent> ListAll = from _CerContent in _db.CertificateContents
                                                     where _CerContent.Category == 1
                                                     select _CerContent;


            if (ListAll == null)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {
                return View(ListAll.ToList());
                // 直到程式的最後，把查詢結果 IQueryable呼叫.ToList()時，上面那一段LINQ才會真正被執行！
            }


        }
        public ActionResult BabyList()
        {
            //第一種寫法：  //*** 查詢結果是一個 IQueryable *******************************************
            IQueryable<CertificateContent> ListAll = from _CerContent in _db.CertificateContents
                                                     where _CerContent.Category == 3
                                                     select _CerContent;


            if (ListAll == null)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {
                return View(ListAll.ToList());
                // 直到程式的最後，把查詢結果 IQueryable呼叫.ToList()時，上面那一段LINQ才會真正被執行！
            }


        }
        public ActionResult LeaveList()
        {
            //第一種寫法：  //*** 查詢結果是一個 IQueryable *******************************************
            IQueryable<CertificateContent> ListAll = from _CerContent in _db.CertificateContents
                                                     where _CerContent.Category == 2
                                                     select _CerContent;


            if (ListAll == null)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {
                return View(ListAll.ToList());
                // 直到程式的最後，把查詢結果 IQueryable呼叫.ToList()時，上面那一段LINQ才會真正被執行！
            }


        }

        public ActionResult Certificate()
        {  
            var last = from _c in _db.CertificateContents
                       orderby _c.ContentNO descending
                       select _c.ContentNO;

            ViewBag.ContentNO = last.FirstOrDefault() + 1;


            return View();
        }
        [HttpPost, ActionName("Certificate")]   // 把下面的動作名稱，改成 CreateConfirm 試試看？
        [ValidateAntiForgeryToken]   // 避免CSRF攻擊
        public ActionResult Certificate(CertificateContent _cer)
        {
            if ((_cer != null) && (ModelState.IsValid))
            // ModelState.IsValid，通過表單驗證（Server-side validation）需搭配 Model底下類別檔的 [驗證]
            {

                _db.CertificateContents.Add(_cer);
                _db.SaveChanges();
                return RedirectToAction("CertificateList");
            }
            else
            {   // 搭配 ModelState.IsValid，如果驗證沒過，就出現錯誤訊息。
                ModelState.AddModelError("Value1", " 自訂錯誤訊息(1) ");   // 第一個輸入值是 key，第二個是錯誤訊息（字串）
                ModelState.AddModelError("Value2", " 自訂錯誤訊息(2) ");
                return View();   // 將錯誤訊息，返回並呈現在「新增」的檢視畫面上
            }
        }


        public ActionResult InserviceVM(int _ID)
        {


            //HCmultiViewModel resultVM = new HCmultiViewModel
            //{
            //    HrmuVM = _db.Hrs.ToList(),
            //    CermuVM = _db.CertificateContents.Where(c => c.ContentID ==_ID).ToList()
            //};
            HCmultiViewModel Inservice = _IP.GetProveByID(_ID);



            return View(Inservice);
        }

        public ActionResult LeaveVM(int _ID)
        {
            HCmultiViewModel LeaveVM = _IP.GetProveByID(_ID);

            //var resultVM = from c in _db.CertificateContents
            //               join h in _db.Hrs on c.MemberID equals h.MemberID
            //               where c.ContentID == _ID
            //               select new HCmultiViewModel { CermuVM = c, HrmuVM = h };
            return View(LeaveVM);
        }

        public ActionResult BabyCareVM(int _ID)
        {
            HCmultiViewModel BabyCareVM = _IP.GetProveByID(_ID);

            return View(BabyCareVM);
        }
        public ActionResult Inservice(int _ID)
        {
            //HrCerViewModel resultVM = new HrCerViewModel();
            //resultVM.CerVM = _db.CertificateContents.Where(c => c.ContentID == _ID);

            return View(/*resultVM.ToList()*/);
        }


        public ActionResult Index2()
        {
            return View();
        }
        public ActionResult Create()
        {
            var dt = _db.Dep1s;
            //===================== 集合,       Value,                  Text  =======  
            SelectList listItems = new SelectList(dt, "Dep1ID", "Dep1Name");   // 先寫 <option>子選項的 value，再寫 text
            ViewBag.DtListItem = listItems;
            //*******************


            return View();
        }

        [HttpPost]
        public ActionResult GetDEP(string id)
        {
            // 傳回一個「完整的」下拉式選單 <select>包含裡面各種<option>子選項
            string tagName = "Dep";
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (!string.IsNullOrWhiteSpace(id))
            {
                sb.AppendFormat("<select id=\"{0}\" name=\"{1}\" class=\"form - control\" > ", tagName, tagName);

                string i = id;
                var result = _db.Deps.Where(u => u.DepID == i);

                // 注意！如果後面不加上 .AsEnumerable()，就會報錯！
                // 錯誤訊息 - LINQ to Entities 無法辨識方法 'Int32 Parse(System.String)' 方法，而且這個方法無法轉譯成存放區運算式。
                //foreach (var r in result.AsEnumerable())
                //{   //                           *******************
                foreach (var r in result)   // 2018/8/13註解： 沒這一段也正常
                {   // 組合字串，做下拉式選單的「子選項」
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>",
                               r.DepID,
                               r.DepName);
                }
                sb.Append("</select>");
            }
            return Content(sb.ToString());    // 傳回一段字串。完整的「完整的」下拉式選單 <select>包含裡面各種<option>子選項
        }
        [HttpPost]
        public ActionResult GetUserTable(string id)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();   // 需要 System.Text命名空間
            if (!string.IsNullOrWhiteSpace(id))
            {
                string i = id;
                var result = _db.Dep2s.Where(u => u.Dep1ID == i);

                //// 注意！如果後面不加上 .AsEnumerable()，就會報錯！
                //// 錯誤訊息 - LINQ to Entities 無法辨識方法 'Int32 Parse(System.String)' 方法，而且這個方法無法轉譯成存放區運算式。
                //foreach (var r in result.AsEnumerable())
                //{   //                           *******************
                foreach (var r in result)   // 2018/8/13註解： 沒這一段也正常
                {   // 組合字串，此結果將都丟往檢視畫面上，做下拉式選單的「子選項」
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>",
                                       r.Dep2ID,
                                       r.Dep2Name);
                }
            }
            return Content(sb.ToString());    // 傳回一段字串。 只有下拉式選單裡面的 各種<option>子選項
            //return sb.ToString();   // 自己測試用的
        }

        [HttpPost]
        public ActionResult GetUserTable2(string id)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();   // 需要 System.Text命名空間
            if (!string.IsNullOrWhiteSpace(id))
            {
                string i = id;
                var result = _db.Dep3s.Where(u => u.Dep2ID == i);

                //// 注意！如果後面不加上 .AsEnumerable()，就會報錯！
                //// 錯誤訊息 - LINQ to Entities 無法辨識方法 'Int32 Parse(System.String)' 方法，而且這個方法無法轉譯成存放區運算式。
                //foreach (var r in result.AsEnumerable())
                //{   //                           *******************
                foreach (var r in result)   // 2018/8/13註解： 沒這一段也正常
                {   // 組合字串，此結果將都丟往檢視畫面上，做下拉式選單的「子選項」
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>",
                                       r.Dep3ID,
                                       r.Dep3Name);
                }
            }
            return Content(sb.ToString());    // 傳回一段字串。 只有下拉式選單裡面的 各種<option>子選項
            //return sb.ToString();   // 自己測試用的
        }

        [HttpPost]
        public ActionResult GetPostal(string id)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();   // 需要 System.Text命名空間
            if (!string.IsNullOrWhiteSpace(id))
            {
                string i = id;
                var result = _db.Addresss.Where(u => u.City == i);

                //// 注意！如果後面不加上 .AsEnumerable()，就會報錯！
                //// 錯誤訊息 - LINQ to Entities 無法辨識方法 'Int32 Parse(System.String)' 方法，而且這個方法無法轉譯成存放區運算式。
                //foreach (var r in result.AsEnumerable())
                //{   //                           *******************
                foreach (var r in result)   // 2018/8/13註解： 沒這一段也正常
                {   // 組合字串，此結果將都丟往檢視畫面上，做下拉式選單的「子選項」
                    sb.AppendFormat("<option value=\"{0}\">{1}</option>",
                                       r.Zone,
                                       r.Zone);
                }
            }
            return Content(sb.ToString());    // 傳回一段字串。 只有下拉式選單裡面的 各種<option>子選項
            //return sb.ToString();   // 自己測試用的
        }

        [HttpPost, ActionName("Create")]   // 把下面的動作名稱，改成 CreateConfirm 試試看？
        [ValidateAntiForgeryToken]   // 避免CSRF攻擊
        public ActionResult Create(Hr _hr)
        {
            using (SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["CGH"].ConnectionString))
            {

                Conn.Open();
                if (_hr != null)
                // ModelState.IsValid，通過表單驗證（Server-side validation）需搭配 Model底下類別檔的 [驗證]
                {//體檢日不為null
                    if (_hr.MEdate.ToString() != "0001/1/1 上午 12:00:00")
                    {

                        string sqlstr = "INSERT INTO [Hr] (	[MemberID],[Status],[Name],[IDcardNO],[Birthday],[Nationality],[StartDate],[Labor],[Hire],[Dep],[TitleID],[Boss],[Manager],[Remarks],[Marriage],[Blood],[Sex],[Height],[Weight],[Military],[MEdate],[Birthplace],[Email],[MobilePhone],[HomePhone],[Address1],[Postal1],[Address2],[Postal2],[Emergency],[Relationship],[Ephone],[Highestedu],[Graduation])";
                        sqlstr += " VALUES (@MemberID,@Status,@Name,@IDcardNO,@Birthday,@Nationality,@StartDate,@Labor,@Hire,@Dep,@TitleID,@Boss,@Manager,@Remarks,@Marriage,@Blood,@Sex,@Height,@Weight,@Military,@MEdate,@Birthplace,@Email,@MobilePhone,@HomePhone,@Address1,@Postal1,@Address2,@Postal2,@Emergency,@Relationship,@Ephone,@Highestedu,@Graduation)";
                        int affectedRows = Conn.Execute(sqlstr, new
                        {
                            MemberID = _hr.MemberID,
                            Status = _hr.Status,
                            Name = _hr.Name,
                            IDcardNO = _hr.IDcardNO,
                            Birthday = _hr.Birthday,
                            Nationality = _hr.Nationality,
                            StartDate = _hr.StartDate,
                            Labor = _hr.Labor,
                            Hire = _hr.Hire,
                            Dep = _hr.Dep,
                            TitleID = _hr.TitleID,
                            Boss = _hr.Boss,
                            Manager = _hr.Manager,
                            Remarks = _hr.Remarks,
                            Marriage = _hr.Marriage,
                            Blood = _hr.Blood,
                            Sex = _hr.Sex,
                            Height = _hr.Height,
                            Weight = _hr.Weight,
                            Military = _hr.Military,
                            MEdate = _hr.MEdate,
                            Birthplace = _hr.Birthplace,
                            Email = _hr.Email,
                            MobilePhone = _hr.MobilePhone,
                            HomePhone = _hr.HomePhone,
                            Address1 = _hr.Address1,
                            Postal1 = _hr.Postal1,
                            Address2 = _hr.Address2,
                            Postal2 = _hr.Postal2,
                            Emergency = _hr.Emergency,
                            Relationship = _hr.Relationship,
                            Ephone = _hr.Ephone,
                            Highestedu = _hr.Highestedu,
                            Graduation = _hr.Graduation


                        }
                        );
                    }
                    else
                    {
                        string sqlstr = "INSERT INTO [Hr] (	[MemberID],[Status],[Name],[IDcardNO],[Birthday],[Nationality],[StartDate],[Labor],[Hire],[Dep],[TitleID],[Boss],[Manager],[Remarks],[Marriage],[Blood],[Sex],[Height],[Weight],[Military],[Birthplace],[Email],[MobilePhone],[HomePhone],[Address1],[Postal1],[Address2],[Postal2],[Emergency],[Relationship],[Ephone],[Highestedu],[Graduation])";
                        sqlstr += " VALUES (@MemberID,@Status,@Name,@IDcardNO,@Birthday,@Nationality,@StartDate,@Labor,@Hire,@Dep,@TitleID,@Boss,@Manager,@Remarks,@Marriage,@Blood,@Sex,@Height,@Weight,@Military,@Birthplace,@Email,@MobilePhone,@HomePhone,@Address1,@Postal1,@Address2,@Postal2,@Emergency,@Relationship,@Ephone,@Highestedu,@Graduation)";
                        int affectedRows = Conn.Execute(sqlstr, new
                        {
                            MemberID = _hr.MemberID,
                            Status = _hr.Status,
                            Name = _hr.Name,
                            IDcardNO = _hr.IDcardNO,
                            Birthday = _hr.Birthday,
                            Nationality = _hr.Nationality,
                            StartDate = _hr.StartDate,
                            Labor = _hr.Labor,
                            Hire = _hr.Hire,
                            Dep = _hr.Dep,
                            TitleID = _hr.TitleID,
                            Boss = _hr.Boss,
                            Manager = _hr.Manager,
                            Remarks = _hr.Remarks,
                            Marriage = _hr.Marriage,
                            Blood = _hr.Blood,
                            Sex = _hr.Sex,
                            Height = _hr.Height,
                            Weight = _hr.Weight,
                            Military = _hr.Military,

                            Birthplace = _hr.Birthplace,
                            Email = _hr.Email,
                            MobilePhone = _hr.MobilePhone,
                            HomePhone = _hr.HomePhone,
                            Address1 = _hr.Address1,
                            Postal1 = _hr.Postal1,
                            Address2 = _hr.Address2,
                            Postal2 = _hr.Postal2,
                            Emergency = _hr.Emergency,
                            Relationship = _hr.Relationship,
                            Ephone = _hr.Ephone,
                            Highestedu = _hr.Highestedu,
                            Graduation = _hr.Graduation

                        });
                    }
                    return RedirectToAction("List");
                }

                else
                {   // 搭配 ModelState.IsValid，如果驗證沒過，就出現錯誤訊息。
                    ModelState.AddModelError("Value1", " 自訂錯誤訊息(1) ");   // 第一個輸入值是 key，第二個是錯誤訊息（字串）
                    ModelState.AddModelError("Value2", " 自訂錯誤訊息(2) ");
                    return View();   // 將錯誤訊息，返回並呈現在「新增」的檢視畫面上
                }
            }
        }

        public ActionResult List(Hr _hr)
        {
            //第一種寫法：  //*** 查詢結果是一個 IQueryable *******************************************
            IQueryable<Hr> ListAll = from _List in _db.Hrs
                                     select _List;
            // 從畫面上，輸入的第一個搜尋條件。  姓名。

            string uStatus = _hr.Status.ToString();
            
            if (!string.IsNullOrWhiteSpace(uStatus) )
            {

                ListAll = ListAll.Where(s => s.Status == _hr.Status );
            }

            if (!string.IsNullOrWhiteSpace(_hr.Dep))
            {
                ListAll = ListAll.Where(s => s.Dep.Contains(_hr.Dep));
                //                                                                      /
            }
            return View("List", ListAll);


        }

        public ActionResult Details(int? _ID)
        {
            // 如果沒有修改 /App_Start/RouteConfig.cs，網址輸入 http://xxxxxx/UserDB/Details/2 會報錯！ 
            if (_ID == null || _ID.HasValue == false)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            

            // 第四種寫法：透過 .Find() 函數
            Hr ut = _db.Hrs.Find(_ID);    // 翻譯成SQL指令的結果，同上（第一種方法）
            //如果有老闆
            if (!string.IsNullOrEmpty(ut.Boss))
            {

                var BossName = from _hr in _db.Hrs
                               where _hr.MemberID == ut.Boss
                               select _hr.Name;
                ViewBag.BossName = BossName.FirstOrDefault().ToString();
            }
            else
            {
                ViewBag.BossName = "";
            }
         DateTime Today = DateTime.Now;
            var age = CalculationDate.AGE(ut.Birthday, Today);
            ViewBag.age = age;
            //在職
            if (ut.Status != 2)
            {

                var service = CalculationDate.Seniority(ut.StartDate,Today);
                ViewBag.service = service;

            }
            else
            {
                var service = CalculationDate.Seniority(ut.StartDate, ut.EndDate);
                ViewBag.service = service;

            }



            if (ut == null)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {
                return View(ut);
            }
        }

        public ActionResult Edit(int? _ID)
        {
            if (_ID == null)
            {   // 沒有輸入文章編號（_ID），就會報錯 - Bad Request
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            // 使用上方 Details動作的程式，先列出這一筆的內容，給使用者確認
            Hr ut = _db.Hrs.Find(_ID);

            if (ut == null)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {
                return View(ut);
            }
        }
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Hr _userTable)
        {   // 參考資料 http://blog.kkbruce.net/2011/10/aspnet-mvc-model-binding6.html
            if (_userTable == null)
            {   // 沒有輸入內容，就會報錯 - Bad Request
                return Content(" *** 最前端失敗！！*** ");/*new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest)*/;
            }

            if (ModelState.IsValid)   // ModelState.IsValid，通過表單驗證（Server-side validation）需搭配 Model底下類別檔的 [驗證]
            {
                //_db.Entry(_userTable).State = System.Data.Entity.EntityState.Modified;  //確認被修改（狀態：Modified）
                //_db.SaveChanges();

                // 第二種寫法：========================================= (start)

                //使用上方 Details動作的程式，先列出這一筆的內容，給使用者確認
                
               Hr ut = _db.Hrs.Find(_userTable.ID); 

                if (ut == null)
                {   // 找不到這一筆記錄
                    return Content(" *** 二階段失敗！！*** ");
                }
                else
                {

                    
                    ut.Name = _userTable.Name;
                    ut.IDcardNO = _userTable.IDcardNO;
                    ut.Birthday = _userTable.Birthday;
                    ut.Nationality = _userTable.Nationality;
                    ut.StartDate = _userTable.StartDate;
                    ut.Labor = _userTable.Labor;
                    ut.Hire = _userTable.Hire;
                    ut.Dep = _userTable.Dep;
                    ut.TitleID = _userTable.TitleID;
                    ut.Boss = _userTable.Boss;
                    ut.Manager = _userTable.Manager;
                    ut.Remarks = _userTable.Remarks;
                    ut.Marriage = _userTable.Marriage;
                    ut.Blood = _userTable.Blood;
                    ut.Sex = _userTable.Sex;
                    ut.Height = _userTable.Height;
                    ut.Weight = _userTable.Weight;
                    ut.Military = _userTable.Military;
                    ut.MEdate = _userTable.MEdate;
                    ut.Birthplace = _userTable.Birthplace;
                    ut.Email = _userTable.Email;
                    ut.MobilePhone = _userTable.MobilePhone;
                    ut.HomePhone = _userTable.HomePhone;
                    ut.Address1 = _userTable.Address1;
                    ut.Postal1 = _userTable.Postal1;
                    ut.Address2 = _userTable.Address2;
                    ut.Postal2 = _userTable.Postal2;
                    ut.Emergency = _userTable.Emergency;
                    ut.Relationship = _userTable.Relationship;
                    ut.Ephone = _userTable.Ephone;
                    ut.Highestedu = _userTable.Highestedu;
                    ut.Graduation = _userTable.Graduation;
                    _db.SaveChanges();   // 回寫資料庫（進行修改）
                }



                //return Content(" 更新一筆記錄，成功！");    // 更新成功後，出現訊息（字串）。
                return RedirectToAction("List");
            }
            else
            {
                return View(_userTable);  // 若沒有修改成功，則列出原本畫面
                //return Content(" *** 更新失敗！！*** "); 
            }
        }


        ////== 修改（更新），回寫資料庫 #1 ============ 注意！這裡的輸入值是一個 UserTable
        //[HttpPost]
        //[ValidateAntiForgeryToken]   // 避免CSRF攻擊  https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/implementing-basic-crud-functionality-with-the-entity-framework-in-asp-net-mvc-application#overpost

        //// [Bind(Include=.......)] 也可以寫在 Model的類別檔裡面，就不用重複地寫在新增、刪除、修改每個動作之中。需搭配 System.Web.Mvc命名空間。
        //// 可以避免 overposting attacks （過多發佈）攻擊  http://www.cnblogs.com/Erik_Xu/p/5497501.html
        //// 參考資料 http://blog.kkbruce.net/2011/10/aspnet-mvc-model-binding6.html
        //public ActionResult Edit([Bind(Include = "UserId, UserName, UserSex, UserBirthDay, UserMobilePhone")]Hr _hr)
        //{   // 參考資料 http://blog.kkbruce.net/2011/10/aspnet-mvc-model-binding6.html
        //    if (_hr == null)
        //    {   // 沒有輸入內容，就會報錯 - Bad Request
        //        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        //    }

        //    if (ModelState.IsValid)   // ModelState.IsValid，通過表單驗證（Server-side validation）需搭配 Model底下類別檔的 [驗證]
        //    {    // 參考資料： （理論）http://www.entityframeworktutorial.net/basics/entity-states.aspx
        //         // （有CRUD完整範例） https://msdn.microsoft.com/en-us/library/jj592676(v=vs.113).aspx

        //        // 第一種寫法：
        //        _db.Entry(_hr).State = System.Data.Entity.EntityState.Modified;  //確認被修改（狀態：Modified）
        //        _db.SaveChanges();

        //        //// 第二種寫法：========================================= (start)
        //        #region
        //        //// 使用上方 Details動作的程式，先列出這一筆的內容，給使用者確認
        //        //UserTable ut = _db.UserTables.Find(_userTable.UserId);                

        //        //if (ut == null)
        //        //{   // 找不到這一筆記錄
        //        //    return HttpNotFound();
        //        //}
        //        //else   {
        //        //    ut.UserName = _userTable.UserName;  // 修改後的值
        //        //    ut.UserSex = _userTable.UserSex;
        //        //    ut.UserBirthDay = _userTable.UserBirthDay;
        //        //    ut.UserMobilePhone = _userTable.UserMobilePhone;

        //        //    _db.SaveChanges();   // 回寫資料庫（進行修改）
        //        //}
        //        //// 第二種寫法：========================================= (end)
        //        #endregion

        //        //return Content(" 更新一筆記錄，成功！");    // 更新成功後，出現訊息（字串）。
        //        return RedirectToAction("List");
        //    }
        //    else
        //    {
        //        return View(_hr);  // 若沒有修改成功，則列出原本畫面
        //        //return Content(" *** 更新失敗！！*** "); 
        //    }
        //public ActionResult OrderList()
        //{

        //    IQueryable<Order> ListAll = from _OrderTable in _db.Orders /*where _userTable.Status==
        //                                select _OrderTable;



        //    if (ListAll == null)
        //    {   // 找不到這一筆記錄
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        return View(ListAll.ToList());

        //    }

        //}
        //public ActionResult OrderDetails(int? _ID)
        //{

        //    if (_ID == null || _ID.HasValue == false)
        //    {
        //        return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
        //    }


        //    Order ut = _db.Orders.Find(_ID);
        //    if (ut == null)
        //    {   // 找不到這一筆記錄
        //        return HttpNotFound();
        //    }
        //    else
        //    {
        //        return View(ut);
        //    }
        //}
        //[Authorize]
        //public ActionResult OrderCreate()
        //{
        //    //取出今日年月
        //    var Today = DateTimeExtensions.ToFullTaiwanDate(DateTime.Now).ToString();
        //    //搜尋最後一筆訂單編號
        //    var OrderID1 = from _c in _db.Orders
        //                   orderby _c.OrderID descending
        //                   select _c.OrderID;
        //    //將訂單變號轉換為string
        //    string OrderID2 = OrderID1.FirstOrDefault().ToString();
        //    //使用關鍵字"今日年月"搜尋訂單編號
        //    var list = from _o in _db.Orders
        //               where OrderID2.Contains(Today)
        //               select OrderID2;
        //    //如果搜尋結果為0
        //    if (list.FirstOrDefault() == null)
        //    {
        //        ViewBag.ContentNO = Today + "001";

        //    }
        //    else
        //    {
        //        ViewBag.ContentNO = int.Parse(list.FirstOrDefault()) + 1;
        //    }
        //    return View();
        //    //Contains
        //}
        //[HttpPost, ActionName("OrderCreate")]   // 把下面的動作名稱，改成 CreateConfirm 試試看？
        //[ValidateAntiForgeryToken]   // 避免CSRF攻擊
        //public ActionResult OrderCreate(Order _order)
        //{
        //    using (SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["CGH"].ConnectionString))
        //    {

        //        Conn.Open();
        //        var UserIDx = HttpContext.User.Identity.Name;
        //        //日期有內容
        //        if ((_order.StartDate.ToString() != "0001/1/1 上午 12:00:00")&& (_order.EndDate.ToString() != "0001/1/1 上午 12:00:00"))
        //        {

        //            string sqlstr = "INSERT INTO [Order] ([OrderID],[BuyerUnit],[BuyerName],[Category],[Quantity],[Money],[StartDate],[EndDate],[Remark],[UserID]) ";
        //            sqlstr += " VALUES (@OrderID,@BuyerUnit,@BuyerName,@Category,@Quantity,@Money,@StartDate,@EndDate,@Remark,@UserID)";

        //            _order.UserID = UserIDx;
        //            int affectedRows = Conn.Execute(sqlstr, new
        //            { 

        //                 OrderID = _order.OrderID,
        //                BuyerUnit = _order.BuyerUnit,
        //                BuyerName = _order.BuyerName,
        //                Category = _order.Category,
        //                Quantity = _order.Quantity,
        //                Money = _order.Money,
        //                StartDate = _order.StartDate,
        //                EndDate=_order.EndDate,
        //                Remark = _order.Remark,
        //                UserID = _order.UserID
        //        });

        //        }
        //        else
        //        {//日期無內容
        //            string sqlstr = "INSERT INTO [Order] ([OrderID],[BuyerUnit],[BuyerName],[Category],[Quantity],[Money],[Remark],[UserID]) ";
        //            sqlstr += " VALUES (@OrderID,@BuyerUnit,@BuyerName,@Category,@Quantity,@Money,@Remark,@UserID)";

        //            _order.UserID = UserIDx;
        //            int affectedRows = Conn.Execute(sqlstr, new
        //            {   
        //                OrderID = _order.OrderID,
        //                BuyerUnit = _order.BuyerUnit,
        //                BuyerName = _order.BuyerName,
        //                Category = _order.Category,
        //                Quantity = _order.Quantity,
        //                Money = _order.Money,

        //                Remark = _order.Remark,
        //                UserID = _order.UserID
        //            });



        //        }
        //        return RedirectToAction("List");

        //    }


        //}
    }
}
    

    
