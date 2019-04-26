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
    [Authorize]
    public class OrderController : Controller
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
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult OrderList()
        {

            IQueryable<Order> ListAll = from _OrderTable in _db.Orders orderby _OrderTable.OrderID,_OrderTable.BillingDate
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

        public ActionResult OrderInventory(Order _date)
        {

            IQueryable<Order> ListAll = from _OrderTable in _db.Orders
                                       where _OrderTable.Cancel==false
                                        select _OrderTable;
            string satrt = _date.StartDate.ToString();
            string end = _date.EndDate.ToString();


            if (!string.IsNullOrWhiteSpace(satrt)&& !string.IsNullOrWhiteSpace(end))
            {

                ListAll = ListAll.Where(x=>x.BillingDate>=_date.StartDate && x.BillingDate<= _date.EndDate);
            }


            
            return View("OrderInventory", ListAll);


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
            ////取出今日年月
            //var Today = DateTimeExtensions.ToFullTaiwanDate(DateTime.Now).ToString();
            ////搜尋最後一筆訂單編號
            //var OrderID1 = from _c in _db.Orders
            //               orderby _c.OrderID descending
            //               select _c.OrderID;
            ////將訂單變號轉換為string
            //string OrderID2 = OrderID1.FirstOrDefault().ToString();
            ////使用關鍵字"今日年月"搜尋訂單編號
            //var list = from _o in _db.Orders
            //           where OrderID2.Contains(Today)
            //           select OrderID2;
            ////如果搜尋結果為0
            //if (list.FirstOrDefault() == null)
            //{
            //    ViewBag.ContentNO = Today + "001";

            //}
            //else
            //{
            //    ViewBag.ContentNO = int.Parse(list.FirstOrDefault()) + 1;
            //}
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
                //登入帳號帶入
                var UserIDx = HttpContext.User.Identity.Name;
                _order.UserID = UserIDx;

                //取出今日年月
                var Today = DateTimeExtensions.ToFullTaiwanDate(_order.BillingDate).ToString();
                //搜尋最後一筆訂單編號
                var OrderID1 = from _c in _db.Orders
                               where _c.OrderID.ToString().Contains(Today)
                               orderby _c.OrderID descending
                               select _c.OrderID;
                //將訂單變號轉換為string
                string OrderID2 = OrderID1.FirstOrDefault().ToString();
                //使用關鍵字"今日年月"搜尋訂單編號
                var list = from _o in _db.Orders
                           where OrderID2.Contains(Today)
                           select OrderID2;
                //如果搜尋結果為0
                if (list.FirstOrDefault() == null)
                {
                    _order.OrderID = int.Parse (Today + "001");

                }
                else
                {
                    _order.OrderID = int.Parse(list.FirstOrDefault()) + 1;
                }
                
                //Contains
                //日期有內容
                if ((_order.StartDate.ToString() != "0001/1/1 上午 12:00:00") && (_order.EndDate.ToString() != "0001/1/1 上午 12:00:00"))
                {

                    string sqlstr = "INSERT INTO [Order] ([OrderID],[BuyerUnit],[BuyerName],[Category],[Quantity],[Money],[StartDate],[EndDate],[Remark],[UserID],[BillingDate]) ";
                    sqlstr += " VALUES (@OrderID,@BuyerUnit,@BuyerName,@Category,@Quantity,@Money,@StartDate,@EndDate,@Remark,@UserID,@BillingDate)";

                    
                    int affectedRows = Conn.Execute(sqlstr, new
                    {

                        OrderID = _order.OrderID,
                        BuyerUnit = _order.BuyerUnit,
                        BuyerName = _order.BuyerName,
                        Category = _order.Category,
                        Quantity = _order.Quantity,
                        Money = _order.Money,
                        StartDate = _order.StartDate,
                        EndDate = _order.EndDate,
                        Remark = _order.Remark,
                        UserID = _order.UserID,
                        BillingDate=_order.BillingDate
                    });

                }
                else
                {//日期無內容
                    string sqlstr = "INSERT INTO [Order] ([OrderID],[BuyerUnit],[BuyerName],[Category],[Quantity],[Money],[Remark],[UserID],[BillingDate]) ";
                    sqlstr += " VALUES (@OrderID,@BuyerUnit,@BuyerName,@Category,@Quantity,@Money,@Remark,@UserID,@BillingDate)";

                    
                    int affectedRows = Conn.Execute(sqlstr, new
                    {
                        OrderID = _order.OrderID,
                        BuyerUnit = _order.BuyerUnit,
                        BuyerName = _order.BuyerName,
                        Category = _order.Category,
                        Quantity = _order.Quantity,
                        Money = _order.Money,

                        Remark = _order.Remark,
                        UserID = _order.UserID,
                        BillingDate = _order.BillingDate

                    });



                }
                return RedirectToAction("OrderList");

            }


        }

        public ActionResult OrderEdit(int? _ID)
        {var UserIDx = HttpContext.User.Identity.Name;
            if (_ID == null || _ID.HasValue == false)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            Order OrderEdit = _db.Orders.Find(_ID);    // 翻譯成SQL指令的結果，同上（第一種方法）
            if (OrderEdit == null || OrderEdit.UserID != UserIDx)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {
                return View(OrderEdit);
            }

        }
        [HttpPost, ActionName("OrderEdit")]
        [ValidateAntiForgeryToken]   // 避免CSRF攻擊
        public ActionResult OrderEdit(Order _order )
        {
            if (_order == null)
            {   // 沒有輸入內容，就會報錯 - Bad Request
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)   // ModelState.IsValid，通過表單驗證（Server-side validation）需搭配 Model底下類別檔的 [驗證]
            {
                //_db.Entry(_unit).State = System.Data.Entity.EntityState.Modified;  //確認被修改（狀態：Modified）
                //_db.SaveChanges();
                Order OrderEdit = _db.Orders.Find(_order.ID);

                if (OrderEdit == null)
                {   // 找不到這一筆記錄
                    return HttpNotFound();
                }
                else
                {
                    OrderEdit.BillingDate = _order.BillingDate;
                    OrderEdit.BuyerName = _order.BuyerName;
                    OrderEdit.BuyerUnit = _order.BuyerUnit;
                    OrderEdit.Category = _order.Category;
                    OrderEdit.Quantity = _order.Quantity;
                    OrderEdit.Money = _order.Money;
                    OrderEdit.StartDate = _order.StartDate;
                    OrderEdit.EndDate = _order.EndDate;
                    OrderEdit.Remark = _order.Remark;
                    OrderEdit.Cancel = _order.Cancel;
                    _db.SaveChanges();   // 回寫資料庫（進行修改）
                }



                return RedirectToAction("OrderList");
            }
            else
            {
                return View(_order);  // 若沒有修改成功，則列出原本畫面

            }

        }
        public ActionResult UnitEdit(int? _ID)
        {
            if (_ID == null || _ID.HasValue == false)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            BuyerUnit bu = _db.BuyerUnits.Find(_ID);    // 翻譯成SQL指令的結果，同上（第一種方法）
            if (bu == null)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {
                return View(bu);
            }
            
        }
        [HttpPost, ActionName("UnitEdit")]   
        [ValidateAntiForgeryToken]   // 避免CSRF攻擊
        public ActionResult UnitEdit(BuyerUnit _unit)
        {
            if (_unit == null)
            {   // 沒有輸入內容，就會報錯 - Bad Request
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)   // ModelState.IsValid，通過表單驗證（Server-side validation）需搭配 Model底下類別檔的 [驗證]
            {
                //_db.Entry(_unit).State = System.Data.Entity.EntityState.Modified;  //確認被修改（狀態：Modified）
                //_db.SaveChanges();
                BuyerUnit bu = _db.BuyerUnits.Find(_unit.BuyerUniID);

                if (bu == null)
                {   // 找不到這一筆記錄
                    return HttpNotFound();
                }
                else
                {
                    bu.BuyerUnitName = _unit.BuyerUnitName;
                    bu.Enable = _unit.Enable;

                    _db.SaveChanges();   // 回寫資料庫（進行修改）
                }



                return RedirectToAction("UnitList");
            }
            else
            {
                return View(_unit);  // 若沒有修改成功，則列出原本畫面

            }

        }
        public ActionResult UnitList()
        {
            IQueryable<BuyerUnit> ListAll = from _UnitList in _db.BuyerUnits
                                            select _UnitList;

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
        public ActionResult UnitCreate()
        {
            return View();
        }
        [HttpPost, ActionName("UnitCreate")]   // 把下面的動作名稱，改成 CreateConfirm 試試看？
        [ValidateAntiForgeryToken]   // 避免CSRF攻擊
        public ActionResult UnitCreate(BuyerUnit _unit)
        {
            if ((_unit != null) && (ModelState.IsValid))
            // ModelState.IsValid，通過表單驗證（Server-side validation）需搭配 Model底下類別檔的 [驗證]
            {

                _db.BuyerUnits.Add(_unit);
                _db.SaveChanges();
                return RedirectToAction("UnitList");
            }
            else
            {   // 搭配 ModelState.IsValid，如果驗證沒過，就出現錯誤訊息。
                ModelState.AddModelError("Value1", " 自訂錯誤訊息(1) ");   // 第一個輸入值是 key，第二個是錯誤訊息（字串）
                ModelState.AddModelError("Value2", " 自訂錯誤訊息(2) ");
                return View();   // 將錯誤訊息，返回並呈現在「新增」的檢視畫面上
            }
          
        }

        public ActionResult CategoryList ()
        {
            IQueryable<BuyCategory> ListAll = from _CategoryList in _db.BuyCategorys
                                            select _CategoryList;

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
        public ActionResult CategoryEdit(int? _ID)
        {
            if (_ID == null || _ID.HasValue == false)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            BuyCategory bc = _db.BuyCategorys.Find(_ID);    // 翻譯成SQL指令的結果，同上（第一種方法）
            if (bc == null)
            {   // 找不到這一筆記錄
                return HttpNotFound();
            }
            else
            {
                return View(bc);
            }

        }
        [HttpPost, ActionName("CategoryEdit")]
        [ValidateAntiForgeryToken]   // 避免CSRF攻擊
        public ActionResult CategoryEdit(BuyCategory _Category)
        {
            if (_Category == null)
            {   // 沒有輸入內容，就會報錯 - Bad Request
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            if (ModelState.IsValid)   // ModelState.IsValid，通過表單驗證（Server-side validation）需搭配 Model底下類別檔的 [驗證]
            {
                //_db.Entry(_unit).State = System.Data.Entity.EntityState.Modified;  //確認被修改（狀態：Modified）
                //_db.SaveChanges();
                BuyCategory bc = _db.BuyCategorys.Find(_Category.BuyCategoryID);

                if (bc == null)
                {   // 找不到這一筆記錄
                    return HttpNotFound();
                }
                else
                {
                    bc.BuyCategoryName = _Category.BuyCategoryName;
                    bc.Enable = _Category.Enable;


                    _db.SaveChanges();   // 回寫資料庫（進行修改）
                }



                return RedirectToAction("CategoryList");
            }
            else
            {
                return View(_Category);  // 若沒有修改成功，則列出原本畫面

            }

        }


        public ActionResult CategoryCreate()
        {
            return View();
        }
        [HttpPost, ActionName("CategoryCreate")]   // 把下面的動作名稱，改成 CreateConfirm 試試看？
        [ValidateAntiForgeryToken]   // 避免CSRF攻擊
        public ActionResult CategoryCreate(BuyCategory _Category)
        {
            if ((_Category != null) && (ModelState.IsValid))
            // ModelState.IsValid，通過表單驗證（Server-side validation）需搭配 Model底下類別檔的 [驗證]
            {

                _db.BuyCategorys.Add(_Category);
                _db.SaveChanges();
                return RedirectToAction("CategoryList");
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