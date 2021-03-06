﻿using Lark.Bot.CQA.Uitls;
using Lark.Bot.CQA.Uitls.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services
{
    public class NewsService:INewsService
    {
        public string[] RequestBiQuanApi()
        {
            string[] reStr;

            try
            {
                var jinseLatestNewsFlash = JsonConvert.DeserializeObject<CoinNewsResultModel<CoinNewsModel>>(HttpUitls.Get(ConfigManager.pushNewsConfig.NewsServerURL + "/News/GetJinseLatestNewsFlash"));
                var bishijieLatestNewsFlash = JsonConvert.DeserializeObject<CoinNewsResultModel<CoinNewsModel>>(HttpUitls.Get(ConfigManager.pushNewsConfig.NewsServerURL + "/News/GetBishijieLatestNewsFlash"));
                var bitcoinLatestNewsFlash = JsonConvert.DeserializeObject<CoinNewsResultModel<CoinNewsModel>>(HttpUitls.Get(ConfigManager.pushNewsConfig.NewsServerURL + "/News/GetBitcoinLatestNewsFlash"));
                var OkexNotice = JsonConvert.DeserializeObject<CoinNewsResultModel<CoinNewsModel>>(HttpUitls.Get(ConfigManager.pushNewsConfig.NewsServerURL + "/News/GetOkexLatestNotice"));

                reStr = new string[] { "【金色财经】" + jinseLatestNewsFlash.Data.Content, "【币世界】" + bishijieLatestNewsFlash.Data.Content, "【Bitcoin】" + bitcoinLatestNewsFlash.Data.Content, "【OKEX公告】" + OkexNotice.Data.Title + " " + OkexNotice.Data.FromUrl };

            }
            catch (Exception e)
            {
                reStr = new string[] { e.ToString() + "\n席马达！程序BUG了，快召唤老铁来维修!" };
            }

            return reStr;
        }
    }

    #region News Pojo
    public class CoinNewsModel
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 重要性等级
        /// </summary>
        public int ImportantLevel { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string From { get; set; }
        /// <summary>
        /// 来源地址
        /// </summary>
        public string FromUrl { get; set; }
        /// <summary>
        /// 来源方推送时间
        /// </summary>
        public string PushTime { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 标签
        /// </summary>
        public string Tag { get; set; }
        /// <summary>
        /// 推送等级
        /// </summary>
        public int PushLevel { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public string AddTime { get; set; }
    }

    public class CoinNewsResultModel<T>
    {
        /// <summary>
        /// 返回状态
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Msg { get; set; }
    }
    #endregion

}
