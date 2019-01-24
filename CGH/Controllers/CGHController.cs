using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CGH.Models;
namespace CGH.Controllers
{
    public class CGHController : Controller
    {
        private CGHContext _db = new CGHContext();

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
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost, ActionName("Create")]   // 把下面的動作名稱，改成 CreateConfirm 試試看？
        [ValidateAntiForgeryToken]   // 避免CSRF攻擊
        public ActionResult Create(Hr _hr)
        {
            if ((_hr != null) && (ModelState.IsValid))   // ModelState.IsValid，通過表單驗證（Server-side validation）需搭配 Model底下類別檔的 [驗證]
            {   // 第一種方法
                _db.Hrs.Add(_hr);
                _db.SaveChanges();

                //// 第二種方法（作法類似後續的 Edit動作）
                //// 資料來源  https://msdn.microsoft.com/en-us/library/jj592676(v=vs.113).aspx
                //_db.Entry(_userTable).State = System.Data.Entity.EntityState.Added;  //確認新增一筆（狀態：Added）
                //_db.SaveChanges();

                //return Content(" 新增一筆記錄，成功！");    // 新增成功後，出現訊息（字串）。
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
            IQueryable<Hr> ListAll = from _userTable in _db.Hrs
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
        public ActionResult Details(int? _ID = 3)
        {
            // 如果沒有修改 /App_Start/RouteConfig.cs，網址輸入 http://xxxxxx/UserDB/Details/2 會報錯！ 
            if (_ID == null || _ID.HasValue == false)
            {   //// 沒有輸入文章編號（_ID），就會報錯 - Bad Request
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
    }
}