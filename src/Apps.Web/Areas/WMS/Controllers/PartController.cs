﻿using System.Collections.Generic;
using System.Linq;
using Apps.Web.Core;
using Apps.IBLL.WMS;
using Apps.Locale;
using System.Web.Mvc;
using Apps.Common;
using Apps.IBLL;
using Apps.Models.WMS;
using Unity.Attributes;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Data;

namespace Apps.Web.Areas.WMS.Controllers
{
    public class PartController : BaseController
    {
        [Dependency]
        public IWMS_PartBLL m_BLL { get; set; }
        ValidationErrors errors = new ValidationErrors();

        [SupportFilter]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        [SupportFilter(ActionName = "Index1")]
        public JsonResult GetList(GridPager pager, string queryStr)
        {
            List<WMS_PartModel> list = m_BLL.GetList(ref pager, queryStr);
            GridRows<WMS_PartModel> grs = new GridRows<WMS_PartModel>();
            grs.rows = list;
            grs.total = pager.totalRows;
            return Json(grs);
        }

        [HttpPost]
        [SupportFilter(ActionName = "Index")]
        public JsonResult GetListByCode(GridPager pager, string queryStr/*string partCode, string partName*/)
        {
            List<WMS_PartModel> list = m_BLL.GetList(ref pager, queryStr);
            //List<WMS_PartModel> list = m_BLL.GetListByWhere(ref pager, "PartCode.Contains(\"" + partCode + "\") && PartName.Contains(\"" + partName + "\")");
            GridRows<WMS_PartModel> grs = new GridRows<WMS_PartModel>();
            grs.rows = list;
            grs.total = pager.totalRows;
            return Json(grs);
        }

