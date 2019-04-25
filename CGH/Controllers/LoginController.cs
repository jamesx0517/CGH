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
    public class LoginController : Controller
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

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [Authorize]
        public ActionResult Index2()
        {

            
            return View();    // 登入成功（會員）才可以看見
        }
        [Authorize(Roles = "999")]
        public ActionResult Index3()
        {
            return View();    // 登入成功（會員）才可以看見
        }


        [HttpPost]
        [ValidateAntiForgeryToken]   // 避免XSS、CSRF攻擊
        public ActionResult Login(UsersTable _User)
        {
            // 請參考 UserDBController（第三天課程）的 Details動作，共有四種寫法。

            string hash = GetSHA1.GetSHA1Hash(_User.Password);

            _User.Password = hash;
            var ListOne = from m in _db.UsersTables
                          where m.UserID == _User.UserID && m.Password == _User.Password
                          select m;

            UsersTable _result = ListOne.FirstOrDefault();  // 執行上面的查詢句，得到結果。

            if (_result == null)
            {   // 找不到這一筆記錄（帳號與密碼有錯，沒有這個會員）
                //return HttpNotFound();
                ViewData["ErrorMessage"] = "帳號與密碼有錯";
                return View();
            }
            else
            {   //*************************************************************(start)
                // https://dotblogs.com.tw/mickey/2017/01/01/154812 
                // https://dotblogs.com.tw/mis2000lab/2014/08/01/authentication-mode-forms_web-config
                // https://blog.miniasp.com/post/2008/06/11/How-to-define-Roles-but-not-implementing-Role-Provider-in-ASPNET.aspx
                // http://kevintsengtw.blogspot.com/2013/11/aspnet-mvc.html
                DateTime DTnow = DateTime.Now;

                // 以下需要搭配 System.Web.Security 命名空間。                
                var authTicket = new FormsAuthenticationTicket(   // 登入成功，取得門票 (票證)。請自行填寫以下資訊。
                    version: 1,   //版本號（Ver.）
                    name: _result.UserID,  // ***自行放入資料（如：使用者帳號、真實名稱）
                    
                    issueDate: DTnow,  // 登入成功後，核發此票證的本機日期和時間（資料格式 DateTime）
                    expiration: DTnow.AddDays(1),  //  "一天"內都有效（票證到期的本機日期和時間。）
                    isPersistent: true,  // 記住我？ true or false（畫面上通常會用 CheckBox表示）

                    userData: _result.Rank.ToString(),   // ***自行放入資料（如：會員權限、等級、群組） 
                                                             // 與票證一起存放的使用者特定資料。
                                                             // 需搭配 Global.asax設定檔 - Application_AuthenticateRequest事件。

                    cookiePath: FormsAuthentication.FormsCookiePath
                    );


                //                                                                                                                        // *** 把上面的 ticket資訊 "加密"  ****** 
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket))
                {   // 重點！！避免 Cookie遭受攻擊、盜用或不當存取。請查詢關鍵字「」。
                    HttpOnly = true  // 必須上網透過http才可以存取Cookie。不允許用戶端（寫前端程式）存取

                    //HttpOnly = true,  // 必須上網透過http才可以存取Cookie。不允許用戶端（寫前端程式）存取
                    //Secure = true;      // 需要搭配https（SSL）才行。
                };


                if (authTicket.IsPersistent)
                {
                    authCookie.Expires = authTicket.Expiration;    // Cookie過期日（票證到期的本機日期和時間。）
                }
                Response.Cookies.Add(authCookie);   // 完成 Cookie，寫入使用者的瀏覽器與設備中
                                                    //*************************************************************(end)

                return RedirectToAction("Index2", "Login");

                // 完成這個範例以後，您可以參考這篇文章 - OWIN Forms authentication（作法很類似）
                // https://blogs.msdn.microsoft.com/webdev/2013/07/03/understanding-owin-forms-authentication-in-mvc-5/
            }

        }


        //========================================
        //==  會員登出（Log Out）
        //========================================
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();  // 登出
            Session.Abandon();   // 不光是清除 Session裡面的資料，把 Session也取消了！

            //// 參考資料 http://kevintsengtw.blogspot.com/2013/11/aspnet-mvc.html
            //// 建立一個同名的 Cookie 來覆蓋原本的 Cookie
            //HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "")   {
            //    Expires = DateTime.Now.AddYears(-1)   // 設定過期日（已過期一年）
            //};
            //Response.Cookies.Add(cookie1);

            //// 建立 ASP.NET 的 Session Cookie 同樣是為了覆蓋
            //HttpCookie cookie2 = new HttpCookie("ASP.NET_SessionId", "")   {
            //    Expires = DateTime.Now.AddYears(-1)   // 設定過期日（已過期一年）
            //};
            //Response.Cookies.Add(cookie2);

            return RedirectToAction("Login");
            // 回到 登入畫面（Login動作）
        }

        public ActionResult Registered()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registered([Bind(Include = "UserID,UserName, Password")]UsersTable _usersTable)
        {
            using (SqlConnection Conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["CGH"].ConnectionString))
            {

                Conn.Open();
                if ((_usersTable != null) && (ModelState.IsValid))
                // ModelState.IsValid，通過表單驗證（Server-side validation）需搭配 Model底下類別檔的 [驗證]
                {
                    string hash = GetSHA1.GetSHA1Hash(_usersTable.Password);

                    _usersTable.Password = hash;
                    string sqlstr = "INSERT INTO [UsersTable] (	[UserID],[UserName],[Password])";
                    sqlstr += " VALUES (@UserID,@UserName,@Password)";
                    int affectedRows = Conn.Execute(sqlstr, new
                    {
                        
                        UserID=_usersTable.UserID,
                        UserName=_usersTable.UserName,
                        Password=_usersTable.Password


                    });


                    return RedirectToAction("Index");
                }

                else
                {   // 搭配 ModelState.IsValid，如果驗證沒過，就出現錯誤訊息。
                    ModelState.AddModelError("Value1", " 自訂錯誤訊息(1) ");   // 第一個輸入值是 key，第二個是錯誤訊息（字串）
                    ModelState.AddModelError("Value2", " 自訂錯誤訊息(2) ");
                    return View();   // 將錯誤訊息，返回並呈現在「新增」的檢視畫面上
                }
            }
            
        }
    }
}