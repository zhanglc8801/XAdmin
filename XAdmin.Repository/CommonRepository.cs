using XAdmin.SqlSugar;
using XAdmin.Model.CustomEntity;
using XAdmin.Model.Entity;
using XAdmin.Model.QueryEntity;
using XAdmin.IRepository;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XAdmin.Repository
{
    public class CommonRepository: ICommonRepository
    {
        SqlSugarClient db = SugarContext.GetMSSqlInstance();

        /// <summary>
        /// 获取省级行政区划列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<Area>> GetProvinceList(AreaQuery query)
        {
            var area = db.Queryable<Area>().Where(x => x.Status == true);
            if (query.ProvinceID == 0)
                area = db.Queryable<Area>().Where(x => x.Status == true && x.ParentID == 1);
            else if (query.ProvinceID == 1)
                area = db.Queryable<Area>().Where(x => (x.Status == true && x.ParentID == 1) || x.ID == 1);
             else if (query.ProvinceID > 1)
                area = db.Queryable<Area>().Where(x => (x.Status == true && x.ParentID == 1) && x.ID == query.ParentID);
            return await area.ToListAsync();
        }

        /// <summary>
        /// 根据ParentID获取行政区划列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<List<Area>> GetAreaListByParentID(AreaQuery query)
        {
            var area = db.Queryable<Area>().Where(x => x.Status == true);
            area = db.Queryable<Area>().Where(x => x.Status == true && x.ParentID == query.ParentID);
            return await area.ToListAsync();
        }

        /// <summary>
        /// 获取行政区划实体
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Area GetAreaModel(AreaQuery query)
        {
            return db.Queryable<Area>().Where(x => x.ID == query.ID).First();
        }

        #region 通用选择区域下拉框TreeSelect
        /// <summary>
        /// 通用选择区域下拉框TreeSelect
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<TreeSelectModel> MakeAreaTreeSelect(AreaQuery query)
        {
            var area = db.Queryable<Area>().Where(x => (x.Status == true && x.ParentID != -1 && x.ParentID != 0) || x.ID == 1);
            if (query.ParentID > 0 && query.ParentID != 1)
                area = db.Queryable<Area>().Where(x => (x.Status == true && x.ParentID != -1 && x.ParentID != 0));

            if (!string.IsNullOrWhiteSpace(query.AreaName))
                area = area.Where(x => x.AreaName.Contains(query.AreaName));

            List<Area> areaList = await area.OrderBy("ID ASC").ToListAsync();
            TreeSelectModel model = new TreeSelectModel();
            Area a = new Area();
            if (query.ParentID == 0)
            {
                a.ID = 1;
                a.AreaName = "所有区域";
            }
            else
            {
                a.ID = query.ParentID.Value;
                a.AreaName = db.Queryable<Area>().Where(x => x.ID == query.ParentID.Value).First().AreaName;
            }
            model.TreeSelectList = InitRootTreeSelect(areaList, a);
            return model;
        }
        #region 生成树形菜单下拉框

        private List<TreeSelectItem> InitRootTreeSelect(List<Area> areaList, Area item)
        {
            List<TreeSelectItem> rootNodeList = new List<TreeSelectItem>();
            List<Area> List = areaList.Where(x => x.ID == item.ID).OrderBy(x => x.ID).ToList();
            TreeSelectItem node0 = new TreeSelectItem();
            node0.id = item.ID;
            node0.name = item.AreaName;
            node0.open = true;
            node0.@checked = false;
            node0.children = InitTreeSelect(areaList, List[0]);
            rootNodeList.Add(node0);
            return rootNodeList;
        }

        private List<TreeSelectItem> InitTreeSelect(List<Area> areaList, Area item)
        {
            List<TreeSelectItem> rootNodeList = new List<TreeSelectItem>();
            List<Area> childNodeList = areaList.Where(x => x.ParentID == item.ID).OrderBy(x => x.ID).ToList();
            foreach (var chl in childNodeList)
            {
                TreeSelectItem node = new TreeSelectItem();
                node.id = chl.ID;
                node.name = chl.AreaName;
                node.open = false;
                node.@checked = false;
                node.children = InitTreeSelect(areaList, chl);
                rootNodeList.Add(node);
            }
            return rootNodeList;
        }
        #endregion 
        #endregion

    }
}
