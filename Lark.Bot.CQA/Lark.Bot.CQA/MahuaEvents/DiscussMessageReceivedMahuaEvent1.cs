﻿
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using System;

namespace Lark.Bot.CQA.MahuaEvents
{
    /// <summary>
    /// 讨论组消息接受事件
    /// </summary>
    public class DiscussMessageReceivedMahuaEvent1
        : IDiscussMessageReceivedMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public DiscussMessageReceivedMahuaEvent1(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        public void ProcessDiscussGroupMessageReceived(DiscussMessageReceivedMahuaEventContext context)
        {
            // todo 填充处理逻辑
            if (context.Message.Equals("币圈消息"))
            {
                //查询币圈
                var reMsg = RequestHandler.RequestBiQuanApi() + "\n" + RequestHandler.GetBitPrice("btc_usdt")+"\n"+ context.FromDiscuss;
                //回发
                _mahuaApi.SendDiscussMessage(context.FromDiscuss, reMsg);
            }

            if (context.Message.Length > 4 && context.Message.Substring(0, 4).Equals("查币价 "))
            {
                string key = context.Message.Remove(0, 4);

                //使用CoolQApi将信息回发给发送者
                _mahuaApi.SendDiscussMessage(context.FromDiscuss, RequestHandler.GetBitPrice(key));
            }

            // 不要忘记在MahuaModule中注册
        }
    }
}