        #region 创建
        [SupportFilter]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Create(WMS_PartModel model)
        {
            model.Id = 0;
            model.CreateTime = ResultHelper.NowTime;
            model.CreatePerson = GetUserTrueName();
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Create(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserTrueName(), "Id" + model.Id + ",PartCode" + model.PartCode, "成功", "创建", "WMS_Part");
                    return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed));
                }
                else
                {
                    //string ErrorCol = errors.Error;
                    string ErrorCol = " ：输入错误数据";
                    LogHandler.WriteServiceLog(GetUserTrueName(), "Id" + model.Id + ",PartCode" + model.PartCode + "," + ErrorCol, "失败", "创建", "WMS_Part");
                    return Json(JsonHandler.CreateMessage(0, Resource.InsertFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.InsertFail));
            }
        }
        #endregion

        #region 修改
        [SupportFilter]
        public ActionResult Edit(long id)
        {
            WMS_PartModel entity = m_BLL.GetById(id);
            ViewBag.EditStatus = true;
            return View(entity);
        }

        [HttpPost]
        [SupportFilter]
        public JsonResult Edit(WMS_PartModel model)
        {
            model.ModifyTime = ResultHelper.NowTime;
            model.ModifyPerson = GetUserTrueName();
            if (model != null && ModelState.IsValid)
            {

                if (m_BLL.Edit(ref errors, model))
                {
                    LogHandler.WriteServiceLog(GetUserTrueName(), "Id" + model.Id + ",PartCode" + model.PartCode, "成功", "修改", "WMS_Part");
                    return Json(JsonHandler.CreateMessage(1, Resource.EditSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserTrueName(), "Id" + model.Id + ",PartCode" + model.PartCode + "," + ErrorCol, "失败", "修改", "WMS_Part");
                    return Json(JsonHandler.CreateMessage(0, Resource.EditFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.EditFail));
            }
        }
        #endregion

        #region 详细
        [SupportFilter]
        public ActionResult Details(long id)
        {
            WMS_PartModel entity = m_BLL.GetById(id);
            return View(entity);
        }

        #endregion

        #region 删除
        [HttpPost]
        [SupportFilter]
        public ActionResult Delete(long id)
        {
            if (id != 0)
            {
                if (m_BLL.Delete(ref errors, id))
                {
                    LogHandler.WriteServiceLog(GetUserTrueName(), "Id:" + id, "成功", "删除", "WMS_Part");
                    return Json(JsonHandler.CreateMessage(1, Resource.DeleteSucceed));
                }
                else
                {
                    string ErrorCol = errors.Error;
                    LogHandler.WriteServiceLog(GetUserTrueName(), "Id" + id + "," + ErrorCol, "失败", "删除", "WMS_Part");
                    return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail + ErrorCol));
                }
            }
            else
            {
                return Json(JsonHandler.CreateMessage(0, Resource.DeleteFail));
            }
        }
        #endregion

        #region 导出导入
        [HttpPost]
        [SupportFilter]
        public ActionResult Import(string filePath)
        {
            if (m_BLL.ImportExcelData(GetUserTrueName(), Utils.GetMapPath(filePath), ref errors))
            {
                LogHandler.WriteImportExcelLog(GetUserTrueName(), "WMS_Part", filePath.Substring(filePath.LastIndexOf('/') + 1), filePath, "导入成功");
                return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed, filePath));
            }
            else
            {
                LogHandler.WriteImportExcelLog(GetUserTrueName(), "WMS_Part", filePath.Substring(filePath.LastIndexOf('/') + 1), filePath, "导入失败");
                return Json(JsonHandler.CreateMessage(0, Resource.InsertFail, filePath));
            }

            //var list = new List<WMS_PartModel>();
            //bool checkResult = m_BLL.CheckImportData(Utils.GetMapPath(filePath), list, ref errors);
            ////校验通过直接保存
            //if (checkResult)
            //{
            //    m_BLL.SaveImportData(list);
            //    LogHandler.WriteServiceLog(GetUserTrueName(), "导入成功", "成功", "导入", "WMS_Part");
            //    return Json(JsonHandler.CreateMessage(1, Resource.InsertSucceed));
            //}
            //else
            //{
            //    string ErrorCol = errors.Error;
            //    LogHandler.WriteServiceLog(GetUserTrueName(), ErrorCol, "失败", "导入", "WMS_Part");
            //    return Json(JsonHandler.CreateMessage(0, Resource.InsertFail + ErrorCol));
            //}
        }
        [HttpPost]
        [SupportFilter(ActionName = "Export")]
        public JsonResult CheckExportData(string queryStr)
        {
            List<WMS_PartModel> list = m_BLL.GetList(ref setNoPagerAscById, queryStr);
            if (list.Count().Equals(0))
            {
                return Json(JsonHandler.CreateMessage(0, "没有可以导出的数据"));
            }
            else
            {
                return Json(JsonHandler.CreateMessage(1, "可以导出"));
            }
        }
        [SupportFilter]
        public ActionResult Export(string queryStr)
        {
            List<WMS_PartModel> list = m_BLL.GetList(ref setNoPagerAscById, queryStr);
            JArray jObjects = new JArray();
            foreach (var item in list)
            {
                var jo = new JObject();
                //jo.Add("物料ID", item.Id);
                jo.Add("物料编码", item.PartCode);
                jo.Add("物料名称", item.PartName);
                jo.Add("物料类型", item.PartType);
                jo.Add("客户编码", item.CustomerCode);
                jo.Add("物流号", item.LogisticsCode);
                jo.Add("额外信息编码", item.OtherCode);
                jo.Add("每箱数量", item.PCS);
                jo.Add("保管员", item.StoreMan);
                jo.Add("单位", item.Unit);
                jo.Add("每箱体积", item.Volume);
                jo.Add("说明", item.Remark);

                //jo.Add("物料状态", item.Status);
                //jo.Add("创建人", item.CreatePerson);
                //jo.Add("创建时间", item.CreateTime);
                //jo.Add("修改人", item.ModifyPerson);
                //jo.Add("修改时间", item.ModifyTime);
                jObjects.Add(jo);
            }
            var dt = JsonConvert.DeserializeObject<DataTable>(jObjects.ToString());
            var exportFileName = string.Concat(
                RouteData.Values["controller"].ToString() + "_",
                DateTime.Now.ToString("yyyyMMddHHmmss"),
                ".xlsx");
            return new ExportExcelResult
            {
                SheetName = "Sheet1",
                FileName = exportFileName,
                ExportData = dt
            };
        }
        [SupportFilter(ActionName = "Export")]
        public ActionResult ExportTemplate()
        {
            JArray jObjects = new JArray();
            var jo = new JObject();
            //jo.Add("物料ID", "");
            jo.Add("物料编码", "");
            jo.Add("物料名称", "");
            jo.Add("物料类型", "");
            jo.Add("客户编码", "");
            jo.Add("物流号", "");
            jo.Add("额外信息编码", "");
            jo.Add("每箱数量", "");
            jo.Add("保管员", "");
            jo.Add("单位", "");
            jo.Add("每箱体积", "");
            jo.Add("说明", "");
            //jo.Add("物料状态", "");
            //jo.Add("创建人", "");
            //jo.Add("创建时间", "");
            //jo.Add("修改人", "");
            //jo.Add("修改时间", "");
            jo.Add("导入的错误信息", "");
            jObjects.Add(jo);
            var dt = JsonConvert.DeserializeObject<DataTable>(jObjects.ToString());
            var exportFileName = string.Concat("零件导入模板",
                    ".xlsx");
            return new ExportExcelResult
            {
                SheetName = "Sheet1",
                FileName = exportFileName,
                ExportData = dt
            };
        }
        #endregion

        #region 选择物料
        /// <summary>
        /// 弹出选择物料
        /// </summary>
        /// <param name="mulSelect">是否多选</param>
        /// <returns></returns>
        [SupportFilter(ActionName = "Index")]
        public ActionResult PartLookUp(bool mulSelect = false)
        {
            return View();
        }

        [HttpPost]
        [SupportFilter(ActionName = "Index")]
        public JsonResult PartGetList(GridPager pager, string partCode, string partName)
        {
            List<WMS_PartModel> list = m_BLL.GetListByWhere(ref pager, "Status == \"有效\" && PartCode.Contains(\"" 
                + partCode + "\") && PartName.Contains(\"" + partName + "\")");
            GridRows<WMS_PartModel> grs = new GridRows<WMS_PartModel>();
            grs.rows = list;
            grs.total = pager.totalRows;
            return Json(grs);
        }
        #endregion
    }
}

