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

        [HttpPost, ActionName("Create")]   // 把下面的動作名稱，改成 CreateConfirm 試試看？
        [ValidateAntiForgeryToken]   // 避免CSRF攻擊
        public ActionResult Create(Hr _hr)
        {
            if ((_hr != null) && (ModelState.IsValid))
            // ModelState.IsValid，通過表單驗證（Server-side validation）需搭配 Model底下類別檔的 [驗證]
            {

                _db.Hrs.Add(_hr);
                _db.SaveChanges();
                return RedirectToAction("List");
            }
            else
            {   // 搭配 ModelState.IsValid，如果驗證沒過，就出現錯誤訊息。
                ModelState.AddModelError("Value1", " 自訂錯誤訊息(1) ");   // 第一個輸入值是 key，第二個是錯誤訊息（字串）
                ModelState.AddModelError("Value2", " 自訂錯誤訊息(2) ");
                return View();   // 將錯誤訊息，返回並呈現在「新增」的檢視畫面上
            }
        }

        public ActionResult List()
        {
            //第一種寫法：  //*** 查詢結果是一個 IQueryable *******************************************
            IQueryable<Hr> ListAll = from _userTable in _db.Hrs /*where _userTable.Status==*/
                                     select _userTable;

            // 或是寫成
            //var ListAll = from m in _db.UserTables
            //                   select m;
            //翻譯後的SQL指令：SELECT [Extent1].[UserId] AS[UserId], 
            //    [Extent1].[UserName] AS[UserName], 
            //    [Extent1].[UserSex] AS[UserSex], 
            //    [Extent1].[UserBirthDay] AS[UserBirthDay], 
            //    [Extent1].[UserMobilePhone] AS[UserMobilePhone]
            //FROM[dbo].[UserTable] AS[Extent1]

            //*** 使用 IQueryable的好處是什麼？？************************************
            // The method uses "LINQ to Entities" to specify the column to sort by.The code creates an IQueryable variable 
            // before the switch statement, modifies it in the switch statement, and calls the ".ToList()" method after the 
            // switch statement.When you create and modify IQueryable variables, no query is sent to the database. 
            //
            // The query is not executed until you convert the IQueryable object into a collection by calling a method such as ".ToList()".
            // （直到程式的最後，你把查詢結果 IQueryable，呼叫.ToList()時，這段LINQ才會真正被執行！）
            // Therefore, this code results in a single query that is not executed until the return View statement.

            // (1) http://blog.darkthread.net/post-2012-10-23-iqueryable-experiment.aspx
            //......發現 IQueryable<T> 是在 Server 端作過濾, 再將結果傳回 Client 端, 故若為資料庫存取, 應採用 IQueryable<T>
            // (2) http://jasper-it.blogspot.tw/2015/01/c-ienumerable-ienumerator.html
            //......在資料庫相關的環境下, 用 IQueryable<T> 的效能會比 IEnumerable< T > 來得好.
            //*****************************************************************************

            if (ListAll == null)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {
                return View(ListAll.ToList());
                // 直到程式的最後，把查詢結果 IQueryable呼叫.ToList()時，上面那一段LINQ才會真正被執行！
            }

            ////第二種寫法：
            //if (_db.UserTables == null)
            //{   // 找不到任何記錄
            //    return HttpNotFound();
            //// return Content("抱歉！找不到！");
            //}
            //else
            //{
            //    return View(_db.UserTables.ToList());   //直接把 UserTables的全部內容 列出來
            //    // 翻譯成SQL指令的成果，跟第一種方法相同。
            //}
        }
        public ActionResult Details(int? _ID)
        {
            // 如果沒有修改 /App_Start/RouteConfig.cs，網址輸入 http://xxxxxx/UserDB/Details/2 會報錯！ 
            if (_ID == null || _ID.HasValue == false)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            //// 第一種寫法：========================================
            ////var ListOne = from _userTable in _db.UserTables
            ////              where _userTable.UserId == _ID
            ////              select _userTable;
            ////也可以寫成下面這樣：
            //var ListOne = from m in _db.UserTables
            //              where m.UserId == _ID
            //              select m;
            //// 翻譯成SQL指令的結果：SELECT TOP (1) [Extent1].[Id] AS[Id],     [Extent1].[ModelHash] AS[ModelHash]
            ////                                        FROM[dbo].[EdmMetadata] AS[Extent1]
            ////                                        ORDER BY[Extent1].[Id] DESC

            //if (ListOne == null)
            //{    // 找不到這一筆記錄
            //    return HttpNotFound();
            //}
            //else   {
            //    return View(ListOne.FirstOrDefault());
            //}

            //// 第二種寫法： 透過 .Where() 函數=============================
            //var ListOne2 = _db.UserTables.Where(x => x.UserId == _ID);
            //if (ListOne2 == null)
            //{    // 找不到這一筆記錄
            //    return HttpNotFound();
            //}
            //else  {
            //    return View(ListOne2.FirstOrDefault());
            //    // 翻譯成SQL指令的結果，同上（第一種方法）。
            //}

            //// 第三種寫法： 透過 .FirstOrDefault() 函數=========================
            //var ListOne3 = _db.UserTables.FirstOrDefault(b => b.UserId == _ID);
            //// 翻譯成SQL指令的結果，同上（第一種方法）。
            //if (ListOne3 == null)
            //{    // 找不到這一筆記錄
            //    return HttpNotFound();
            //}
            //else   {
            //    return View(ListOne3);
            //}

            // 第四種寫法：透過 .Find() 函數
            Hr ut = _db.Hrs.Find(_ID);    // 翻譯成SQL指令的結果，同上（第一種方法）
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
        public ActionResult OrderList()
        {

            IQueryable<Order> ListAll = from _OrderTable in _db.Orders /*where _userTable.Status==*/
                                        select _OrderTable;



            if (ListAll == null)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {
                return View(ListAll.ToList());

            }

        }
        public ActionResult OrderDetails(int? _ID)
        {

            if (_ID == null || _ID.HasValue == false)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }


            Order ut = _db.Orders.Find(_ID);
            if (ut == null)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {
                return View(ut);
            }
        }
        [Authorize]
        public ActionResult OrderCreate()
        {
            
            //   var Today = DateTimeExtensions.ToFullTaiwanDate(DateTime.Now).ToString();
            //var OrderID1 = from _c in _db.Orders
            //           orderby _c.OrderID descending
            //           select _c.OrderID;
            //var OrderID2 = OrderID1.ToString();
            //var list = from _o in _db.Orders
            //           where _o.OrderID.Contains('1')
            //           select _o.OrderID;

            //ViewBag.ContentNO = list.FirstOrDefault() ;
            return View();
            //Contains
        }
        [HttpPost, ActionName("OrderCreate")]   // 把下面的動作名稱，改成 CreateConfirm 試試看？
        [ValidateAntiForgeryToken]   // 避免CSRF攻擊
        public ActionResult OrderCreate(Order _order)
        {
            using (SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["CGH"].ConnectionString))
            {

                Conn.Open();
                var UserIDx = HttpContext.User.Identity.Name;
                //日期有內容
                if ((_order.StartDate.ToString() != "0001/1/1 上午 12:00:00")&& (_order.EndDate.ToString() != "0001/1/1 上午 12:00:00"))
                {

                    string sqlstr = "INSERT INTO [Order] ([OrderID],[BuyerUnit],[BuyerName],[Category],[Quantity],[Money],[StartDate],[EndDate],[Remark],[UserID]) ";
                    sqlstr += " VALUES (@OrderID,@BuyerUnit,@BuyerName,@Category,@Quantity,@Money,@StartDate,@EndDate,@Remark,@UserID)";

                    _order.UserID = UserIDx;
                    int affectedRows = Conn.Execute(sqlstr, new
                    { 
                    
                         OrderID = _order.OrderID,
                        BuyerUnit = _order.BuyerUnit,
                        BuyerName = _order.BuyerName,
                        Category = _order.Category,
                        Quantity = _order.Quantity,
                        Money = _order.Money,
                        StartDate = _order.StartDate,
                        EndDate=_order.EndDate,
                        Remark = _order.Remark,
                        UserID = _order.UserID
                });

                }
                else
                {
                    string sqlstr = "INSERT INTO [Order] ([OrderID],[BuyerUnit],[BuyerName],[Category],[Quantity],[Money],[Remark],[UserID]) ";
                    sqlstr += " VALUES (@OrderID,@BuyerUnit,@BuyerName,@Category,@Quantity,@Money,@Remark,@UserID)";

                    _order.UserID = UserIDx;
                    int affectedRows = Conn.Execute(sqlstr, new
                    {   
                        OrderID = _order.OrderID,
                        BuyerUnit = _order.BuyerUnit,
                        BuyerName = _order.BuyerName,
                        Category = _order.Category,
                        Quantity = _order.Quantity,
                        Money = _order.Money,

                        Remark = _order.Remark,
                        UserID = _order.UserID
                    });



                }
                return RedirectToAction("List");

            }


        }
    }
}
    

    
